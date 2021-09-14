use crate::common::{*};
use libc::c_char;

extern "C" {
    pub fn OpenLinks(db: *mut RawDB, filename: *const c_char) -> SignedInteger;
    pub fn CloseLinks(db: *mut RawDB) -> SignedInteger;

    pub fn GetMappedLink(db: *mut RawDB, mapped_index: SignedInteger) -> LinkIndex;
    pub fn SetMappedLink(db: *mut RawDB, mapped_index: SignedInteger, link_index: LinkIndex);

    pub fn WalkThroughAllLinks(db: *mut RawDB, visitor: Visitor);
    pub fn WalkThroughLinks(db: *mut RawDB, visitor: StoppableVisitor) -> SignedInteger;

    pub fn GetLinksCount(db: *mut RawDB) -> UnsignedInteger;

    pub fn AllocateLink(db: *mut RawDB) -> LinkIndex;
    pub fn FreeLink(db: *mut RawDB, link_index: LinkIndex);
}