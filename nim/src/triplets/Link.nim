# author: Ethosa
import LinkC
export LinkC


type
  Link* = culonglong
    ## Link is a convenient wrapper for LinkC.nim.
  LinkMode* = enum
    sourcemode, linkermode, targetmode


template link*(source, linker, target: culonglong): Link =
  ## Creates a new Link object
  ##
  ## Arguments:
  ## -   ``source`` -- start link.
  ## -   ``linker``.
  ## -   ``target`` -- target link.
  Link CreateLink(source, linker, target)

template link*(index: culonglong): Link =
  ## See `create template <#create.t,culonglong,culonglong,culonglong>`_
  Link CreateLink(index, index, index)

proc delete*(link: var Link) {.inline.} =
  ## Deletes link, if available.
  if link == 0:
    return
  DeleteLink link
  link = 0

proc linker*(link: Link): culonglong {.inline.} =
  ## Gets Link's linker index.
  GetLinkerIndex(link)

proc merge*(link, other: var Link) {.inline.} =
  ## Merges two Links.
  link = ReplaceLink(link, other)

proc count*(link: Link, what: LinkMode, value: culonglong): culonglong =
  ## Gets the count of Links by source, linker or target.
  ##
  ## Arguments:
  ## -   ``what`` -- sourcemode, linkermode or targetmode
  ## -   ``value`` -- source, target or link.
  case what
  of sourcemode:
    return GetLinkNumberOfReferersBySource value
  of linkermode:
    return GetLinkNumberOfReferersByLinker value
  of targetmode:
    return GetLinkNumberOfReferersByTarget value

template search*(source, linker, target: culonglong): Link =
  ## Searchs created Link object.
  ##
  ## Arguments:
  ## -   ``source`` -- start link.
  ## -   ``linker``.
  ## -   ``target`` -- target link.
  Link SearchLink(source, linker, target)

template search*(index: culonglong): Link =
  ## See `search template <#search.t,culonglong,culonglong,culonglong>`_
  Link SearchLink(index, index, index)

proc source*(link: Link): culonglong {.inline.} =
  ## Gets Link's source index.
  GetSourceIndex(link)

proc target*(link: Link): culonglong {.inline.} =
  ## Gets Link's target index.
  GetTargetIndex(link)

proc time*(index: Link): clonglong {.inline.} =
  ## Returns time of link creation.
  GetTime index

proc update*(link: var Link, s, l, t: culonglong) {.inline.} =
  link = UpdateLink(link, s, l, t)

proc update*(link: var Link, i: culonglong) {.inline.} =
  link = UpdateLink(link, i, i, i)
