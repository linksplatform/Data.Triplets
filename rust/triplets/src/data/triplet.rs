use doublets::data::LinkType;
use std::fmt::{Debug, Formatter};

#[derive(Default, Eq, PartialEq, Hash, Clone)]
pub struct Triplet<T> {
    pub from_id: T,
    pub to_id: T,
    pub type_id: T,
}

impl<T: LinkType> Triplet<T> {
    pub fn new(from_id: T, to_id: T, type_id: T) -> Self {
        Self {
            from_id,
            to_id,
            type_id,
        }
    }
}

impl<T: LinkType> Debug for Triplet<T> {
    fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
        let Self {
            from_id,
            to_id,
            type_id,
        } = self;
        write!(f, "{from_id} -> {to_id} | {type_id}")
    }
}
