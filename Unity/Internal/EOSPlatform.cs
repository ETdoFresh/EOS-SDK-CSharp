#if UNITY_EDITOR
	#define EOS_EDITOR
    #define EOS_DYNAMIC_BINDINGS
#endif

using System;
using Epic.OnlineServices.Platform;

namespace Epic.OnlineServices.Unity.Internal
{
    internal class EOSPlatform
    {
        private bool _isInitialized;
        private IntPtr _dllHandle;

        internal PlatformInterface Interface { get; private set; }

        internal EOSPlatform()
        {
            InitializePlatformInterface();
            CreatePlatformInterface();
        }

        internal void Dispose()
        {
            Interface?.Release();
            Interface = null;
            PlatformInterface.Shutdown();
#if EOS_DYNAMIC_BINDINGS
            Bindings.Unhook();
#endif
            DLLLoader.FreeLibrary(_dllHandle);
            _dllHandle = default;
        }

        internal void Tick()
        {
            if (Interface == null) return;
            Interface.Tick();
        }

        private Result InitializePlatformInterface()
        {
            if (_isInitialized) return Result.AlreadyConfigured;

#if EOS_DYNAMIC_BINDINGS
            _dllHandle = DLLLoader.LoadLibrary(DLLPath.GetDLLPathByPlatform());
            Bindings.Hook(_dllHandle, DLLLoader.GetProcAddress);
#endif
            var initializeOptions = new InitializeOptions
            {
                AllocateMemoryFunction = default,
                ReallocateMemoryFunction = default,
                ReleaseMemoryFunction = default,
                ProductName = EOSConfig.ProductName,
                ProductVersion = EOSConfig.ProductVersion,
                SystemInitializeOptions = default,
                OverrideThreadAffinity = default,
            };
            var result = PlatformInterface.Initialize(ref initializeOptions);
            if (result != Result.Success && result != Result.AlreadyConfigured)
                throw new Exception($"Failed to initialize platform interface {result}");
            _isInitialized = true;
            return result;
        }

        private PlatformInterface CreatePlatformInterface()
        {
            var options = new Options
            {
                Reserved = default,
                ProductId = EOSConfig.ProductId,
                SandboxId = EOSConfig.SandboxId,
                ClientCredentials = new ClientCredentials
                {
                    ClientId = EOSConfig.ClientId,
                    ClientSecret = EOSConfig.ClientSecret,
                },
                IsServer = false,
                EncryptionKey = EOSConfig.EncryptionKey,
                OverrideCountryCode = null,
                OverrideLocaleCode = null,
                DeploymentId = EOSConfig.DeploymentId,
                Flags = PlatformFlags.None,
                CacheDirectory = null,
                TickBudgetInMilliseconds = 0,
                RTCOptions = null,
                IntegratedPlatformOptionsContainerHandle = null
            };
            Interface = PlatformInterface.Create(ref options);
            return Interface;
        }
    }
}