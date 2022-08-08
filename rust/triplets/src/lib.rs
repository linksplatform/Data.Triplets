#![feature(try_trait_v2)]
#![feature(try_blocks)]
#![feature(backtrace)]
#![feature(box_syntax)]
#![feature(default_free_fn)]
#![feature(unboxed_closures)]
#![feature(fn_traits)]
#![cfg_attr(not(test), forbid(clippy::unwrap_used))]
#![warn(
    clippy::perf,
    clippy::single_match_else,
    clippy::dbg_macro,
    clippy::doc_markdown,
    clippy::wildcard_imports,
    clippy::struct_excessive_bools,
    clippy::semicolon_if_nothing_returned,
    clippy::pedantic
)]
// for `clippy::pedantic`
#![allow(
    clippy::missing_errors_doc,
    clippy::missing_panics_doc,
    clippy::missing_safety_doc
)]
#![deny(
    clippy::all,
    clippy::cast_lossless,
    clippy::redundant_closure_for_method_calls,
    clippy::use_self,
    clippy::unnested_or_patterns,
    clippy::trivially_copy_pass_by_ref,
    clippy::needless_pass_by_value,
    clippy::match_wildcard_for_single_variants,
    clippy::map_unwrap_or,
    unused_qualifications,
    unused_import_braces,
    unused_lifetimes,
    unreachable_pub,
    trivial_numeric_casts,
    // rustdoc,
    // missing_debug_implementations,
    // missing_copy_implementations,
    deprecated_in_future,
    meta_variable_misuse,
    non_ascii_idents,
    rust_2018_compatibility,
    rust_2018_idioms,
    future_incompatible,
    nonstandard_style,
)]

mod data;
pub mod doublets;

pub use self::data::{
    Each, Error, Fuse, Handler, Link, Links, ReadHandler, Triplet, Triplets,
    WriteHandler,
};
use ::doublets::{data::Flow::Continue, mem, unit, Doublets};

#[test]
fn basic() -> Result<(), Box<dyn std::error::Error>> {
    let doublets = unit::Store::<usize, _>::new(mem::Global::new())?;
    let mut store = doublets::Store::new(doublets)?;

    let x = store.create()?;
    let y = store.create()?;
    let z = store.create()?;

    let x = store.update_with(x, y, z, 0, |_, link| {
        println!("{link:?}");
        Continue
    })?;

    store.doublets.each(|link| {
        println!("{link:?}");
        Continue
    });

    store.each(|link| {
        println!("{link:?}");
        Continue
    });

    Ok(())
}
