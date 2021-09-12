use crate::common::{*};
use libc::c_char;
use std::path::Path;
use std::io::{Error, ErrorKind};
use std::ffi::{CStr, CString, NulError};

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

#[derive(Debug)]
pub struct PersistentMemoryManager;

impl PersistentMemoryManager {
    pub fn open<P: AsRef<Path>>(path: P) -> std::io::Result<Self> {
        //std::fs::File::open(&path)?; // wrapper to return an error if the file does not exist

        let raw_path_str = path.as_ref().to_str();
        let path_str;
        match raw_path_str {
            None => { return Err(Error::from(ErrorKind::NotFound)) }
            Some(str) => { path_str = str }
        }

        let c_str = CString::new(path_str)?;
        let result = unsafe { OpenLinks(c_str.as_ptr()) };
        match result {
            SUCCESS_RESULT => { Ok(Self { }) }
            _ => { Err(Error::from_raw_os_error(result as i32)) }
        }
    }

    pub fn create<P: AsRef<Path>>(path: P) -> std::io::Result<Self> {
        let raw_parent = path.as_ref().parent();
        match raw_parent {
            None => { Err(Error::from(ErrorKind::NotFound)) }
            Some(parent) => {
                std::fs::create_dir_all(parent)?;
                std::fs::File::create(&path)?;
                Self::open(path)
            }
        }
    }

    pub fn close(&self) -> std::io::Result<()> {
        let result = unsafe { CloseLinks() };
        match result {
            SUCCESS_RESULT => { Ok(()) }
            _ => { Err(Error::from_raw_os_error(result as i32)) }
        }
    }
}

impl Drop for PersistentMemoryManager {
    fn drop(&mut self) {
        self.close();
    }
}