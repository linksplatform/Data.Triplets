# author: Ethosa

when defined windows:
  const LIB_NAME*: string = "Platform_Data_Triplets_Kernel.dll"
when (defined linux) or (hostOS == "linux"):
  const LIB_NAME*: string = "libPlatform_Data_Triplets_Kernel.so"
when hostOS == "macosx":
  const LIB_NAME*: string = "libPlatform_Data_Triplets_Kernel.dylib"

type
  stoppable_visitor* = proc(linkIndex: culonglong): clonglong {. cdecl .}
  visitor* = proc(linkIndex: culonglong) {. cdecl .}

echo "your host os - ", hostOS
