use crate::{Error, Fuse, Link};
use doublets::data::{Flow, LinkType, LinksConstants, ToQuery};
use std::{
    default::default,
    ops::{ControlFlow, Try},
};

type LinksResult<T> = Result<T, Error<T>>;

pub type ReadHandler<'a, T> = &'a mut dyn FnMut(Link<T>) -> Flow;

pub type WriteHandler<'a, T> = &'a mut dyn FnMut(Link<T>, Link<T>) -> Flow;

pub trait Links<T: LinkType>: Send + Sync {
    fn constants(&self) -> &LinksConstants<T>;

    fn count_links(&self, query: &[T]) -> T;

    fn create_links(&mut self, query: &[T], handler: WriteHandler<'_, T>)
    -> Result<Flow, Error<T>>;

    fn each_links(&self, query: &[T], handler: ReadHandler<'_, T>) -> Flow;

    fn update_links(
        &mut self,
        query: &[T],
        change: &[T],
        handler: WriteHandler<'_, T>,
    ) -> Result<Flow, Error<T>>;

    fn delete_links(&mut self, query: &[T], handler: WriteHandler<'_, T>)
    -> Result<Flow, Error<T>>;

    fn iter_links(&self) -> Box<dyn Iterator<Item = Link<T>>> {
        self.each_iter_links(&[])
    }

    fn each_iter_links(&self, query: &[T]) -> Box<dyn Iterator<Item = Link<T>>> {
        let capacity = self.count_links(query).as_usize();

        cfg_if::cfg_if! {
            if #[cfg(feature = "smallvec")] {
                let mut vec = smallvec::SmallVec::<[_; 2]>::with_capacity(capacity);
            } else {
                let mut vec = Vec::with_capacity(capacity);
            }
        }

        self.each_links(query, &mut |link| {
            vec.push(link);
            Flow::Continue
        });

        box vec.into_iter()
    }
}
pub trait Triplets<T: LinkType>: Links<T> {
    fn count_by(&self, query: impl ToQuery<T>) -> T
    where
        Self: Sized,
    {
        self.count_links(&query.to_query()[..])
    }

    fn count(&self) -> T
    where
        Self: Sized,
    {
        self.count_by([])
    }

    fn create_by_with<F, R>(
        &mut self,
        query: impl ToQuery<T>,
        mut handler: F,
    ) -> Result<R, Error<T>>
    where
        F: FnMut(Link<T>, Link<T>) -> R,
        R: Try<Output = ()>,
        Self: Sized,
    {
        let mut output = R::from_output(());
        let query = query.to_query();

        self.create_links(
            &query[..],
            &mut |before, after| match handler(before, after).branch() {
                ControlFlow::Continue(_) => Flow::Continue,
                ControlFlow::Break(residual) => {
                    output = R::from_residual(residual);
                    Flow::Break
                }
            },
        )
        .map(|_| output)
    }

    fn create_by(&mut self, query: impl ToQuery<T>) -> Result<T, Error<T>>
    where
        Self: Sized,
    {
        let mut index = default();
        self.create_by_with(query, |_before, link| {
            index = link.id;
            Flow::Continue
        })
        .map(|_| index)
    }

    fn create_with<F, R>(&mut self, handler: F) -> Result<R, Error<T>>
    where
        F: FnMut(Link<T>, Link<T>) -> R,
        R: Try<Output = ()>,
        Self: Sized,
    {
        self.create_by_with([], handler)
    }

    fn create(&mut self) -> LinksResult<T>
    where
        Self: Sized,
    {
        self.create_by([])
    }

    fn create_link_with<F, R>(
        &mut self,
        from_id: T,
        to_id: T,
        type_id: T,
        handler: F,
    ) -> Result<Flow, Error<T>>
    where
        F: FnMut(Link<T>, Link<T>) -> R,
        R: Try<Output = ()>,
        Self: Sized,
    {
        let mut new = default();
        let mut handler = Fuse::new(handler);
        self.create_with(|before, after| {
            new = after.id;
            handler(before, after);
            Flow::Continue
        })?;

        self.update_with(new, from_id, to_id, type_id, handler)
    }

    fn create_link(&mut self, from_id: T, to_id: T, type_id: T) -> LinksResult<T>
    where
        Self: Sized,
    {
        let mut result = default();
        self.create_link_with(from_id, to_id, type_id, |_, link| {
            result = link.id;
            Flow::Continue
        })
        .map(|_| result)
    }

    fn each_by<F, R>(&self, query: impl ToQuery<T>, mut handler: F) -> R
    where
        F: FnMut(Link<T>) -> R,
        R: Try<Output = ()>,
        Self: Sized,
    {
        let mut output = R::from_output(());
        let query = query.to_query();

        self.each_links(&query[..], &mut |link| match handler(link).branch() {
            ControlFlow::Continue(_) => Flow::Continue,
            ControlFlow::Break(residual) => {
                output = R::from_residual(residual);
                Flow::Break
            }
        });

        output
    }

    fn each<F, R>(&self, handler: F) -> R
    where
        F: FnMut(Link<T>) -> R,
        R: Try<Output = ()>,
        Self: Sized,
    {
        self.each_by([self.constants().any; 3], handler)
    }

    fn iter(&self) -> Box<dyn Iterator<Item = Link<T>>> {
        self.iter_links()
    }

    fn each_iter(&self, query: impl ToQuery<T>) -> Box<dyn Iterator<Item = Link<T>>>
    where
        Self: Sized,
    {
        self.each_iter_links(&query.to_query()[..])
    }

    fn update_by_with<H, R>(
        &mut self,
        query: impl ToQuery<T>,
        change: impl ToQuery<T>,
        mut handler: H,
    ) -> Result<R, Error<T>>
    where
        H: FnMut(Link<T>, Link<T>) -> R,
        R: Try<Output = ()>,
        Self: Sized,
    {
        let mut output = R::from_output(());
        let query = query.to_query();
        let change = change.to_query();

        self.update_links(
            &query[..],
            &change[..],
            &mut |before, after| match handler(before, after).branch() {
                ControlFlow::Continue(_) => Flow::Continue,
                ControlFlow::Break(residual) => {
                    output = R::from_residual(residual);
                    Flow::Break
                }
            },
        )
        .map(|_| output)
    }

    fn update_by(&mut self, query: impl ToQuery<T>, change: impl ToQuery<T>) -> LinksResult<T>
    where
        Self: Sized,
    {
        let mut result = default();
        self.update_by_with(query, change, |_, after| {
            result = after.id;
            Flow::Continue
        })
        .map(|_| result)
    }

    fn update_with<F, R>(
        &mut self,
        id: T,
        from_id: T,
        to_id: T,
        type_id: T,
        handler: F,
    ) -> Result<R, Error<T>>
    where
        F: FnMut(Link<T>, Link<T>) -> R,
        R: Try<Output = ()>,
        Self: Sized,
    {
        self.update_by_with([id], [id, from_id, to_id, type_id], handler)
    }

    fn update(&mut self, id: T, from_id: T, to_id: T, type_id: T) -> LinksResult<T>
    where
        Self: Sized,
    {
        self.update_by([id], [id, from_id, to_id, type_id])
    }

    fn delete_by_with<F, R>(
        &mut self,
        query: impl ToQuery<T>,
        mut handler: F,
    ) -> Result<R, Error<T>>
    where
        F: FnMut(Link<T>, Link<T>) -> R,
        R: Try<Output = ()>,
        Self: Sized,
    {
        let mut output = R::from_output(());
        let query = query.to_query();

        self.delete_links(
            &query[..],
            &mut |before, after| match handler(before, after).branch() {
                ControlFlow::Continue(_) => Flow::Continue,
                ControlFlow::Break(residual) => {
                    output = R::from_residual(residual);
                    Flow::Break
                }
            },
        )
        .map(|_| output)
    }

    fn delete_by(&mut self, query: impl ToQuery<T>) -> LinksResult<T>
    where
        Self: Sized,
    {
        let mut result = default();
        self.delete_by_with(query, |_, after| {
            result = after.id;
            Flow::Continue
        })
        .map(|_| result)
    }

    fn delete_with<F, R>(&mut self, index: T, handler: F) -> Result<R, Error<T>>
    where
        F: FnMut(Link<T>, Link<T>) -> R,
        R: Try<Output = ()>,
        Self: Sized,
    {
        self.delete_by_with([index], handler)
    }

    fn delete(&mut self, index: T) -> LinksResult<T>
    where
        Self: Sized,
    {
        self.delete_by([index])
    }

    fn get_link(&self, id: T) -> Option<Link<T>>;

    fn try_get_link(&self, id: T) -> Result<Link<T>, Error<T>> {
        self.get_link(id).ok_or(Error::NotFound(id))
    }

    fn found(&self, query: impl ToQuery<T>) -> bool
    where
        Self: Sized,
    {
        self.count_by(query) != T::funty(0)
    }

    fn find(&self, query: impl ToQuery<T>) -> Option<Link<T>>
    where
        Self: Sized,
    {
        let mut result = None;
        self.each_by(query, |link| {
            result = Some(link);
            Flow::Break
        });
        result
    }

    fn search(&self, from_id: T, to_id: T, type_id: T) -> Option<T>
    where
        Self: Sized,
    {
        self.find([self.constants().any, from_id, to_id, type_id])
            .map(|link| link.id)
    }

    fn get_or_create(&mut self, from_id: T, to_id: T, type_id: T) -> LinksResult<T>
    where
        Self: Sized,
    {
        if let Some(link) = self.search(from_id, to_id, type_id) {
            Ok(link)
        } else {
            self.create_link(from_id, to_id, type_id)
        }
    }
}
