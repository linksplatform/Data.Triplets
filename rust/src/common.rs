pub type SignedInteger = libc::int64_t;
pub type UnsignedInteger = libc::uint64_t;
pub type LinkIndex = UnsignedInteger;

pub type Visitor = extern "C" fn(LinkIndex);
pub type StoppableVisitor = extern "C" fn(LinkIndex) -> SignedInteger;

pub const SUCCESS_RESULT: SignedInteger = 0;
pub const ERROR_RESULT: SignedInteger = 1;