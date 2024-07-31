using PlayerPartsManage;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerBodyPartDataSO))]
public class PlayerBodyPartDataCustomEditor : Editor
{
    private SerializedProperty _id;
    private SerializedProperty _partName;
    private SerializedProperty _description;
    private SerializedProperty _partPrefab;
    private SerializedProperty _bodyPartSprite;
    
    private void OnEnable()
    {
        _id = serializedObject.FindProperty("id");
        _partName = serializedObject.FindProperty("partName");
        _description = serializedObject.FindProperty("description");
        _partPrefab = serializedObject.FindProperty("partPrefab");
        _bodyPartSprite = serializedObject.FindProperty("bodyPartSprite");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.BeginHorizontal("helpbox");
        {

            _bodyPartSprite.objectReferenceValue = EditorGUILayout.ObjectField(GUIContent.none,
                _bodyPartSprite.objectReferenceValue,
                typeof(Sprite),
                false,
                GUILayout.Width(65));
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("Body Part Data", EditorStyles.boldLabel);
                EditorGUILayout.Space(10);
                EditorGUILayout.PropertyField(_id);

                #region PartName
                EditorGUI.BeginChangeCheck();
                string part = _partName.stringValue;
                EditorGUILayout.DelayedTextField(_partName);
                
                if (EditorGUI.EndChangeCheck())
                {
                    string assetPath = AssetDatabase.GetAssetPath(target);
                    string newName = $"Body_{_partName.stringValue}";
                    
                    serializedObject.ApplyModifiedProperties();
                    string msg = AssetDatabase.RenameAsset(assetPath, newName);
                    if (string.IsNullOrEmpty(msg))
                    {
                        target.name = newName;
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        return;
                    }
                    _partName.stringValue = part;
                    
                }
                #endregion
                EditorGUILayout.PropertyField(_description);
                EditorGUILayout.PropertyField(_partPrefab);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
    
}