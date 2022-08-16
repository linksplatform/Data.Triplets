use doublets::data::{LinkType, Query, ToQuery};
use std::fmt::{Debug, Formatter};

#[derive(Default, Eq, PartialEq, Clone, Hash)]
#[repr(C)]
pub struct Link<T> {
    pub id: T,
    pub from_id: T,
    pub to_id: T,
    pub type_id: T,
}

impl<T: LinkType> Link<T> {
    pub fn new(id: T, from_id: T, to_id: T, type_id: T) -> Self {
        Self {
            id,
            from_id,
            to_id,
            type_id,
        }
    }

    #[must_use]
    pub fn nothing() -> Self {
        Self::new(T::funty(0), T::funty(0), T::funty(0), T::funty(0))
    }

    fn as_slice(&self) -> &[T] {
        // SAFETY: `Self` is repr(C)
        unsafe { std::slice::from_raw_parts(&self.id, 4) }
    }
}

impl<T: LinkType> Debug for Link<T> {
    fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
        let Self {
            id,
            from_id,
            to_id,
            type_id,
        } = self;
        write!(f, "{id}: {from_id} {to_id} | {type_id}")
    }
}

impl<T: LinkType> ToQuery<T> for Link<T> {
    fn to_query(&self) -> Query<'_, T> {
        self.as_slice().to_query()
    }
}
