using System;
using UnityEditor;
using UnityEngine;

public partial class UtilityWindow
{
    private readonly string _powerUpDirectory = "Assets/08.SO/PowerUp";
    private PowerUpListSO _powerUpTable;
    
    /**
     * Create PowerUp SO Draw
     */
    private void CreatePowerUpSODraw()
    {
        EditorGUILayout.BeginHorizontal();
        {
            GUI.color = new Color(0.19f, 0.76f, 0.08f);
            if (GUILayout.Button("New PowerUp Item"))
            {
                CreatePowerUpSO();

                EditorUtility.SetDirty(_powerUpTable);
                AssetDatabase.SaveAssets();
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    /**
     * Create PowerUp SO
     */
    private void CreatePowerUpSO()
    {
        Guid guid = Guid.NewGuid();
        PowerUpSO newData = CreateInstance<PowerUpSO>();
        newData.code = guid.ToString();
        AssetDatabase.CreateAsset(newData, $"{_powerUpDirectory}/PowerUp_{newData.code}.asset");
        _powerUpTable.list.Add(newData);
    }

    /**
     * PowerUp Item Icon Draw
     */
    private void PowerUpItemIconDraw(PowerUpSO so)
    {
        if (so.icon != null)
        {
            //아이콘 그려준다.
            Texture2D previewTexture = AssetPreview.GetAssetPreview(so.icon);
            GUILayout.Label(
                previewTexture, GUILayout.Width(40f), GUILayout.Height(40f));
        }
    }

    /**
     * PowerUp Item Delete Button
     */
    private void PowerUpItemDeleteButton(PowerUpSO so)
    {
        GUI.color = Color.red;
        if (GUILayout.Button("X", GUILayout.Width(20f)))
        {
            Debug.Log("삭제");
            _powerUpTable.list.Remove(so);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(so));
            EditorUtility.SetDirty(_powerUpTable);
            AssetDatabase.SaveAssets();
        }
        GUI.color = Color.white;
    }

    /**
     * PowerUp Get Rect
     */
    private void GetPowerUpRect(PowerUpSO so)
    {
        //마지막으로 그린 사각형 정보를 알아온다.
        Rect lastRect = GUILayoutUtility.GetLastRect();

        if (Event.current.type == EventType.MouseDown
            && lastRect.Contains(Event.current.mousePosition))
        {
            inspectorScroll = Vector2.zero;
            selectedItem[UtilType.PowerUp] = so;
            Event.current.Use();
        }
    }

    /**
     * Draw PowerUp Inspector
     */
    private void DrawPowerUpInspector()
    {
        //인스펙터를 그려줘야 해.
        if (selectedItem[UtilType.PowerUp] != null)
        {
            Vector2 scroll = Vector2.zero;
            EditorGUILayout.BeginScrollView(scroll);
            {
                EditorGUILayout.Space(2f);
                Editor.CreateCachedEditor(
                    selectedItem[UtilType.PowerUp], null, ref _cachedEditor);

                _cachedEditor.OnInspectorGUI();
            }
            EditorGUILayout.EndScrollView();
        }
    }

    /**
     * Draw PowerUp Table
     */
    private void DrawPowerUpTable()
    {
        foreach (var so in _powerUpTable.list)
        {
            float labelWidth = so.icon != null ? 200f : 240f;
            GUIStyle style = selectedItem[UtilType.PowerUp] == so
                ? _selectStyle
                : GUIStyle.none;

            //한줄 그린다.
            EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
            {
                PowerUpItemIconDraw(so);

                EditorGUILayout.LabelField(
                    so.code, GUILayout.Width(labelWidth), GUILayout.Height(40f));

                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.Space(10f);
                    PowerUpItemDeleteButton(so);
                }
                EditorGUILayout.EndVertical();

            }
            EditorGUILayout.EndHorizontal();
            if (so == null)
                break;

            GetPowerUpRect(so);

        }
    }
    
    /**
     * PowerUp Items Draw
     */
    private void DrawPowerUpItems()
    {
        CreatePowerUpSODraw();
        GUI.color = Color.white;


        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
            {
                EditorGUILayout.LabelField("PowerUp List");
                EditorGUILayout.Space(3f);


                scrollPositions[UtilType.PowerUp] = EditorGUILayout.BeginScrollView(
                    scrollPositions[UtilType.PowerUp],
                    false, true, GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                {
                    DrawPowerUpTable();
                    
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            if (selectedItem[UtilType.PowerUp] != null)
            {
                DrawPowerUpInspector();
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}