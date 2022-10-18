using Epic.OnlineServices.Logging;
using UnityEngine;

namespace Epic.OnlineServices.Unity
{
    public static class EOSLogging
    {
        public delegate void ActionRef<T>(ref T item);
        public static event ActionRef<LogMessage> OnLog = delegate(ref LogMessage item) {  };

        public static bool LogToUnityDebug { set => SetLogToUnityDebug(value); }

        private static bool _hasRegisteredCallback;
        private static bool _logToUnityDebug = true;

        public static void SetLogLevel(LogLevel logLevel)
        {
            EOS.GetPlatformInterface();
            if (!_hasRegisteredCallback) RegisterCallback();
            LoggingInterface.SetLogLevel(LogCategory.AllCategories, logLevel);
        }

        internal static void Dispose()
        {
            OnLog -= UnityDebugLog;
            LoggingInterface.SetCallback(null);
        }

        private static void RegisterCallback()
        {
            if (_logToUnityDebug) OnLog += UnityDebugLog;
            LoggingInterface.SetCallback(OnLog.Invoke);
            _hasRegisteredCallback = true;
        }

        private static void SetLogToUnityDebug(bool newValue)
        {
            var oldValue = _logToUnityDebug;
            if (newValue && !oldValue) OnLog += UnityDebugLog;
            if (!newValue && oldValue) OnLog -= UnityDebugLog;
            _logToUnityDebug = newValue;
        }

        private static void UnityDebugLog(ref LogMessage message)
        {
            if (message.Level == LogLevel.Error || message.Level == LogLevel.Fatal)
                Debug.LogError($"[{message.Category}] {message.Message}");
            else if (message.Level == LogLevel.Warning)
                Debug.LogWarning($"[{message.Category}] {message.Message}");
            else
                Debug.Log($"[{message.Category}] {message.Message}");
        }
    }
}