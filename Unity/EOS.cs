using Epic.OnlineServices.Platform;
using Epic.OnlineServices.Unity.Internal;

namespace Epic.OnlineServices.Unity
{
    public class EOS : MonoBehaviourDDOLSingleton<EOS>
    {
        private EOSPlatform _platform;

        public static PlatformInterface GetPlatformInterface() => TryAndGetPlatformInterface();
        
        private static PlatformInterface TryAndGetPlatformInterface()
        {
            if (!Instance) return null;
            if (Instance._platform != null) return Instance._platform.Interface;
            if (_isQuitting) return null;
            Instance._platform = new EOSPlatform();
            return Instance._platform.Interface;
        }
        
        private void OnDestroy()
        {
            EOSLogging.Dispose();
            _platform?.Dispose();
            _platform = null;
        }

        private void Update()
        {
            _platform?.Tick();
        }
    }
}