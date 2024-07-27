using PlayerPartsManage;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerLegPartDataSO))]
public class PlayerLegPartDataCustomEditor : Editor
{
    private SerializedProperty id;
    private SerializedProperty partName;
    private SerializedProperty description;
    private SerializedProperty partPrefab;
    private SerializedProperty legPartSprites;
    
    private void OnEnable()
    {
        id = serializedObject.FindProperty("id");
        partName = serializedObject.FindProperty("partName");
        description = serializedObject.FindProperty("description");
        partPrefab = serializedObject.FindProperty("partPrefab");
        legPartSprites = serializedObject.FindProperty("legPartSprites");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.BeginHorizontal("helpbox");
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("Leg Part Data", EditorStyles.boldLabel);
                EditorGUILayout.Space(10);
                EditorGUILayout.PropertyField(id);

                #region PartName
                EditorGUI.BeginChangeCheck();
                string part = partName.stringValue;
                EditorGUILayout.DelayedTextField(partName);
                
                if (EditorGUI.EndChangeCheck())
                {
                    string assetPath = AssetDatabase.GetAssetPath(target);
                    string newName = $"Leg_{partName.stringValue}";
                    
                    serializedObject.ApplyModifiedProperties();
                    string msg = AssetDatabase.RenameAsset(assetPath, newName);
                    if (string.IsNullOrEmpty(msg))
                    {
                        target.name = newName;
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        return;
                    }
                    partName.stringValue = part;
                }
                #endregion

                EditorGUILayout.PropertyField(description);
                EditorGUILayout.PropertyField(partPrefab);
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField("Leg Part Sprites");
                EditorGUILayout.PropertyField(legPartSprites, true);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
        
        serializedObject.ApplyModifiedProperties();
    }
}