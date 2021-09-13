#![feature(try_trait)]

use common::{*};
use link_c::{*};
use persistent_memory_manager::{*};

pub mod common;
mod link_c;
mod persistent_memory_manager_c;
pub mod persistent_memory_manager;


#[cfg(test)]
mod test {
    use crate::{*};

    #[test]
    fn it_works() {
        {
            let mem = Links::create("db.links");
            println!("{:?}", mem);
            // close db file
        }

        let mem = Links::open("db.links").unwrap();
        println!("{:?}", mem);
        mem.close().unwrap();
    }
}
