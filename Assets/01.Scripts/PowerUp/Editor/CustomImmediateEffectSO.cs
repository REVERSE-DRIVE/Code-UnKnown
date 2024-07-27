using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ImmediateEffectSO))]
public class CustomImmediateEffectSO : CustomPowerEffectSO
{
    private SerializedProperty targetEffectProp;
    private SerializedProperty effectValueProp;

    protected override void OnEnable()
    {
        base.OnEnable();
        targetEffectProp = serializedObject.FindProperty("effectType");
        effectValueProp = serializedObject.FindProperty("effectValue");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        try
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(targetEffectProp);
            EditorGUILayout.PropertyField(effectValueProp);

            serializedObject.ApplyModifiedProperties();
        }
        catch (Exception e)
        {
            Debug.Log($"exception occur when draw{e.Message}");
        }
    }
}