# authors: Ethosa, Konard
import consts


proc CreateLink*(sourceIndex, linkerIndex, targetIndex: culonglong): culonglong 
  {. cdecl, dynlib: LIB_NAME, importc .}
proc SearchLink*(sourceIndex, linkerIndex, targetIndex: culonglong): culonglong 
  {. cdecl, dynlib: LIB_NAME, importc .}

proc ReplaceLink*(linkIndex, replacementIndex: culonglong): culonglong 
  {. cdecl, dynlib: LIB_NAME, importc .}
proc UpdateLink*(linkIndex, sourceIndex, linkerIndex, targetIndex: culonglong): culonglong 
  {. cdecl, dynlib: LIB_NAME, importc .}

proc DeleteLink*(linkIndex: culonglong) {. cdecl, dynlib: LIB_NAME, importc .}

proc GetTime*(linkIndex: culonglong): clonglong {. dynlib: LIB_NAME, importc .}

proc GetSourceIndex*(linkIndex: culonglong): culonglong {. cdecl, dynlib: LIB_NAME, importc .}
proc GetLinkerIndex*(linkIndex: culonglong): culonglong {. cdecl, dynlib: LIB_NAME, importc .}
proc GetTargetIndex*(linkIndex: culonglong): culonglong {. cdecl, dynlib: LIB_NAME, importc .}

proc GetFirstRefererBySourceIndex*(linkIndex: culonglong): culonglong {. cdecl, dynlib: LIB_NAME, importc .}
proc GetFirstRefererByLinkerIndex*(linkIndex: culonglong): culonglong {. cdecl, dynlib: LIB_NAME, importc .}
proc GetFirstRefererByTargetIndex*(linkIndex: culonglong): culonglong {. cdecl, dynlib: LIB_NAME, importc .}

proc GetLinkNumberOfReferersBySource*(linkIndex: culonglong): culonglong {. cdecl, dynlib: LIB_NAME, importc .}
proc GetLinkNumberOfReferersByLinker*(linkIndex: culonglong): culonglong {. cdecl, dynlib: LIB_NAME, importc .}
proc GetLinkNumberOfReferersByTarget*(linkIndex: culonglong): culonglong {. cdecl, dynlib: LIB_NAME, importc .}

proc WalkThroughAllReferersBySource*(linkIndex: culonglong, visitor: visitor)
  {. dynlib: LIB_NAME, importc .}
proc WalkThroughReferersBySource*(linkIndex: culonglong, stoppable_visitor: stoppable_visitor): clonglong
  {. dynlib: LIB_NAME, importc .}

proc WalkThroughAllReferersByLinker*(linkIndex: culonglong, visitor: visitor)
  {. dynlib: LIB_NAME, importc .}
proc WalkThroughReferersByLinker*(linkIndex: culonglong, stoppable_visitor: stoppable_visitor): clonglong
  {. dynlib: LIB_NAME, importc .}

proc WalkThroughAllReferersByTarget*(linkIndex: culonglong, visitor: visitor)
  {. dynlib: LIB_NAME, importc .}
proc WalkThroughReferersByTarget*(linkIndex: culonglong, stoppable_visitor: stoppable_visitor): clonglong
  {. dynlib: LIB_NAME, importc .}
