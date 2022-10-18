namespace Epic.OnlineServices.Unity.Internal
{
    internal static class DLLPath
    {
        public static string GetDLLPathByPlatform()
        {
#if UNITY_STANDALONE_WIN && UNITY_64
            return $"{EOSConfig.DynamicLibraryDirectory}/EOSSDK-Win64-Shipping.dll";
#elif UNITY_STANDALONE_WIN
            return $"{EOSConfig.DynamicLibraryDirectory}/EOSSDK-Win32-Shipping.dll";
#elif UNITY_STANDALONE_LINUX
            return $"{EOSConfig.DynamicLibraryDirectory}/libEOSSDK-Linux-Shipping.so";
#elif UNITY_STANDALONE_OSX
            return $"{EOSConfig.DynamicLibraryDirectory}/libEOSSDK-Mac-Shipping.dylib";
#else
            return null;
#endif
        }
    }
}