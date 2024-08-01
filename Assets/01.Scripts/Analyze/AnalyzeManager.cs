using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class AnalyzeManager : MonoBehaviour
{
    static readonly string host = "172.30.1.10";
    static readonly ushort port = 3000;

    static string GetURL(string path) => $"http://{host}:{port}/game/{path}";

    static bool registered = false; // 등록됨?

    
    // 등록하기
    public async Task<bool> RegisterDevice() {
        // 보낼 데이터
        Dictionary<string, string> form = new()
        {
            { "uuid", SystemInfo.deviceUniqueIdentifier },
            { "model", SystemInfo.deviceModel },
            { "platform", Application.platform.ToString() },
        };

        using var handler = UnityWebRequest.Post(GetURL("register"), form);
        handler.SetRequestHeader("Content-Type", "application/json");

        var operation = handler.SendWebRequest();
        
        while (!operation.isDone)
            await Task.Yield();

        if (handler.result != UnityWebRequest.Result.Success) {
            Debug.LogError($"[domiAnalyze] 등록 실패. {handler.result}");            
            return false;
        }

        string body = System.Text.Encoding.UTF8.GetString(handler.downloadHandler.data);
        if (handler.responseCode != 403 && handler.responseCode != 200) {
            Debug.LogError($"[domiAnalyze] 등록 실패. ({handler.responseCode}) {body}");            
            return false;
        }

        return true; // 403 은 이미 등록된거라 정상 처리함
    }
}
