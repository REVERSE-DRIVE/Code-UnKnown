using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(RandomSizeField), true)]
public class CustomRandomSizeField : PropertyDrawer {
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // 안먹음
        return null;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // EditorGUILayout.PropertyField(property);
        // EditorGUILayout.LabelField(property.displayName);

        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, property.displayName);
        if (!property.isExpanded) return;

        SerializedProperty isRandom = property.FindPropertyRelative("isRandom");
        EditorGUILayout.PropertyField(isRandom, new GUIContent("Random"));

        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        EditorGUIUtility.labelWidth = 60;
        if (isRandom.boolValue) {
            EditorGUILayout.PropertyField(property.FindPropertyRelative("min"), new GUIContent("Min"), GUILayout.MinWidth(10));
            EditorGUILayout.Space(10);
            EditorGUILayout.PropertyField(property.FindPropertyRelative("max"), new GUIContent("Max"), GUILayout.MinWidth(10));
        } else {
            EditorGUILayout.PropertyField(property.FindPropertyRelative("width"), new GUIContent("Width"), GUILayout.MinWidth(10));
            EditorGUILayout.Space(10);
            EditorGUILayout.PropertyField(property.FindPropertyRelative("height"), new GUIContent("Height"), GUILayout.MinWidth(10));
        }
        EditorGUILayout.EndHorizontal();
    }
}