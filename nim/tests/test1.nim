# author: Ethosa
# open and close BD
import triplets

var is_open = OpenLinks("test1.db")

assert is_open == 1

if bool(is_open):
  CloseLinks()
