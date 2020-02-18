# authors: Ethosa, Konard
import consts

proc OpenLinks*(filename: cstring): cint {. cdecl, dynlib: LIB_NAME, importc .}
proc CloseLinks*() {. cdecl, dynlib: LIB_NAME, importc .}

proc GetMappedLink*(mappedIndex: clonglong): culonglong {. cdecl, dynlib: LIB_NAME, importc .}
proc SetMappedLink*(mappedIndex: clonglong, linkIndex: culonglong) {. cdecl, dynlib: LIB_NAME, importc .}

proc AllocateLink*(): culonglong {. cdecl, dynlib: LIB_NAME, importc .}
proc FreeLink*(link: culonglong) {. cdecl, dynlib: LIB_NAME, importc .}

proc WalkThroughAllLinks*(visitor: visitor): culonglong {. cdecl, dynlib: LIB_NAME, importc .}
proc WalkThroughLinks*(stoppable_visitor: stoppable_visitor) {. cdecl, dynlib: LIB_NAME, importc .}


proc GetLinksCount*(): culonglong {. cdecl, dynlib: LIB_NAME, importc .}
