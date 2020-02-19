# author: Ethosa
import LinkC
export LinkC


type
  Link* = culonglong

template create*(source, linker, target: culonglong): Link =
  ## Creates a new Link object
  ##
  ## Arguments:
  ## -   ``source`` -- start link.
  ## -   ``linker``.
  ## -   ``target`` -- target link.
  Link CreateLink(source, linker, target)

template create*(index: culonglong): Link =
  ## See `create template <#create,culonglong,culonglong,culonglong>`_
  Link CreateLink(index, index, index)

proc delete*(link: var Link) {.inline.} =
  ## Deletes link, if available.
  if link == 0:
    return
  DeleteLink link
  link = 0

proc merge*(link, other: var Link) {.inline.} =
  link = ReplaceLink(link, other)

template search*(source, linker, target: culonglong): Link =
  ## Searchs created Link object.
  ##
  ## Arguments:
  ## -   ``source`` -- start link.
  ## -   ``linker``.
  ## -   ``target`` -- target link.
  Link SearchLink(source, linker, target)

template search*(index: culonglong): Link =
  ## See `search template <#search,culonglong,culonglong,culonglong>`_
  Link SearchLink(index, index, index)

proc time*(index: Link): clonglong {.inline.} =
  ## Returns time of link creation.
  GetTime index

proc update*(link: var Link, s, l, t: culonglong) {.inline.} =
  link = UpdateLink(link, s, l, t)

proc update*(link: var Link, i: culonglong) {.inline.} =
  link = UpdateLink(link, i, i, i)
