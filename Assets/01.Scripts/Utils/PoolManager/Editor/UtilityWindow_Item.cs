using System;
using System.IO;
using ItemManage;
using UnityEditor;
using UnityEngine;

public partial class UtilityWindow
{
    private readonly string _itemDirectory = "Assets/08.SO/Item";
    private ItemTableSO _itemTable; 
    
    /**
     * Create Item SO Draw
     */
    private void CreateItemSODraw()
    {
        #region SO Create
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Create Item SO"))
            {
                CreateItemSO();
            }
        }
        EditorGUILayout.EndHorizontal();
        #endregion
    }

    /**
     * GetRect_Item
     */
    private void GetRect(ItemSO itemSO)
    {
        // 마지막으로 그린 사각형 정보를 알아옴
        Rect lastRect = GUILayoutUtility.GetLastRect();

        if (Event.current.type == EventType.MouseDown
            && lastRect.Contains(Event.current.mousePosition)) 
        {
            inspectorScroll = Vector2.zero;
            selectedItem[UtilType.Item] = itemSO;
            Event.current.Use();
        }
    }

    /**
     * Item Delete Button
     */
    private void ItemDeleteButton(ItemSO itemSO)
    {
        #region Delete Button
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.Space(10f);
            GUI.color = Color.red;
            if (GUILayout.Button("X", GUILayout.Width(20f)))
            {
                _itemTable.itemSOList.Remove(itemSO);
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(itemSO));
                EditorUtility.SetDirty(_itemTable);
                AssetDatabase.SaveAssets();
            }
            GUI.color = Color.white;
        }
        EditorGUILayout.EndVertical();
        #endregion
    }

    /**
     * Draw Item Table
     */
    private void DrawItemTable()
    {
        foreach (ItemSO itemSo in _itemTable.itemSOList)
        {
            GUIStyle style = selectedItem[UtilType.Item] == itemSo
                ? _selectStyle
                : GUIStyle.none;
                        
            EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
            {
                EditorGUILayout.LabelField(itemSo.itemName, 
                    GUILayout.Height(40f), GUILayout.Width(240f));

                ItemDeleteButton(itemSo);
            }
            EditorGUILayout.EndHorizontal();
                        
            GetRect(itemSo);
                        
            // 삭제 확인 break;
            if (itemSo == null)
                break;
        }
    }

    /**
     * Item Table Input
     */
    private void InputItemTable()
    {
        #region Item Table Input
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(_itemTable == null ? "Item Table" : _itemTable.name);
            _itemTable = EditorGUILayout.ObjectField("", _itemTable, typeof(ItemTableSO), false) as ItemTableSO;
        }
        EditorGUILayout.EndHorizontal();    
        #endregion
    }
    
    /**
     * Create Item SO
     */
    private void CreateItemSO()
    {
        ItemSO itemSo = CreateInstance<ItemSO>();
        string _path = $"{_itemDirectory}/{_itemTable.name}";
        if (Directory.Exists(_path) == false)
        {
            Directory.CreateDirectory(_path);
        }
        Guid guid = Guid.NewGuid();
        itemSo.itemName = guid.ToString();
        string fileName = AssetDatabase.GenerateUniqueAssetPath($"{_itemDirectory}/{_itemTable.name}/Item_{itemSo.itemName}.asset");
        AssetDatabase.CreateAsset(itemSo, fileName);
        _itemTable.itemSOList.Add(itemSo);
        EditorUtility.SetDirty(_itemTable);
        AssetDatabase.SaveAssets();
    }

    /**
     * Item Inspector Draw
     */
    private void ItemInspectorDraw()
    {
        // 인스펙터 그리기
        if (selectedItem[UtilType.Item] != null)
        {
            inspectorScroll = EditorGUILayout.BeginScrollView(inspectorScroll);
            {
                EditorGUILayout.Space(2f);
                Editor.CreateCachedEditor(
                    selectedItem[UtilType.Item], null, ref _cachedEditor);
                        
                _cachedEditor.OnInspectorGUI();
            }
            EditorGUILayout.EndScrollView();
        }
    }
    
    /**
     * Draw Items
     */
    private void DrawItems()
    {
        InputItemTable();

        if (_itemTable == null)
            return;

        CreateItemSODraw();
        
        #region Item List
        EditorGUILayout.BeginHorizontal();
        {
            #region Scroll View Set
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
            {
                EditorGUILayout.LabelField("Item list");
                EditorGUILayout.Space(3f);
                
                #region Scroll View
                scrollPositions[UtilType.Item] = EditorGUILayout.BeginScrollView
                (scrollPositions[UtilType.Item], false, true,
                    GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                {
                    DrawItemTable();
                }
                EditorGUILayout.EndScrollView();
                #endregion
            }
            EditorGUILayout.EndVertical();
            #endregion
            
            ItemInspectorDraw();
        }
        EditorGUILayout.EndHorizontal();
        #endregion
    }
}