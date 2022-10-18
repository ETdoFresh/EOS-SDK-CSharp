using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Unity.Internal
{
    internal static class DLLLoader
    {
#if UNITY_STANDALONE_WIN
        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern int FreeLibrary(IntPtr hLibModule);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
        
#elif UNITY_STANDALONE_LINUX
    [DllImport("libdl.so")]
    private static extern IntPtr dlopen(String fileName, int flags);

    [DllImport("libdl.so")]
    private static extern int dlclose(IntPtr handle);

    [DllImport("libdl.so")]
    private static extern IntPtr dlsym(IntPtr handle, String symbol);

    [DllImport("libdl.so")]
    private static extern IntPtr dlerror();

#elif UNITY_STANDALONE_OSX
    [DllImport("libdl.dylib")]
    private static extern IntPtr dlopen(String fileName, int flags);

    [DllImport("libdl.dylib")]
    private static extern int dlclose(IntPtr handle);

    [DllImport("libdl.dylib")]
    private static extern IntPtr dlsym(IntPtr handle, String symbol);

    [DllImport("libdl.dylib")]
    private static extern IntPtr dlerror();

#endif

#if UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX
    public static IntPtr LoadLibrary(string lpFileName) => dlopen(lpFileName, 2);

    public static int FreeLibrary(IntPtr hLibModule) => dlclose(hLibModule);

    public static IntPtr GetProcAddress(IntPtr hModule, string lpProcName)
    {
        // clear previous errors if any
        dlerror();
        var res = dlsym(hModule, lpProcName);
        var errPtr = dlerror();
        if (errPtr != IntPtr.Zero)
        {
            throw new Exception("dlsym: " + Marshal.PtrToStringAnsi(errPtr));
        }

        return res;
    }

#endif
    }
}