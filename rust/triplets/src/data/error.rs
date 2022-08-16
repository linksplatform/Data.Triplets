use crate::{Link, Triplet};
use doublets::{data::LinkType, mem};
use std::error::Error as StdError;

#[derive(thiserror::Error, Debug)]
pub enum Error<T: LinkType> {
    #[error("invalid query size - expected {expected}, got {got}")]
    InvalidQuery { expected: usize, got: usize },

    #[error("not found {0} link.")]
    NotFound(T),

    #[error("link {0:?} has dependencies")]
    HasUsages(Vec<Link<T>>),

    #[error("link {0:?} already exists")]
    AlreadyExists(Triplet<T>),

    #[error("limit for the number of links in the storage has been reached: {0}")]
    LimitReached(T),

    #[error("unable to allocate memory for links storage: `{0}`")]
    AllocFailed(#[from] mem::Error),

    #[error("other internal error: `{0}`")]
    Other(#[from] Box<dyn StdError + Sync + Send>),
}

static_assertions::assert_impl_all!(Error<usize>: Sync, Send);

impl<T: LinkType> From<doublets::Error<T>> for Error<T> {
    fn from(err: doublets::Error<T>) -> Self {
        use doublets::Error;
        match err {
            Error::NotExists(link) => Self::NotFound(link),
            Error::LimitReached(limit) => Self::LimitReached(limit),
            Error::AllocFailed(alloc) => Self::AllocFailed(alloc),
            other => Self::Other(box other),
        }
    }
}
