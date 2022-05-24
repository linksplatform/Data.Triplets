#[cfg(windows)]
use std::os::windows::raw::HANDLE;
use libc::c_void;
use libc::int64_t;
use libc::uint64_t;

pub type SignedInteger = libc::int64_t;
pub type UnsignedInteger = libc::uint64_t;
pub type LinkIndex = UnsignedInteger;

pub type Visitor = extern "C" fn(LinkIndex);
pub type StoppableVisitor = extern "C" fn(LinkIndex) -> SignedInteger;

pub const SUCCESS_RESULT: SignedInteger = 0;
pub const ERROR_RESULT: SignedInteger = 1;

#[repr(C)]
pub struct Link
{
    pub SourceIndex: LinkIndex,
    pub TargetIndex: LinkIndex,
    pub LinkerIndex: LinkIndex,
    pub Timestamp: SignedInteger,
    pub BySourceRootIndex: LinkIndex,
    pub BySourceLeftIndex: LinkIndex,
    pub BySourceRightIndex: LinkIndex,
    pub BySourceCount: UnsignedInteger,
    pub ByTargetRootIndex: LinkIndex,
    pub ByTargetLeftIndex: LinkIndex,
    pub ByTargetRightIndex: LinkIndex,
    pub ByTargetCount: UnsignedInteger,
    pub ByLinkerRootIndex: LinkIndex,
    pub ByLinkerLeftIndex: LinkIndex,
    pub ByLinkerRightIndex: LinkIndex,
    pub ByLinkerCount: UnsignedInteger,
}

#[repr(C)]
pub struct RawDB
{
    #[cfg(windows)]
    pub storageFileHandle: HANDLE,
    #[cfg(windows)]
    pub storageFileMappingHandle: HANDLE,
    #[cfg(unix)]
    storageFileHandle: int64_t,
    pub storageFileSizeInBytes: int64_t,
    pub pointerToMappedRegion: *mut c_void,

    pub currentMemoryPageSizeInBytes: int64_t,
    pub serviceBlockSizeInBytes: int64_t,
    pub baseLinksSizeInBytes: int64_t,
    pub baseBlockSizeInBytes: int64_t,
    pub storageFileMinSizeInBytes: int64_t,

    pub pointerToDataSeal: *mut int64_t,
    pub pointerToLinkIndexSize: *mut int64_t,
    pub pointerToMappingLinksMaxSize: *mut int64_t,
    pub lpointerToPointerToMappingLinks: *mut LinkIndex,
    pub lpointerToLinksMaxSize: *mut LinkIndex,
    pub lpointerToLinksSize: *mut LinkIndex,
    pub LpointerToLinks: *mut Link,

    pub pointerToUnusedMarker: *mut Link,
}
