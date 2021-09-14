use crate::common::{*};

extern "C" {
    pub fn RawDB_new() -> *mut RawDB;

    pub fn GetSourceIndex(
        db: *mut RawDB,
        index: LinkIndex,
    ) -> LinkIndex;

    pub fn GetLinkerIndex(
        db: *mut RawDB,
        index: LinkIndex,
    ) -> LinkIndex;

    pub fn GetTargetIndex(
        db: *mut RawDB,
        index: LinkIndex,
    ) -> LinkIndex;

    pub fn GetTime(
        db: *mut RawDB,
        index: LinkIndex,
    ) -> SignedInteger;

    pub fn CreateLink(
        db: *mut RawDB,
        source: LinkIndex,
        linker: LinkIndex,
        target: LinkIndex,
    ) -> LinkIndex;

    pub fn SearchLink(
        db: *mut RawDB,
        source: LinkIndex,
        linker: LinkIndex,
        target: LinkIndex,
    ) -> LinkIndex;

    pub fn ReplaceLink(
        db: *mut RawDB,
        index: LinkIndex,
        replacement: LinkIndex,
    ) -> LinkIndex;

    pub fn UpdateLink(
        db: *mut RawDB,
        index: LinkIndex,
        source: LinkIndex,
        linker: LinkIndex,
        target: LinkIndex,
    ) -> LinkIndex;

    pub fn DeleteLink(
        db: *mut RawDB,
        index: LinkIndex,
    );

    pub fn GetFirstRefererBySourceIndex(
        db: *mut RawDB,
        index: LinkIndex,
    ) -> LinkIndex;

    pub fn GetFirstRefererByLinkerIndex(
        db: *mut RawDB,
        index: LinkIndex,
    ) -> LinkIndex;

    pub fn GetFirstRefererByTargetIndex(
        db: *mut RawDB,
        index: LinkIndex,
    ) -> LinkIndex;

    pub fn GetLinkNumberOfReferersBySource(
        db: *mut RawDB,
        index: LinkIndex,
    ) -> UnsignedInteger;

    pub fn GetLinkNumberOfReferersByLinker(
        db: *mut RawDB,
        index: LinkIndex,
    ) -> UnsignedInteger;

    pub fn GetLinkNumberOfReferersByTarget(
        db: *mut RawDB,
        index: LinkIndex,
    ) -> UnsignedInteger;

    pub fn WalkThroughAllReferersBySource(
        db: *mut RawDB,
        root: LinkIndex,
        visitor: Visitor,
    );

    pub fn WalkThroughReferersBySource(
        db: *mut RawDB,
        root: LinkIndex,
        visitor: StoppableVisitor,
    ) -> SignedInteger;

    pub fn WalkThroughAllReferersByLinker(
        db: *mut RawDB,
        root: LinkIndex,
        visitor: Visitor,
    );

    pub fn WalkThroughReferersByLinker(
        db: *mut RawDB,
        root: LinkIndex,
        visitor: StoppableVisitor,
    ) -> SignedInteger;

    pub fn WalkThroughAllReferersByTarget(
        db: *mut RawDB,
        root: LinkIndex,
        visitor: Visitor,
    );

    pub fn WalkThroughReferersByTarget(
        db: *mut RawDB,
        root: LinkIndex,
        visitor: StoppableVisitor,
    ) -> SignedInteger;
}
