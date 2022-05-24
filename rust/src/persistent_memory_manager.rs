use std::ffi::{CStr, CString, NulError};
use std::io::{Error, ErrorKind};
use std::path::Path;

use libc::{c_char, c_void};

use crate::common::{*};
use crate::link_c::{*};
use crate::persistent_memory_manager_c::{*};
use std::ops::Deref;

pub enum LinkKind {
    Source,
    Linker,
    Target,
}

pub struct LinkAdapter<'a> {
    parent: &'a PersistentMemoryManager,
    index: LinkIndex,
}

impl<'a> Clone for LinkAdapter<'a> {
    fn clone(&self) -> Self {
        Self { parent: self.parent, index: self.index }
    }
}

pub trait AsIndex {
    fn index(&self) -> LinkIndex;
}

impl AsIndex for LinkIndex {
    fn index(&self) -> LinkIndex {
        *self
    }
}

impl<'a> AsIndex for LinkAdapter<'a> {
    fn index(&self) -> LinkIndex {
        self.index
    }
}

impl<'a> AsIndex for &LinkAdapter<'a> {
    fn index(&self) -> LinkIndex {
        self.index
    }
}

impl<'a> AsIndex for &mut LinkAdapter<'a> {
    fn index(&self) -> LinkIndex {
        self.index
    }
}

impl<'a> Deref for LinkAdapter<'a> {
    type Target = LinkIndex;

    fn deref(&self) -> &Self::Target {
        &self.index
    }
}

impl<'a> LinkAdapter<'a> {
    // pub fn first_referer_by(&self, kind: LinkKind) -> Self {
    //     match kind {
    //         LinkKind::Source => unsafe {
    //             LinkAdapter {
    //                 parent: self.parent,
    //                 index: GetFirstRefererBySourceIndex(self.index),
    //             }
    //         }
    //         LinkKind::Linker => unsafe {
    //             LinkAdapter {
    //                 parent: self.parent,
    //                 index: GetFirstRefererByLinkerIndex(self.index),
    //             }
    //         }
    //         LinkKind::Target => unsafe {
    //             LinkAdapter {
    //                 parent: self.parent,
    //                 index: GetFirstRefererByTargetIndex(self.index),
    //             }
    //         }
    //     }
    // }

    pub fn update<S, L, T>(&mut self, source: S, linker: L, target: T)
        where S: AsIndex, L: AsIndex, T: AsIndex
    {
        self.index = *self.parent.update(&*self, source, linker, target)
    }

    pub fn delete(self) {
        self.parent.delete(self)
    }
}

pub struct Links;

#[derive(Debug)]
pub struct PersistentMemoryManager {
    db: *mut RawDB
}

impl Links {
    const ITSELF: LinkIndex = 0;
    const CONTINUE: LinkIndex = 0;
    const STOP: LinkIndex = 1;

    pub fn open<P: AsRef<Path>>(path: P) -> std::io::Result<PersistentMemoryManager> {
        std::fs::File::open(&path)?; // simulate return an error if the file does not exist

        let raw_path_str = path.as_ref().to_str();
        let path_str;
        match raw_path_str {
            None => { return Err(Error::from(ErrorKind::NotFound)); }
            Some(str) => { path_str = str }
        }

        let db = unsafe { RawDB_new() };

        let c_str = CString::new(path_str)?;
        let result = unsafe { OpenLinks(db, c_str.as_ptr()) };

        match result {
            SUCCESS_RESULT => { Ok(PersistentMemoryManager { db }) }
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
    pub fn create<S, L, T>(&'a self, source: S, linker: L, target: T) -> LinkAdapter<'a>
        where S: AsIndex, L: AsIndex, T: AsIndex
    {
        let index = unsafe { CreateLink(self.db, source.index(), linker.index(), target.index()) };
        LinkAdapter { parent: self, index }
    }

    pub fn update<I, S, L, T>(&'a self, index: I, source: S, linker: L, target: T) -> LinkAdapter<'a>
        where I: AsIndex, S: AsIndex, L: AsIndex, T: AsIndex
    {
        let index = unsafe { UpdateLink(self.db, index.index(), source.index(), linker.index(), target.index()) };
        LinkAdapter { parent: self, index }
    }

    pub fn delete<I: AsIndex>(&'a self, index: I) {
        unsafe { DeleteLink(self.db, index.index()) };
    }

    pub fn count(&self) -> usize {
        (unsafe { GetLinksCount(self.db) }) as usize
    }

    pub fn close(&self) -> std::io::Result<()> {
        let result = unsafe { CloseLinks(self.db) };
        match result {
            SUCCESS_RESULT => { Ok(()) }
            _ => { Err(Error::from_raw_os_error(result as i32)) }
        }
    }
}

impl Drop for PersistentMemoryManager {
    fn drop(&mut self) {
        self.close();
        unsafe { libc::free(self.db as *mut c_void) }
    }
}