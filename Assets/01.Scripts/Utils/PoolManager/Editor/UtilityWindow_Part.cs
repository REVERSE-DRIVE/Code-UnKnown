using System;
using System.IO;
using PlayerPartsManage;
using UnityEditor;
using UnityEngine;

public partial class UtilityWindow
{
    private readonly string _partDirectory = "Assets/08.SO/Part";
    private PlayerPartTableSO _playerPartTableSO;

    private void InputPartTable()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("Part Table", EditorStyles.boldLabel);
            _playerPartTableSO = (PlayerPartTableSO) EditorGUILayout.ObjectField(_playerPartTableSO, typeof(PlayerPartTableSO), false);
        }
        EditorGUILayout.EndHorizontal();
    }
    private void DrawPartItems()
    {
        InputPartTable();
        
        if (_playerPartTableSO == null)
            return;
        
        CreatePartSODraw();

        #region Part List
    
        GUI.color = Color.white;
        EditorGUILayout.BeginHorizontal();
        {
            #region Scroll View

            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
            {
                EditorGUILayout.LabelField("Part List", EditorStyles.boldLabel);
                EditorGUILayout.Space(3f);
                
                scrollPositions[UtilType.Part] = EditorGUILayout.BeginScrollView(
                    scrollPositions[UtilType.Part], false, true,
                    GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                {
                    DrawPartTable();
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            #endregion
            
            DrawPartInspector();
        }
        EditorGUILayout.EndHorizontal();

        #endregion
    }

    private void DrawPartInspector()
    {
        //인스펙터를 그려줘야 해.
        if (selectedItem[UtilType.Part] != null)
        {
            Vector2 scroll = Vector2.zero;
            EditorGUILayout.BeginScrollView(scroll);
            {
                EditorGUILayout.Space(2f);
                Editor.CreateCachedEditor(
                    selectedItem[UtilType.Part], null, ref _cachedEditor);

                _cachedEditor.OnInspectorGUI();
            }
            EditorGUILayout.EndScrollView();
        }
    }

    private void DrawPartTable()
    {
        foreach (PlayerPartDataSO partData in _playerPartTableSO.playerPartDataSOList)
        {
            GUIStyle style = selectedItem[UtilType.Part] == partData
                ? _selectStyle
                : GUIStyle.none;
            
            EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
            {
                EditorGUILayout.LabelField(partData.partName, GUILayout.Width(240f), GUILayout.Height(40f));
                
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.Space(10f);
                    PartItemDeleteButton(partData);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
            if (partData == null)
                break;
            
            GetRect(partData);
        }
    }

    private void GetRect(PlayerPartDataSO partData)
    {
        Rect rect = GUILayoutUtility.GetLastRect();
        
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            inspectorScroll = Vector2.zero;
            selectedItem[UtilType.Part] = partData;
            Event.current.Use();
        }
    }

    private void PartItemDeleteButton(PlayerPartDataSO partData)
    {
        GUI.color = Color.red;
        if (GUILayout.Button("X", GUILayout.Width(20f)))
        {
            Debug.Log("삭제");
            _playerPartTableSO.playerPartDataSOList.Remove(partData);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(partData));
            EditorUtility.SetDirty(_playerPartTableSO);
            AssetDatabase.SaveAssets();
        }
        GUI.color = Color.white;
    }

    private void CreatePartSODraw()
    {
        EditorGUILayout.LabelField("Part Items", EditorStyles.boldLabel);
        
        EditorGUILayout.BeginHorizontal();
        {
            GUI.color = new Color(0.19f, 0.76f, 0.08f);
            if (GUILayout.Button("New Part Item"))
            {
                if (_playerPartTableSO.partType == PartType.Body)
                {
                    CreatePartSO<PlayerBodyPartDataSO>();
                }
                else if (_playerPartTableSO.partType == PartType.Leg)
                {
                    CreatePartSO<PlayerLegPartDataSO>();
                }

                EditorUtility.SetDirty(_playerPartTableSO);
                AssetDatabase.SaveAssets();
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private void CreatePartSO<T>() where T : PlayerPartDataSO
    {
        T newData = CreateInstance<T>();
        string path = $"{_partDirectory}/{_playerPartTableSO.name}";
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        if (_playerPartTableSO.partType == PartType.Body)
        {
            newData.partType = PartType.Body;
        }
        else if (_playerPartTableSO.partType == PartType.Leg)
        {
            newData.partType = PartType.Leg;
        }
        Guid guid = Guid.NewGuid();
        newData.partName = guid.ToString();
        string fileName = AssetDatabase.GenerateUniqueAssetPath($"{path}/{newData.partType}_{newData.partName}.asset");
        AssetDatabase.CreateAsset(newData, fileName);
        _playerPartTableSO.playerPartDataSOList.Add(newData);
        EditorUtility.SetDirty(_playerPartTableSO);
        AssetDatabase.SaveAssets();
    }
}