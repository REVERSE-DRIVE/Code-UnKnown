using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzeErrorDetect : MonoBehaviour
{
    Dictionary<string, Dictionary<string, Dictionary<string, uint>>> errorStack;
    float timer = 0;

    private void Awake() {
        errorStack = new();
        Application.logMessageReceived += HandleException;
    }

    private void OnDestroy() {
        Application.logMessageReceived -= HandleException;
    }

    private void Update() {
        if (errorStack.Count == 0) return;
        timer += Time.unscaledDeltaTime;
        
        if (timer > 10) {
            timer = 0;
            
            List<AnalyzeExceptionData> exceptions = new();

            foreach (var item in errorStack) {
                foreach (var item2 in item.Value) {
                    foreach (var item3 in item2.Value) {
                        exceptions.Add(new () {
                            type = item.Key,
                            function = item2.Key,
                            stack = item3.Key,
                            count = item3.Value
                        });
                    }
                }
            }

            if (exceptions.Count == 0) return;
            AnalyzeManager.SendExceptions(exceptions);
        }
    }

    private void HandleException(string condition, string stackTrace, LogType type)
    {
        if (type != LogType.Exception) return;

        string errorType = condition.Substring(0, condition.IndexOf(':'));
        string firstFunc = stackTrace.Split(' ')[0];
        string message = $"{condition}\n{stackTrace}";

        if (!errorStack.TryGetValue(errorType, out var funcObj)) {
            errorStack[errorType] = funcObj = new();
        }
        if (!funcObj.TryGetValue(firstFunc, out var stackObj)) {
            funcObj[firstFunc] = stackObj = new();
        }
        if (!stackObj.TryGetValue(message, out var count)) {
            stackObj[message] = count = 0;
        }
        
        stackObj[message] = count + 1;
        print($"{errorType} / {firstFunc}");
    }
}
