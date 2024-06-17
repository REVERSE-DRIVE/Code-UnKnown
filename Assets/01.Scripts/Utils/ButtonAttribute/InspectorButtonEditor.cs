#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ButtonAttribute
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class InspectorButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MonoBehaviour mono = (MonoBehaviour)target;
            Type type = mono.GetType();
            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (MethodInfo method in methods)
            {
                InspectorButtonAttribute buttonAttribute =
                    (InspectorButtonAttribute)Attribute.GetCustomAttribute(method, typeof(InspectorButtonAttribute));
                if (buttonAttribute != null)
                {
                    string buttonLabel = string.IsNullOrEmpty(buttonAttribute.label)
                        ? method.Name
                        : buttonAttribute.label;
                    bool isToolTipActive = buttonAttribute.isToolTipActive;
                    string toolTip = buttonAttribute.toolTip;
                    int space = buttonAttribute.space;
                    
                    if (space > 0)
                    {
                        EditorGUILayout.Space(space);
                    }

                    if (isToolTipActive)
                    {
                        if (GUILayout.Button(new GUIContent(buttonLabel, toolTip)))
                        {
                            method.Invoke(mono, null);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button(buttonLabel))
                        {
                            method.Invoke(mono, null);
                        }
                    }
                }
            }
        }
    }
}
#endif