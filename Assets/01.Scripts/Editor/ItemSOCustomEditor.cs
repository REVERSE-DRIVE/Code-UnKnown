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
        
        private void OnEnable()
        {
            _itemType = serializedObject.FindProperty("itemType");
            _id = serializedObject.FindProperty("id");
            _itemName = serializedObject.FindProperty("itemName");
            _itemIcon = serializedObject.FindProperty("itemIcon");
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
                }
                EditorGUILayout.EndVertical();

            }
            EditorGUILayout.EndHorizontal();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}