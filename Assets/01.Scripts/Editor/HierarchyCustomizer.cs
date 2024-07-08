using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

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
            // 오른쪽 끝에 토글버튼
            Rect toggleRect = new Rect(selectionRect.x + selectionRect.width - 25, selectionRect.y - 2.5f, 20, 20);
            obj.SetActive(EditorGUI.Toggle(toggleRect, obj.activeSelf));
            
            
            // 아이콘을 그릴 위치 설정 (오브젝트 이름 오른쪽에 붙도록)
            Rect iconRect = new Rect(selectionRect.x + selectionRect.width - 65, selectionRect.y - 1, 18, 18);

            // 오브젝트에 붙어있는 컴포넌트 가져오기 (Transform 제외)
            Component[] components = obj.GetComponents<Component>();
            int iconCount = 0;

            foreach (Component component in components)
            {
                if (IsComponentIgnored(component)) continue;
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
    
    private static bool IsComponentIgnored(Component component)
    {
        return
            component is Transform ||
            component is CanvasRenderer ||
            component is CanvasScaler ||
            component is GraphicRaycaster ||
            component is AudioListener ||
            component is UniversalAdditionalCameraData ||
            component is StandaloneInputModule;
    }
}