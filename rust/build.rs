fn main() {
    println!("cargo:rustc-link-search=native=dylibs");
    println!("cargo:rustc-link-lib=static=Platform.Data.Triplets.Kernel");
}