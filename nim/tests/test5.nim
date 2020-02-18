# author: Ethosa
# get links info.
import triplets

var is_open = OpenLinks "test5.db"

assert is_open == 1

if bool is_open:
  var mylink = CreateLink(0, 0, 0)

  assert GetLinksCount() == 1
  echo mylink

  assert GetSourceIndex(mylink) == 1
  assert GetLinkerIndex(mylink) == 1
  assert GetTargetIndex(mylink) == 1

  DeleteLink(mylink)

  assert GetLinksCount() == 0

  CloseLinks()
