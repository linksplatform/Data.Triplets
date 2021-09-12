#![feature(try_trait)]

use common::{*};
use link_c::{*};
use persistent_memory_manager::{*};

pub mod common;
pub mod link_c;
pub mod persistent_memory_manager;

#[cfg(test)]
mod test {
    use crate::{*};

    #[test]
    fn it_works() {
        {
            let mem = PersistentMemoryManager::create("db.links");
            println!("{:?}", mem);
            // close db file
        }

        let mem = PersistentMemoryManager::open("db.links").unwrap();
        println!("{:?}", mem);
        mem.close().unwrap();
    }
}
