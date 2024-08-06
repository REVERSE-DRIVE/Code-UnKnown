using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class MainInputManager : MonoSingleton<MainInputManager>
{
    public MainInput MainInput { get; private set; }

    private void Awake()
    {
        MainInput = new MainInput();
        MainInput.Enable();
        MainInput.Player.TestHold.performed += Hold;

        // 초기 설정
        //SetHoldTime(0.5f); // 기본 hold 설정
    }

    private void Hold(InputAction.CallbackContext context)
    {
        Debug.Log("Hold duration: " + context.duration);
    }

    // HoldInteraction의 duration과 pressPoint를 동적으로 설정하는 메서드
    // public void SetHoldTime(float pressPoint)
    // {
    //     // 바인딩의 인터랙션 문자열 생성
    //     string newInteraction = $"hold(pressPoint={pressPoint})";
    //
    //     var bindingIndex = MainInput.Player.TestHold.GetBindingIndexForControl(MainInput.Player.TestHold.controls[0]);
    //     InputBinding
    //     MainInput.Player.TestHold.ApplyBindingOverride(bindingIndex, new InputBinding
    //     {
    //         path = MainInput.Player.TestHold.bindings[bindingIndex].path,
    //         interactions = newInteraction
    //     });
    //
    //     Debug.Log("Press Point Set: " + pressPoint);
    //     Debug.Log("New Interaction: " + newInteraction);
    //     
    //     Debug.Log("Current Interaction: " + MainInput.Player.TestHold.interactions);
    // }
}