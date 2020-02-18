# author: Ethosa
# AllocateLink and FreeLink.
import triplets

var is_open = OpenLinks("test2.db")

assert is_open == 1

if bool(is_open):
  var allocate = AllocateLink()

  assert allocate != 0

  FreeLink(allocate)

  CloseLinks()
