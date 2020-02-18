# author: Ethosa
# links create.
import triplets

var is_open = OpenLinks "test3.db"

assert is_open == 1

if bool is_open:
  var mylink = CreateLink(0, 0, 0)
  echo mylink

  DeleteLink mylink

  CloseLinks()
