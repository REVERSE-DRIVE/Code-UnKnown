using System;
using System.IO;
using QuestManage;
using UnityEditor;
using UnityEngine;

public partial class UtilityWindow
{
    private readonly string _questDirectory = "Assets/08.SO/Quest";
    private QuestListSO _questTable;
    private string _questType;


    private void DrawQuestItems()
    {
        InputQuestTable();
        
        if (_questTable == null)
            return;
        
        CreateQuestSODraw();

        #region Quest List
        GUI.color = Color.white;
        EditorGUILayout.BeginHorizontal();
        {
            #region Scroll View

            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
            {
                EditorGUILayout.LabelField("Quest List", EditorStyles.boldLabel);
                EditorGUILayout.Space(3f);
                
                scrollPositions[UtilType.Quest] = EditorGUILayout.BeginScrollView(
                    scrollPositions[UtilType.Quest], false, true,
                    GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                {
                    DrawQuestTable();
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            #endregion
            
            DrawQuestInspector();
        }
        EditorGUILayout.EndHorizontal();
        #endregion
    }

    private void DrawQuestInspector()
    {
        if (selectedItem[UtilType.Quest] != null)
        {
            Vector2 scroll = Vector2.zero;
            EditorGUILayout.BeginScrollView(scroll);
            {
                EditorGUILayout.Space(2f);
                Editor.CreateCachedEditor(
                    selectedItem[UtilType.Quest], null, ref _cachedEditor);

                _cachedEditor.OnInspectorGUI();
            }
            EditorGUILayout.EndScrollView();
        }
    }

    private void DrawQuestTable()
    {
        foreach (QuestSO data in _questTable.questList)
        {
            GUIStyle style = selectedItem[UtilType.Quest] == data
                ? _selectStyle
                : GUIStyle.none;
            
            EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
            {
                string label = data.title == string.Empty ? data.id.ToString() : data.title;
                EditorGUILayout.LabelField(label, GUILayout.Width(240f), GUILayout.Height(40f));
                
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.Space(10f);
                    QuestDeleteButton(data);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
            
            if (data == null)
                break;
            
            GetQuestRect(data);
        }
    }

    private void GetQuestRect(QuestSO data)
    {
        Rect rect = GUILayoutUtility.GetLastRect();
        
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            inspectorScroll = Vector2.zero;
            selectedItem[UtilType.Quest] = data;
            Event.current.Use();
        }
    }

    private void QuestDeleteButton(QuestSO data)
    {
        GUI.color = Color.red;
        if (GUILayout.Button("X", GUILayout.Width(20f)))
        {
            _questTable.questList.Remove(data);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(data));
            EditorUtility.SetDirty(_questTable);
            AssetDatabase.SaveAssets();
        }
        GUI.color = Color.white;
    }

    private void CreateQuestSODraw()
    {
        EditorGUILayout.LabelField("Quest Items", EditorStyles.boldLabel);
        
        EditorGUILayout.BeginHorizontal();
        {
            // Quest Type 입력
            _questType = EditorGUILayout.TextField("Quest Type", _questType);
            GUI.color = new Color(0.19f, 0.76f, 0.08f);
            if (GUILayout.Button("New Quest Item"))
            {
                if (_questType == string.Empty)
                {
                    Debug.LogError("Quest Type을 입력해주세요.");
                    EditorGUILayout.EndHorizontal();
                    return;
                }
                Debug.Log(_questType);
                CreateQuestSO();
                EditorUtility.SetDirty(_questTable);
                AssetDatabase.SaveAssets();
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private void CreateQuestSO()
    {
        Type type = Type.GetType($"QuestManage.{_questType}QuestSO, Assembly-CSharp");
        if (type == null)
        {
            Debug.LogError("Quest Type을 확인해주세요.");
            return;
        }
        
        QuestSO newData = (QuestSO) CreateInstance(type);
        string path = $"{_questDirectory}/{_questTable.name}";
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        newData.id = _questTable.questList.Count;
        AssetDatabase.CreateAsset(newData, $"{path}/Quest_{newData.id}.asset");
        _questTable.questList.Add(newData);
        
        EditorUtility.SetDirty(_questTable);
        AssetDatabase.SaveAssets();
    }

    private void InputQuestTable()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("Quest Table", GUILayout.Width(100f));
            _questTable = (QuestListSO) EditorGUILayout.ObjectField(_questTable, typeof(QuestListSO), false);
        }
        EditorGUILayout.EndHorizontal();
    }
}