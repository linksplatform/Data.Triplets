use crate::{Each, Error, Handler, Link, Links, ReadHandler, Triplets, WriteHandler};
use doublets::{
    data::{Flow, LinkType, LinksConstants, Query, ToQuery},
    Doublets,
};
use std::{error, marker::PhantomData, ops::Try};

#[inline]
fn validate<T: LinkType>(query: &[T], len: usize) {
    let query = query.to_query();

    assert!(
        query.len() >= len,
        "invalid query size - expected {}, got {}",
        len,
        query.len(),
    );
}

pub struct Store<T, D>
where
    T: LinkType,
    D: Doublets<T>,
{
    pub(crate) doublets: D,
    marker: PhantomData<T>,
}

impl<T, D> Store<T, D>
where
    T: LinkType,
    D: Doublets<T>,
{
    const ID: usize = 0;
    const FROM_ID: usize = 1;
    const TO_ID: usize = 2;
    const TYPE_ID: usize = 3;

    pub fn new(doublets: D) -> Result<Self, Error<T>> {
        // is empty storage
        // todo: impl trait for this
        assert_eq!(doublets.count(), T::funty(0));

        Ok(Self {
            doublets: Self::init(doublets)?,
            marker: PhantomData,
        })
    }

    fn init(mut store: D) -> Result<D, Error<T>> {
        let ret: Result<_, Box<dyn error::Error + Send + Sync>> = try {
            store.create_point()?; // TripletRoot: 1
            store
        };
        ret.map_err(Into::into)
    }

    fn triplet_root() -> T {
        T::funty(1)
    }

    fn meta() -> T {
        Self::triplet_root()
    }

    fn match_link(query: &[T]) -> Link<T> {
        Link {
            id: query.get(Self::ID).copied().unwrap_or_default(),
            from_id: query.get(Self::FROM_ID).copied().unwrap_or_default(),
            to_id: query.get(Self::TO_ID).copied().unwrap_or_default(),
            type_id: query.get(Self::TYPE_ID).copied().unwrap_or_default(),
        }
    }

    fn raw_is_triplet(&self, id: T) -> Option<bool> {
        self.doublets
            .search(Self::triplet_root(), id)
            .map(|x| x != Self::triplet_root())
    }
}

impl<T: LinkType, D: Doublets<T>> Links<T> for Store<T, D> {
    fn constants(&self) -> &LinksConstants<T> {
        self.doublets.constants()
    }

    fn count_links(&self, query: &[T]) -> T {
        todo!()
    }

    fn create_links(&mut self, _: &[T], handler: WriteHandler<'_, T>) -> Result<Flow, Error<T>> {
        let ret: Result<_, Box<dyn error::Error + Send + Sync>> = try {
            let store = &mut self.doublets;

            let from = store.create()?;
            let id = store.create_link(from, T::funty(0))?;
            let _ = store.create_link(Self::triplet_root(), id)?;

            handler(
                Link::nothing(),
                Link::new(id, T::funty(0), T::funty(0), T::funty(0)),
            )
        };
        ret.map_err(Into::into)
    }

    fn each_links(&self, query: &[T], handler: ReadHandler<'_, T>) -> Flow {
        let any = self.constants().any;
        let flow = self
            .doublets
            .each_iter([any, Self::triplet_root(), any])
            .filter(|link| link.index != Self::triplet_root())
            // fixme: potential `.unwrap_unchecked`
            .try_for_each(|link| handler(self.try_get_link(link.target).expect("useless message")));
        flow
    }

    fn update_links(
        &mut self,
        query: &[T],
        change: &[T],
        handler: WriteHandler<'_, T>,
    ) -> Result<Flow, Error<T>> {
        let Link { id, .. } = Self::match_link(query);
        let Link {
            from_id,
            to_id,
            type_id,
            ..
        } = Self::match_link(change);

        let link = self.try_get_link(id)?;

        if !self.raw_is_triplet(from_id).unwrap_or(true) {
            return Err(Error::NotFound(from_id));
        }

        if !self.raw_is_triplet(to_id).unwrap_or(true) {
            return Err(Error::NotFound(to_id));
        }

        if !self.raw_is_triplet(type_id).unwrap_or(true) {
            return Err(Error::NotFound(type_id));
        }

        let ret: Result<_, Box<dyn error::Error + Send + Sync>> = try {
            let store = &mut self.doublets;

            let doublet = store.try_get_link(id)?;
            store.update(doublet.source, from_id, to_id)?;
            store.update(doublet.index, doublet.source, type_id)?;

            handler(link, Link::new(id, from_id, to_id, type_id))
        };
        ret.map_err(Into::into)
    }

    fn delete_links(
        &mut self,
        query: &[T],
        handler: WriteHandler<'_, T>,
    ) -> Result<Flow, Error<T>> {
        todo!()
    }
}

impl<T: LinkType, D: Doublets<T>> Triplets<T> for Store<T, D> {
    fn get_link(&self, id: T) -> Option<Link<T>> {
        if let Some(true) = self.raw_is_triplet(id) {
            let link = self.doublets.get_link(id)?;
            let doublet = self.doublets.get_link(link.source)?;
            Some(Link::new(id, doublet.source, doublet.target, link.target))
        } else {
            None
        }
    }
}
