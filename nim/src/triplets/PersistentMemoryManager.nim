# authors: Ethosa, Konard
import consts

proc OpenLinks*(filename: cstring): cint {. dynlib: LIB_NAME, importc .}
proc CloseLinks*() {. dynlib: LIB_NAME, importc .}

proc AllocateLink*(): culonglong {. dynlib: LIB_NAME, importc .}
proc FreeLink*(link: culonglong) {. dynlib: LIB_NAME, importc .}


proc GetLinksCount*(): culonglong {. dynlib: LIB_NAME, importc .}
