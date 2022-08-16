use crate::{Continue, Error, Link, Links, ReadHandler, Triplets, WriteHandler};
use doublets::{
    data::{Flow, LinkType, LinksConstants, ToQuery},
    Doublets, Link as Dink,
};
use std::{default::default, error, marker::PhantomData};

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

    fn match_link(query: &[T]) -> Link<T> {
        Link {
            id: query.get(Self::ID).copied().unwrap_or_default(),
            from_id: query.get(Self::FROM_ID).copied().unwrap_or_default(),
            to_id: query.get(Self::TO_ID).copied().unwrap_or_default(),
            type_id: query.get(Self::TYPE_ID).copied().unwrap_or_default(),
        }
    }

    fn raw_is_triplet(&self, id: T) -> Option<bool> {
        let any = self.constants().any;
        self.doublets.get_link(id).map(|doublet| {
            self.doublets
                .found([any, doublet.source, Self::triplet_root()])
        })
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

            // create triplet skeleton:
            // (id:
            //     (doublet: |from_id|, |to_id|)
            //     |type_id|
            // )
            // |from_id| |to_id| |type_id| is 0
            let doublet = store.create()?;
            let id = store.get_or_create(doublet, T::funty(0))?;
            let _ = store.get_or_create(doublet, Self::triplet_root())?;

            handler(Link::nothing(), Link { id, ..default() })
        };
        ret.map_err(Into::into)
    }

    fn each_links(&self, query: &[T], handler: ReadHandler<'_, T>) -> Flow {
        assert_eq!(query.len(), 4);

        let (ty, query) = query.split_last().expect("invalid query len");
        let store = &self.doublets;
        let any = self.constants().any;

        let flow = store
            .each_iter(query)
            .filter(|link| link.index != Self::triplet_root())
            .filter(|link| store.found([any, link.index, Self::triplet_root()]))
            .try_for_each(|link| {
                for triplet in store
                    .each_iter([any, link.index, any])
                    .filter(|link| link.target != Self::triplet_root())
                    .filter(|link| *ty == link.target || *ty == any)
                {
                    handler(Link::new(
                        triplet.index,
                        link.source,
                        link.target,
                        triplet.target,
                    ))?;
                }
                Continue
            });

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

        let ret: Result<_, doublets::Error<_>> = try {
            let store = &mut self.doublets;

            // forget old doublet part - it may be used in other triplets
            // it is clone to satisfy borrow anti-pattern: 😐
            // https://github.com/rust-unofficial/patterns/blob/main/anti_patterns/borrow_clone.md
            let Dink {
                source, /* doublet part */
                ..
            } = store.try_get_link(id)?;
            let doublet = store.get_or_create(from_id, to_id)?;
            // dont forget remove old link
            if source != store.update(id, doublet, type_id)? {
                store.delete(source)?;
            }
            let _ = store.get_or_create(doublet, Self::triplet_root())?;

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
        if self.raw_is_triplet(id)? {
            let link = self.doublets.get_link(id)?;
            let doublet = self.doublets.get_link(link.source)?;
            Some(Link::new(id, doublet.source, doublet.target, link.target))
        } else {
            None
        }
    }
}
