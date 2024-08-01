using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
struct RegisterData {
    public string uuid;
    public string model;
    public string platform;
}

public static class AnalyzeManager
{
    static readonly string host = "172.30.1.10";
    static readonly ushort port = 3000;

    static string GetURL(string path) => $"http://{host}:{port}/game/{path}";

    static public bool Registered { get; set; } = false; // 등록됨?
    
    // 등록하기
    public static async Task<bool> RegisterDevice() {
        // 보낼 데이터
        RegisterData form = new()
        {
            uuid = SystemInfo.deviceUniqueIdentifier,
            model = SystemInfo.deviceModel,
            platform = Application.platform.ToString(),
        };

        using var handler = UnityWebRequest.Post(GetURL("register"), JsonUtility.ToJson(form), "application/json");

        var operation = handler.SendWebRequest();
        
        while (!operation.isDone)
            await Task.Yield();

        // if (handler.result != UnityWebRequest.Result.Success) {
        //     Debug.LogError($"[domiAnalyze] 등록 실패. {handler.result}");            
        //     return false;
        // }

        string body = "empty";
        try {
            body = System.Text.Encoding.UTF8.GetString(handler.downloadHandler.data);
        } catch {}
        
        if (handler.responseCode != 403 && handler.responseCode != 200) {
            Debug.LogError($"[domiAnalyze] 등록 실패. ({handler.responseCode}) {body}");            
            return false;
        }

        return true; // 403 은 이미 등록된거라 정상 처리함
    }

    // Ping
    public static void SendPing() {
        var handler = UnityWebRequest.Post(GetURL("ping"), new WWWForm());
        handler.SetRequestHeader("authorization", $"DOMI {SystemInfo.deviceUniqueIdentifier}");

        var operation = handler.SendWebRequest();
        operation.completed += (AsyncOperation e) => {
            string body = "empty";
            try {
                body = System.Text.Encoding.UTF8.GetString(handler.downloadHandler.data);
            } catch {}
            
            if (handler.responseCode != 200) {
                Debug.LogError($"[domiAnalyze] Ping 실패. ({handler.responseCode}) {body}");
            }
            handler.Dispose();
        };
    }

    // scene 전환
    public static async Task<bool> SceneChange(string sceneName) {
        if (!Registered) {
            Debug.LogWarning("[domiAnalyze] 등록이 확실하지 않습니다.");
        }

        Debug.Log($"[domiAnalyze] Scene Change {sceneName}");

        using var handler = UnityWebRequest.Post(GetURL("scene"), $"{{\"scene\":\"{sceneName}\"}}", "application/json");
        handler.SetRequestHeader("authorization", $"DOMI {SystemInfo.deviceUniqueIdentifier}");

        var operation = handler.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        string body = "empty";
        try {
            body = System.Text.Encoding.UTF8.GetString(handler.downloadHandler.data);
        } catch {}

        if (handler.responseCode != 200) {
            Debug.LogError($"[domiAnalyze] Scene 실패. ({handler.responseCode}) {body}");
            return false;
        }

        return true;
    }
}
