# author: Ethosa
##
## Hints to arguments:
## -  ``linksIndex`` -- Link index is the number of element in array of links.
##                      It can be also called address or identifier.
##
## -  ``mappedIndex`` -- Mapped index is the number of element in array of mapped link indices.
##                       These indices can be used to map specific meaning/concept/constant to concrete links.
##
## -  ``linkerIndex`` -- Link's linker index is the index of other link,
##                       that is specified as the beginning of the link.
##                       It can be used as type of connection between objects.
##                       It also can be used as a verb, if the link is used to represent phase or sentence.
##
## -  ``sourceIndex`` -- Link's source index is the index of other link,
##                       that is specified as the beginning of the link.
##                       It can be used to represent subject of the sentence.
##
## -  ``targetIndex`` -- Link's target index is the index of other link,
##                       that is specified as the ending of the link.
##                       It can be used to represent оbject of the sentence.
##
## -  ``stoppable_visitor`` -- Stoppable visitor is a function,
##                             that is used to handle each link in the set of links which is being walked through.
##                             The result of the function is used to decide to stop or to
##                             continue the walk through the set. 0 (false) means stop. 1 (true) means continue.
##
## -  ``visitor`` -- Visitor is a method, that is used to handle each link in the
##                   set of links which is being walked through.
##

import
  triplets/PersistentMemoryManager,
  triplets/Link
export
  PersistentMemoryManager,
  Link
