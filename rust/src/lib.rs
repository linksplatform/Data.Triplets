//#![feature(try_trait)]
#![feature(thread_local)]
#![feature(in_band_lifetimes)]

use common::{*};
use link_c::{*};
//use persistent_memory_manager::{*};

pub mod common;
mod link_c;
mod persistent_memory_manager_c;
//pub mod persistent_memory_manager;


#[cfg(test)]
mod test {
    use crate::common::RawDB;
    use libc::malloc;
    use crate::link_c::{RawDB_new, CreateLink, UpdateLink, DeleteLink};
    use crate::persistent_memory_manager_c::{OpenLinks, CloseLinks, GetLinksCount};
    use std::ffi::CString;

    #[test]
    fn CrUD() { unsafe {
        let db = RawDB_new();

        let name = CString::new("юникод_data_base.links").unwrap();
        OpenLinks(db, name.as_ptr());

        let itself = 0;

        let is_a = CreateLink(db, itself, itself, itself);
        let is_not_a = CreateLink(db, itself, itself, is_a);
        let link = CreateLink(db, itself, is_a, itself);
        let thing = CreateLink(db, itself, is_not_a, link);

        UpdateLink(db, is_a, is_a, is_a, link);

        DeleteLink(db, is_a);
        DeleteLink(db, thing);

        println!("Links count: {}", GetLinksCount(db));

        CloseLinks(db);
    }}
    /*
    use crate::{*};
    use std::cell::RefCell;
    use std::rc::Rc;
    use std::sync::Arc;
    use std::collections::hash_map::RandomState;

    #[test]
    fn it_works() {
        std::fs::remove_file("db.links");
        {
            let mem = Links::create("db.links");
            println!("{:?}", mem);
            // close db file
        }

        let mem = Links::open("db.links").unwrap();
        println!("{:?}", mem);
        mem.close().unwrap();
    }

    #[test]
    fn CrUD() {
        std::fs::remove_file("db.links");
        let mem = Links::create("db.links").unwrap();

        let itself = 0;

        let mut is_a = mem.create(itself, itself, itself);
        let is_not_a = mem.create(itself, itself, &is_a);
        let link = mem.create(itself, &is_a, itself);
        let thing = mem.create(itself, &is_not_a, &link);

        let clone = is_a.clone();
        is_a.update(&clone, &clone, &link);
        // or
        // is_a.update(is_a.index(), is_a.index(), &link);

        is_a.delete(); // delete all
        thing.delete();

        println!("{:?}", mem.count());
    }*/
}
