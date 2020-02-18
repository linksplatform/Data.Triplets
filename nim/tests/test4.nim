# author: Ethosa
# work with links.
import triplets

var is_open = OpenLinks "test4.db"

assert is_open == 1

if bool is_open:
  var mylink = CreateLink(0, 0, 0)
  echo mylink

  assert GetSourceIndex(mylink) == 1
  assert GetLinkerIndex(mylink) == 1
  assert GetTargetIndex(mylink) == 1

  DeleteLink(mylink)

  CloseLinks()
