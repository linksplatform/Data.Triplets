# author: Ethosa

when defined windows:
  const LIB_NAME*: string = "Platform_Data_Triplets_Kernel.dll"
when (defined linux) or (hostOS == "linux"):
  const LIB_NAME*: string = "libPlatform_Data_Triplets_Kernel.so"

echo "your host os - ", hostOS
