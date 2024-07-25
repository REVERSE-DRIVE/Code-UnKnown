using QuestManage;
using UnityEditor;
using UnityEngine;

public partial class UtilityWindow
{
    private readonly string _questDirectory = "Assets/08.SO/Quest";
    private QuestListSO _questTable;


    private void DrawQuestItems()
    {
        InputQuestTable();
        
        if (_questTable == null)
            return;
        
        CreateQuestSODraw();
    }

    private void CreateQuestSODraw()
    {
        EditorGUILayout.LabelField("Quest Items", EditorStyles.boldLabel);
        
        EditorGUILayout.BeginHorizontal();
        {
            GUI.color = new Color(0.19f, 0.76f, 0.08f);
            if (GUILayout.Button("New Quest Item"))
            {

                EditorUtility.SetDirty(_playerPartTableSO);
                AssetDatabase.SaveAssets();
            }
        }
        EditorGUILayout.EndHorizontal();
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