﻿using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class HierarchyCustomizer
{
    static HierarchyCustomizer()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceId, Rect selectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
        if (obj != null)
        {
            // 아이콘을 그릴 위치 설정 (오브젝트 이름 오른쪽에 붙도록)
            Rect iconRect = new Rect(selectionRect.x + selectionRect.width - 50, selectionRect.y, 18, 18);

            // 오브젝트에 붙어있는 컴포넌트 가져오기 (Transform 제외)
            Component[] components = obj.GetComponents<Component>();
            int iconCount = 0;

            foreach (Component component in components)
            {
                if (component is Transform) continue;
                if (iconCount >= 2) break;

                // 컴포넌트 아이콘 가져오기
                Texture2D icon = AssetPreview.GetMiniThumbnail(component);
                string componentName = component.GetType().Name;
                
                if (icon != null)
                {
                    GUIContent iconContent = new GUIContent(icon, componentName);
                    GUI.Label(iconRect, iconContent);
                    iconRect.x += 20; // 다음 아이콘을 그릴 위치로 이동
                    iconCount++;
                }
            }
        }
    }
}