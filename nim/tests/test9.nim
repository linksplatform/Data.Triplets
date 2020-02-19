# author: Ethosa
# links create.
import triplets

var is_open = OpenLinks "test9.db"

assert is_open == 1

if bool is_open:
  var mylink = link(0)

  mylink.update 2

  mylink.delete

  CloseLinks()
