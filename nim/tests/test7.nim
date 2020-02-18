# author: Ethosa
# get links info.
import triplets

var is_open = OpenLinks "test7.db"

assert is_open == 1

if bool is_open:
  var
    isA = CreateLink(0, 0, 0)
    isNotA = CreateLink(0, 0, isA)
    link = CreateLink(0, isA, 0)
    thing = CreateLink(0, isNotA, link)
  isA = UpdateLink(isA, isA, isA, link)

  assert GetLinkNumberOfReferersBySource(thing) == 1
  assert GetLinkNumberOfReferersByLinker(isA) == 2
  assert GetLinkNumberOfReferersByTarget(link) == 3

  var
    thingVisitorCounter: culonglong
    isAVisitorCounter: culonglong
    linkVisitorCounter: culonglong

  proc ThingVisitor(linkIndex: culonglong) {. cdecl .} =
    thingVisitorCounter += linkIndex
  proc IsAVisitor(linkIndex: culonglong) {. cdecl .} =
    isAVisitorCounter += linkIndex
  proc LinkVisitor(linkIndex: culonglong) {. cdecl .} =
    linkVisitorCounter += linkIndex

  WalkThroughAllReferersBySource(thing, ThingVisitor)
  WalkThroughAllReferersByLinker(isA, IsAVisitor)
  WalkThroughAllReferersByTarget(link, LinkVisitor)

  assert thingVisitorCounter == 4
  assert isAVisitorCounter == (1 + 3)
  assert linkVisitorCounter == (1 + 3 + 4)

  CloseLinks()
