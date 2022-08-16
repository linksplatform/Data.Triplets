use crate::Link;
use doublets::data::{Flow, LinkType};
use std::{marker::PhantomData, ops::Try};

pub trait Handler<T, R>: FnMut(Link<T>, Link<T>) -> R
where
    T: LinkType,
    R: Try<Output = ()>,
{
    fn fuse(self) -> Fuse<Self, (Link<T>, Link<T>)>
    where
        Self: Sized,
    {
        Fuse::new(self)
    }
}

impl<T, R, All> Handler<T, R> for All
where
    T: LinkType,
    R: Try<Output = ()>,
    All: FnMut(Link<T>, Link<T>) -> R,
{
}

pub trait Each<T, R>: FnMut(Link<T>) -> R
where
    T: LinkType,
    R: Try<Output = ()>,
{
    fn fuse(self) -> Fuse<Self, (Link<T>,)>
    where
        Self: Sized,
    {
        Fuse::new(self)
    }
}

impl<T, R, All> Each<T, R> for All
where
    T: LinkType,
    R: Try<Output = ()>,
    All: FnMut(Link<T>) -> R,
{
}

#[derive(Debug)]
pub struct Fuse<H, A>
where
    H: FnMut<A>,
{
    handler: H,
    done: bool,
    marker: PhantomData<A>,
}

impl<F, A> Fuse<F, A>
where
    F: FnMut<A>,
    F::Output: Try<Output = ()>,
{
    pub fn new(handler: F) -> Self {
        Self {
            handler,
            done: false,
            marker: PhantomData,
        }
    }
}

impl<F, A> From<F> for Fuse<F, A>
where
    F: FnMut<A>,
    F::Output: Try<Output = ()>,
{
    fn from(handler: F) -> Self {
        Self::new(handler)
    }
}

impl<F, A> FnOnce<A> for Fuse<F, A>
where
    F: FnMut<A>,
    F::Output: Try<Output = ()>,
{
    type Output = Flow;

    extern "rust-call" fn call_once(self, args: A) -> Self::Output {
        self.handler.call_once(args).branch().into()
    }
}
impl<F, A> FnMut<A> for Fuse<F, A>
where
    F: FnMut<A>,
    F::Output: Try<Output = ()>,
{
    extern "rust-call" fn call_mut(&mut self, args: A) -> Self::Output {
        if self.done {
            Flow::Break
        } else {
            let result = self.handler.call_mut(args);
            if result.branch().is_break() {
                self.done = false;
                Flow::Break
            } else {
                Flow::Continue
            }
        }
    }
}
