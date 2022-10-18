using System.IO;
using Epic.OnlineServices.Logging;
using UnityEngine;

namespace Epic.OnlineServices.Unity.Internal
{
    public class EOSConfig : ScriptableObject
    {
        private const string DefaultResourcePath = "Assets/ProjectSettings/Resources/EOSConfig.asset";
        private static EOSConfig _instance;

        [SerializeField] private string productName;
        [SerializeField] private string productVersion;
        [SerializeField] private string productId;
        [SerializeField] private string sandboxId;
        [SerializeField] private string deploymentId;
        [SerializeField] private string clientId;
        [SerializeField] private string clientSecret;
        [SerializeField] private string encryptionKey;
        [SerializeField] private LogLevel logLevel = LogLevel.Error;
        [SerializeField] private bool warnOnMissingConfig = true;
        [SerializeField] private string dynamicLibraryDirectory;

        public static EOSConfig Instance => GetStaticInstance();
        public static string ProductName => Instance.productName;
        public static string ProductVersion => Instance.productVersion;
        public static string ProductId => Instance.productId;
        public static string SandboxId => Instance.sandboxId;
        public static string DeploymentId => Instance.deploymentId;
        public static string ClientId => Instance.clientId;
        public static string ClientSecret => Instance.clientSecret;
        public static string EncryptionKey => Instance.encryptionKey;
        public static LogLevel LogLevel => Instance.logLevel;
        public static string DynamicLibraryDirectory => Instance.dynamicLibraryDirectory;
        private static bool IsConfigured => Instance != null && !string.IsNullOrEmpty(Instance.clientId);

        private static EOSConfig GetStaticInstance()
        {
            if (_instance != null) return _instance;
            _instance = Resources.Load<EOSConfig>("EOSConfig");
            if (_instance != null) return _instance;
            _instance = CreateAndSaveInstance();
            return _instance;
        }

        private static EOSConfig CreateAndSaveInstance()
        {
            var instance = CreateInstance<EOSConfig>();
#if UNITY_EDITOR
            var currentDirectory = Directory.GetCurrentDirectory();
            var resourcesDirectory = Path.GetDirectoryName(DefaultResourcePath);
            Directory.CreateDirectory(Path.Combine(currentDirectory, resourcesDirectory));
            UnityEditor.AssetDatabase.CreateAsset(instance, Path.Combine(resourcesDirectory, "EOSConfig.asset"));
#endif
            return instance;
        }

        internal static void CheckIfConfigured()
        {
#if UNITY_EDITOR
            if (Instance && !Instance.warnOnMissingConfig) return;
            if (IsConfigured) return;
            const string title = "EOS SDK";
            const string message = "Please configure EOS SDK in the Project Settings > Epic Online Services window.";
            if (UnityEditor.EditorUtility.DisplayDialog(title, message, "OK"))
                UnityEditor.SettingsService.OpenProjectSettings("Project/Epic Online Services");
#endif
        }

        internal static void ValidateDynamicLibraryLocation()
        {
#if UNITY_EDITOR
            if (!Instance) return;
            const string filename = "EOSSDK-Win64-Shipping";
            const string ext = ".dll";
            var path = Path.Combine(Instance.dynamicLibraryDirectory.Replace('/', Path.PathSeparator), filename + ext);
            if (!string.IsNullOrEmpty(Instance.dynamicLibraryDirectory) &&
                File.Exists(path)) return;

            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), filename + ".dll",
                SearchOption.AllDirectories);
            if (files.Length == 0) return;
            files[0] = files[0].Replace(Directory.GetCurrentDirectory(), "");
            if (files[0].StartsWith(Path.DirectorySeparatorChar.ToString()))
                files[0] = files[0].Substring(1);
            files[0] = files[0].Replace(Path.DirectorySeparatorChar, '/');
            var directory = Path.GetDirectoryName(files[0]);
            if (string.IsNullOrEmpty(directory)) return;
            Instance.dynamicLibraryDirectory = directory;
            UnityEditor.EditorUtility.SetDirty(Instance);
#endif
        }
    }
}