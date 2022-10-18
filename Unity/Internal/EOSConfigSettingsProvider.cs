#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;

namespace Epic.OnlineServices.Unity.Internal
{
    internal static class EOSConfigSettingsProvider
    {
        [SettingsProvider]
        internal static SettingsProvider CreateEOSConfigSettingsProvider()
        {
            var provider = new SettingsProvider("Project/Epic Online Services", SettingsScope.Project)
            {
                label = "Epic Online Services for Unity (EOS)",
                guiHandler = (searchContext) =>
                {
                    var settings = new SerializedObject(EOSConfig.Instance);
                    EditorGUILayout.PropertyField(settings.FindProperty("productName"));
                    EditorGUILayout.PropertyField(settings.FindProperty("productVersion"));
                    EditorGUILayout.PropertyField(settings.FindProperty("productId"));
                    EditorGUILayout.PropertyField(settings.FindProperty("sandboxId"));
                    EditorGUILayout.PropertyField(settings.FindProperty("deploymentId"));
                    EditorGUILayout.PropertyField(settings.FindProperty("clientId"));
                    EditorGUILayout.PropertyField(settings.FindProperty("clientSecret"));
                    EditorGUILayout.PropertyField(settings.FindProperty("encryptionKey"));
                    EditorGUILayout.PropertyField(settings.FindProperty("logLevel"));
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(settings.FindProperty("warnOnMissingConfig"));
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(settings.FindProperty("dynamicLibraryDirectory"));
                    settings.ApplyModifiedProperties();
                },
                keywords = new HashSet<string>(new[]
                {
                    "EOS", "Product Name", "Product Version", "Product Id", "Sandbox Id", "Deployment Id", "Client Id",
                    "Client Secret", "Encryption Key", "Log Level", "Warn On Missing Config"
                })
            };

            return provider;
        }
    }
}
#endif