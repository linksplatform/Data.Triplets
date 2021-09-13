use crate::common::{*};
use crate::persistent_memory_manager_c::{*};
use libc::c_char;
use std::path::Path;
use std::io::{Error, ErrorKind};
use std::ffi::{CStr, CString, NulError};

pub struct Links;

#[derive(Debug)]
pub struct PersistentMemoryManager;

impl Links {
    const ITSELF: LinkIndex = 0;
    const CONTINUE: LinkIndex = 0;
    const STOP: LinkIndex = 1;

    pub fn open<P: AsRef<Path>>(path: P) -> std::io::Result<PersistentMemoryManager> {
        std::fs::File::open(&path)?; // simulate return an error if the file does not exist

        let raw_path_str = path.as_ref().to_str();
        let path_str;
        match raw_path_str {
            None => { return Err(Error::from(ErrorKind::NotFound)) }
            Some(str) => { path_str = str }
        }

        let c_str = CString::new(path_str)?;
        let result = unsafe { OpenLinks(c_str.as_ptr()) };
        match result {
            SUCCESS_RESULT => { Ok(PersistentMemoryManager { }) }
            _ => { Err(Error::from_raw_os_error(result as i32)) }
        }
    }

    pub fn create<P: AsRef<Path>>(path: P) -> std::io::Result<PersistentMemoryManager> {
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
}

impl PersistentMemoryManager {
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