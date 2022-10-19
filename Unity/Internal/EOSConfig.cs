using System;
using System.IO;
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
        [SerializeField] private bool skipOnMissingConfig;
        [SerializeField] private string dynamicLibraryDirectory;
        private double _lastWarnTime = -1000;

        public static EOSConfig Instance => GetStaticInstance();
        public static string ProductName => Instance.productName;
        public static string ProductVersion => Instance.productVersion;
        public static string ProductId => Instance.productId;
        public static string SandboxId => Instance.sandboxId;
        public static string DeploymentId => Instance.deploymentId;
        public static string ClientId => Instance.clientId;
        public static string ClientSecret => Instance.clientSecret;
        public static string EncryptionKey => Instance.encryptionKey;
        public static string DynamicLibraryDirectory => Instance.dynamicLibraryDirectory;

        private void OnValidate()
        {
            CheckIfConfigured();
            ValidateDynamicLibraryLocation();
        }

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
            if (!_instance) _instance = instance;
            instance.OnValidate();
            var currentDirectory = Directory.GetCurrentDirectory();
            var resourcesDirectory = Path.GetDirectoryName(DefaultResourcePath);
            var absoluteDirectory = Path.Combine(currentDirectory, resourcesDirectory);
            Directory.CreateDirectory(absoluteDirectory);
            var absolutePath = Path.Combine(absoluteDirectory, "EOSConfig.asset");
            var relativePath = Path.Combine(resourcesDirectory, "EOSConfig.asset");
            if (!File.Exists(absolutePath))
                UnityEditor.AssetDatabase.CreateAsset(instance, relativePath);
            else
                instance = UnityEditor.AssetDatabase.LoadAssetAtPath<EOSConfig>(relativePath);
#endif
            return instance;
        }

        private void CheckIfConfigured()
        {
#if UNITY_EDITOR
            if (skipOnMissingConfig) return;
            if (!string.IsNullOrEmpty(clientId)) return;
            var currentTime = UnityEditor.EditorApplication.timeSinceStartup;
            const int fiveMinutes = 5 * 60;
            if (currentTime - _lastWarnTime < fiveMinutes) return;
            _lastWarnTime = currentTime;
            const string title = "EOS SDK";
            const string message = "Please configure EOS SDK in the Project Settings > Epic Online Services window.";
            if (UnityEditor.EditorUtility.DisplayDialog(title, message, "OK"))
                UnityEditor.SettingsService.OpenProjectSettings("Project/Epic Online Services");
#endif
        }

        private void ValidateDynamicLibraryLocation()
        {
#if UNITY_EDITOR
            const string filename = "EOSSDK-Win64-Shipping";
            const string ext = ".dll";
            var directory = dynamicLibraryDirectory?.Replace('/', Path.PathSeparator) ?? string.Empty;
            var path = Path.Combine(directory, filename + ext);
            if (!string.IsNullOrEmpty(dynamicLibraryDirectory) &&
                File.Exists(path)) return;

            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), filename + ".dll",
                SearchOption.AllDirectories);
            if (files.Length == 0) return;
            files[0] = files[0].Replace(Directory.GetCurrentDirectory(), "");
            if (files[0].StartsWith(Path.DirectorySeparatorChar.ToString()))
                files[0] = files[0].Substring(1);
            files[0] = files[0].Replace(Path.DirectorySeparatorChar, '/');
            var newDirectory = Path.GetDirectoryName(files[0]);
            if (string.IsNullOrEmpty(newDirectory)) return;
            Instance.dynamicLibraryDirectory = newDirectory;
            UnityEditor.EditorUtility.SetDirty(Instance);
#endif
        }
    }
}