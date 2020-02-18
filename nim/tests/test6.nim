# author: Ethosa
# get links info.
import triplets

var is_open = OpenLinks "test6.db"

assert is_open == 1

if bool is_open:
  var
    isA = CreateLink(0, 0, 0)
    isNotA = CreateLink(0, 0, isA)
    link = CreateLink(0, isA, 0)
    thing = CreateLink(0, isNotA, link)

  assert GetLinksCount() == 4

  assert GetTargetIndex(isA) == isA

  isA = UpdateLink(isA, isA, isA, link) # Произведено замыкание.

  assert GetTargetIndex(isA) == link

  DeleteLink isA # Одна эта операция удалит все 4 связи.

  assert GetLinksCount() == 0

  CloseLinks()
