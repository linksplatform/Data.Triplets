use crate::common::{*};

extern "C" {
    pub fn GetSourceIndex(
        index: LinkIndex
    ) -> LinkIndex;

    pub fn GetLinkerIndex(
        index: LinkIndex
    ) -> LinkIndex;

    pub fn GetTargetIndex(
        index: LinkIndex
    ) -> LinkIndex;

    pub fn GetTime(
        index: LinkIndex
    ) -> SignedInteger;

    pub fn CreateLink(
        source: LinkIndex,
        linker: LinkIndex,
        target: LinkIndex,
    ) -> LinkIndex;

    pub fn SearchLink(
        source: LinkIndex,
        linker: LinkIndex,
        target: LinkIndex,
    ) -> LinkIndex;

    pub fn ReplaceLink(
        index: LinkIndex,
        replacement: LinkIndex,
    ) -> LinkIndex;

    pub fn UpdateLink(
        index: LinkIndex,
        source: LinkIndex,
        linker: LinkIndex,
        target: LinkIndex,
    ) -> LinkIndex;

    pub fn DeleteLink(
        index: LinkIndex
    );

    pub fn GetFirstRefererBySourceIndex(
        index: LinkIndex
    ) -> LinkIndex;

    pub fn GetFirstRefererByLinkerIndex(
        index: LinkIndex
    ) -> LinkIndex;

    pub fn GetFirstRefererByTargetIndex(
        index: LinkIndex
    ) -> LinkIndex;

    pub fn GetLinkNumberOfReferersBySource(
        index: LinkIndex
    ) -> UnsignedInteger;

    pub fn GetLinkNumberOfReferersByLinker(
        index: LinkIndex
    ) -> UnsignedInteger;

    pub fn GetLinkNumberOfReferersByTarget(
        index: LinkIndex
    ) -> UnsignedInteger;

    pub fn WalkThroughAllReferersBySource(
        root: LinkIndex,
        visitor: Visitor,
    );

    pub fn WalkThroughReferersBySource(
        root: LinkIndex,
        visitor: StoppableVisitor,
    ) -> SignedInteger;

    pub fn WalkThroughAllReferersByLinker(
        root: LinkIndex,
        visitor: Visitor,
    );

    pub fn WalkThroughReferersByLinker(
        root: LinkIndex,
        visitor: StoppableVisitor,
    ) -> SignedInteger;

    pub fn WalkThroughAllReferersByTarget(
        root: LinkIndex,
        visitor: Visitor,
    );

    pub fn WalkThroughReferersByTarget(
        root: LinkIndex,
        visitor: StoppableVisitor,
    ) -> SignedInteger;
}