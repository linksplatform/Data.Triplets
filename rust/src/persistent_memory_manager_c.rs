use crate::common::{*};
use libc::c_char;

extern "C" {
    pub fn OpenLinks(filename: *const c_char) -> SignedInteger;
    pub fn CloseLinks() -> SignedInteger;

    pub fn GetMappedLink(mapped_index: SignedInteger) -> LinkIndex;
    pub fn SetMappedLink(mapped_index: SignedInteger, link_index: LinkIndex);

    pub fn WalkThroughAllLinks(visitor: Visitor);
    pub fn WalkThroughLinks(visitor: StoppableVisitor) -> SignedInteger;

    pub fn GetLinksCount() -> UnsignedInteger;

    pub fn AllocateLink() -> LinkIndex;
    pub fn FreeLink(link_index: LinkIndex);
}