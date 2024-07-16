using UnityEditor;
using UnityEngine;

namespace ItemManage
{
    [CustomEditor(typeof(ItemSO))]
    public class ItemSOCustomEditor : Editor
    {
        private SerializedProperty _itemType;
        private SerializedProperty _id;
        private SerializedProperty _itemName;
        private SerializedProperty _itemIcon;
        private SerializedProperty _resourceRank;
        private SerializedProperty _weaponInfoSO;
        
        private void OnEnable()
        {
            _itemType = serializedObject.FindProperty("itemType");
            _id = serializedObject.FindProperty("id");
            _itemName = serializedObject.FindProperty("itemName");
            _itemIcon = serializedObject.FindProperty("itemIcon");
            _resourceRank = serializedObject.FindProperty("resourceRank");
            _weaponInfoSO = serializedObject.FindProperty("weaponInfoSO");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.BeginHorizontal("HelpBox");
            {
                _itemIcon.objectReferenceValue = EditorGUILayout.ObjectField(GUIContent.none,
                    _itemIcon.objectReferenceValue,
                    typeof(Sprite),
                    false,
                    GUILayout.Width(70));

                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.PropertyField(_itemType);
                    EditorGUILayout.PropertyField(_id);
                    EditorGUI.BeginChangeCheck();
                    string prevName = _itemName.stringValue;
                    EditorGUILayout.DelayedTextField(_itemName);

                    if (EditorGUI.EndChangeCheck())
                    {
                        string assetPath = AssetDatabase.GetAssetPath(target);
                        string newName = $"Item_{_itemName.stringValue}";
                        serializedObject.ApplyModifiedProperties();

                        string msg = AssetDatabase.RenameAsset(assetPath, newName);

                        if (string.IsNullOrEmpty(msg))
                        {
                            target.name = newName;
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                            return;
                        }

                        _itemName.stringValue = prevName;
                    }
                    
                    if (_itemType.enumValueFlag == (int)ItemType.Resource)
                    {
                        EditorGUILayout.PropertyField(_resourceRank);
                    }
                    else if (_itemType.enumValueFlag == (int)ItemType.Weapon)
                    {
                        EditorGUILayout.PropertyField(_weaponInfoSO);
                    }
                }
                EditorGUILayout.EndVertical();

            }
            EditorGUILayout.EndHorizontal();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}