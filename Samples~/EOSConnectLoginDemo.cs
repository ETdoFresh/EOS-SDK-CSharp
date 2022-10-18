using System.Collections;
using Epic.OnlineServices;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.Logging;
using Epic.OnlineServices.P2P;
using Epic.OnlineServices.Unity;
using UnityEngine;
using UnityEngine.UI;

public class EOSConnectLoginDemo : MonoBehaviour
{
    [SerializeField] private LogLevel logLevel = LogLevel.Verbose;
    [SerializeField] private Text textUI;

    private void OnEnable()
    {
        EOSLogging.OnLog += DisplayTextUI;
        EOSLogging.SetLogLevel(logLevel);
        StartCoroutine(LoginCoroutine());
    }

    private void OnDisable()
    {
        EOSLogging.OnLog -= DisplayTextUI;
    }

    private IEnumerator LoginCoroutine()
    {
        DisplayNetworkStatus();

        // Attempt to Login to EOS Connect with Device Id
        var login = AttemptToLoginWithDeviceId();
        yield return login.coroutine;
        if (login.data?.ResultCode == Result.NotFound)
        {
            // No Device Id found on this machine... Create a Device Id
            var createDeviceId = CreateDeviceId();
            yield return createDeviceId.coroutine;
            if (createDeviceId.data?.ResultCode != Result.Success)
            {
                Log($"Failed to create Device Id {createDeviceId.data?.ResultCode}");
                yield break;
            }
            
            // Attempt to Login Again
            var loginAgain = AttemptToLoginWithDeviceId();
            yield return loginAgain.coroutine;
            if (loginAgain.data?.ResultCode != Result.Success)
            {
                Log($"Failed to login with Device Id {loginAgain.data?.ResultCode}");
                yield break;
            }
        }
        else if (login.data?.ResultCode != Result.Success)
        {
            Log($"Failed to login with Device Id {login.data?.ResultCode}");
            yield break;
        }

        // Query NAT Type
        var queryNATType = QueryNATType();
        yield return queryNATType.coroutine;
    }

    private void DisplayNetworkStatus()
    {
        Log($"EOS Network Status: {EOS.GetPlatformInterface().GetNetworkStatus()}");
    }

    private CoroutineData<LoginCallbackInfo?> AttemptToLoginWithDeviceId()
    {
        var login = new CoroutineData<LoginCallbackInfo?>();

        IEnumerator LocalCoroutine()
        {
            var connectOptions = new LoginOptions
            {
                Credentials = new Credentials
                {
                    Token = default,
                    Type = ExternalCredentialType.DeviceidAccessToken
                },
                UserLoginInfo = new UserLoginInfo
                {
                    DisplayName = "FishyEOS"
                }
            };
            EOS.GetPlatformInterface().GetConnectInterface().Login(ref connectOptions, null,
                delegate(ref LoginCallbackInfo loginCallbackInfo) { login.data = loginCallbackInfo; });

            yield return new WaitUntil(() => login.data.HasValue);
            Log($"EOS Connect Login Status: {login.data?.ResultCode}");
        }

        login.coroutine = StartCoroutine(LocalCoroutine());
        return login;
    }

    private CoroutineData<OnQueryNATTypeCompleteInfo?> QueryNATType()
    {
        var queryNATType = new CoroutineData<OnQueryNATTypeCompleteInfo?>();

        IEnumerator LocalCoroutine()
        {
            var queryNATOptions = new QueryNATTypeOptions();
            EOS.GetPlatformInterface().GetP2PInterface().QueryNATType(ref queryNATOptions, null,
                delegate(ref OnQueryNATTypeCompleteInfo queryNATTypeCallbackInfo)
                {
                    queryNATType.data = queryNATTypeCallbackInfo;
                });
            yield return new WaitUntil(() => queryNATType.data.HasValue);
            Log($"EOS P2P NAT Type: {queryNATType.data?.NATType}");
        }

        queryNATType.coroutine = StartCoroutine(LocalCoroutine());
        return queryNATType;
    }

    private CoroutineData<CreateDeviceIdCallbackInfo?> CreateDeviceId()
    {
        var createDeviceId = new CoroutineData<CreateDeviceIdCallbackInfo?>();

        IEnumerator LocalCoroutine()
        {
            var createDeviceIdOptions = new CreateDeviceIdOptions
                { DeviceModel = $"{SystemInfo.deviceModel} {SystemInfo.deviceName}" };
            EOS.GetPlatformInterface().GetConnectInterface().CreateDeviceId(ref createDeviceIdOptions, null,
                delegate(ref CreateDeviceIdCallbackInfo createDeviceIdCallbackInfo)
                {
                    createDeviceId.data = createDeviceIdCallbackInfo;
                });
            yield return new WaitUntil(() => createDeviceId.data.HasValue);
            Log($"EOS Connect Create Device Id Status: {createDeviceId.data?.ResultCode}");
        }
        
        createDeviceId.coroutine = StartCoroutine(LocalCoroutine());
        return createDeviceId;
    }
    
    private CoroutineData<DeleteDeviceIdCallbackInfo?> DeleteDeviceId()
    {
        var deleteDeviceId = new CoroutineData<DeleteDeviceIdCallbackInfo?>();

        IEnumerator LocalCoroutine()
        {
            var deleteDeviceIdOptions = new DeleteDeviceIdOptions();
            EOS.GetPlatformInterface().GetConnectInterface().DeleteDeviceId(ref deleteDeviceIdOptions, null,
                delegate(ref DeleteDeviceIdCallbackInfo deleteDeviceIdCallbackInfo)
                {
                    deleteDeviceId.data = deleteDeviceIdCallbackInfo;
                });
            yield return new WaitUntil(() => deleteDeviceId.data.HasValue);
            Log($"EOS Connect Delete Device Id Status: {deleteDeviceId.data?.ResultCode}");
        }
        
        deleteDeviceId.coroutine = StartCoroutine(LocalCoroutine());
        return deleteDeviceId;
    }

    private void Log(string message)
    {
        var category = nameof(EOSConnectLoginDemo);
        Debug.Log($"[{category}] {message}");
        DisplayTextUI(LogLevel.Info, category, message);
    }

    private void DisplayTextUI(ref LogMessage item) =>
        DisplayTextUI(item.Level, item.Category, item.Message);

    private void DisplayTextUI(LogLevel logLevel, string logCategory, string message)
    {
        if (!Application.isPlaying) return;
        if (logLevel == LogLevel.Error || logLevel == LogLevel.Fatal)
        {
            textUI.text += $"\n<color=red>[{logCategory}] {message}</color>";
        }
        else if (logLevel == LogLevel.Warning)
        {
            textUI.text += $"\n<color=yellow>[{logCategory}] {message}</color>";
        }
        else
        {
            textUI.text += $"\n[{logCategory}] {message}";
        }
    }

    private class CoroutineData<T>
    {
        public T data;
        public Coroutine coroutine;
    }
    
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(EOSConnectLoginDemo))]
    public class CustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var testConnection = (EOSConnectLoginDemo) target;
            UnityEditor.EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            if (GUILayout.Button("Delete Device Id")) 
                testConnection.DeleteDeviceId();
            UnityEditor.EditorGUI.EndDisabledGroup();
        }
    }
#endif
}