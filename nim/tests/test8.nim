# author: Ethosa
# open and close BD
import triplets

var is_open = OpenLinks("test8.db")

assert is_open == 1

if bool(is_open):
  var mapped = GetMappedLink 0

  SetMappedLink 0, mapped

  CloseLinks()
