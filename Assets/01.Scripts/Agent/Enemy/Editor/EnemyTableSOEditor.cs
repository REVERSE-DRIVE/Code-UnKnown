using System;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(EnemyTableSO))]
public class EnemyTableSOEditor : Editor
{
    private SerializedObject _enemyList;
    private string _enemyTypeEnumPath = "Assets/01.Scripts/Agent/Enemy/EnemyCore/EnemyType.cs";
    
    private void OnEnable()
    {
        _enemyList = new SerializedObject(target);
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("id"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyList"), true);
        if (IsDuplicatePrefab())
        {
            EditorGUILayout.HelpBox("Duplicate Prefab", MessageType.Error);
        }

        if (!IsDuplicatePrefab())
        {
            if (GUILayout.Button("Generate Enum"))
            {

                GenerateEnum();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private bool IsDuplicatePrefab()
    {
        for (int i = 0; i < _enemyList.FindProperty("enemyList").arraySize; i++)
        {
            for (int j = i + 1; j < _enemyList.FindProperty("enemyList").arraySize; j++)
            {
                if (_enemyList.FindProperty("enemyList").GetArrayElementAtIndex(i).objectReferenceValue.name ==
                    _enemyList.FindProperty("enemyList").GetArrayElementAtIndex(j).objectReferenceValue.name)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void GenerateEnum()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < _enemyList.FindProperty("enemyList").arraySize; i++)
        {
            string enemyName = _enemyList.FindProperty("enemyList").GetArrayElementAtIndex(i).objectReferenceValue.name;
            sb.Append($"\t{enemyName},\n");
        }
        string code = string.Format(CodeFormat.EnemyTypeFormat, sb);
        System.IO.File.WriteAllText(_enemyTypeEnumPath, code);
        AssetDatabase.Refresh();
    }
}