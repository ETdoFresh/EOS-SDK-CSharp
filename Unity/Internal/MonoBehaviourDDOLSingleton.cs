using UnityEngine;

namespace Epic.OnlineServices.Unity.Internal
{
    public class MonoBehaviourDDOLSingleton<T> : MonoBehaviour where T : MonoBehaviourDDOLSingleton<T>
    {
        private static T _instance;
        protected static bool _isQuitting;
        protected static T Instance => TryAndGetInstance();

        private static T TryAndGetInstance()
        {
            if (_instance != null) return _instance;
            if (_isQuitting) return null;
            _instance = FindObjectOfType<T>();
            if (_instance != null) return _instance;
            _instance = new GameObject(typeof(T).Name).AddComponent<T>();
            return _instance;
        }
        
        protected virtual void Awake()
        {
            _isQuitting = false;
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            _isQuitting = true;
        }
    }
}