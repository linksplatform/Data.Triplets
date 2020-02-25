# author: Ethosa
# links create.
import triplets

var is_open = OpenLinks "test10.db"

assert is_open == 1

if bool is_open:
  var mylink = link(0)

  var lcount = mylink.count(sourcemode, mylink.source)
  echo lcount

  mylink.delete

  CloseLinks()
