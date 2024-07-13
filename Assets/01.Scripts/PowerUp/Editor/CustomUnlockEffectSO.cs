
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnLockSkillEffectSO))]
public class CustomUnlockEffectSO : CustomPowerEffectSO
{ 
    private SerializedProperty unlockSkilProp;

    protected override void OnEnable()
    {
        base.OnEnable();
        unlockSkilProp = serializedObject.FindProperty("unLockSkill");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        try
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(unlockSkilProp);
            serializedObject.ApplyModifiedProperties();
        }   
        catch (Exception e)
        {
            Debug.LogWarning($"error occur when drawer : {e.Message}");
        }
    }
    
}