using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ItemManage;
using ObjectPooling;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public enum UtilType
{
    Pool,
    Item,
    PowerUp
}

public class UtilityWindow : EditorWindow
{
    private static int toolbarIndex = 0;
    private static Dictionary<UtilType, Vector2> scrollPositions 
        = new Dictionary<UtilType, Vector2>();
    private static Dictionary<UtilType, Object> selectedItem 
        = new Dictionary<UtilType, Object>();
    
    private static Vector2 inspectorScroll = Vector2.zero;

    private string[] _toolbarItemNames;
    private Editor _cachedEditor;
    private Texture2D _selectTexture;
    private GUIStyle _selectStyle;

    #region 각 데이터 테이블 모음
    private readonly string _poolDirectory = "Assets/08.SO/ObjectPool";
    private PoolingTableSO _poolTable;
    
    private readonly string _itemDirectory = "Assets/08.SO/Item";
    private ItemTableSO _itemTable; 
    
    private readonly string _powerUpDirectory = "Assets/08.SO/PowerUp";
    private PowerUpListSO _powerUpTable;
    #endregion
    
    [MenuItem("Util/UtilManager")]
    private static void OpenWindow()
    {
        UtilityWindow window = GetWindow<UtilityWindow>("UtilManager");
        window.minSize = new Vector2(700, 500);
        window.Show();
    }

    private void OnEnable()
    {
        CreateDirectory();
        SetUpUtility();
    }

    private void CreateDirectory()
    {
        if (Directory.Exists(_poolDirectory) == false)
        {
            Directory.CreateDirectory(_poolDirectory);
        }
        if (Directory.Exists(_itemDirectory) == false)
        {
            Directory.CreateDirectory(_itemDirectory);
        }
    }

    private void OnDisable()
    {
        DestroyImmediate(_cachedEditor);
        DestroyImmediate(_selectTexture);
    }

    private void SetUpUtility()
    {
        #region Editor Style Setting
        _selectTexture = new Texture2D(1, 1); // 1픽셀짜리 텍스쳐 그림
        _selectTexture.SetPixel(0, 0, new Color(0.24f, 0.48f, 0.9f, 0.4f));
        _selectTexture.Apply();

        _selectStyle = new GUIStyle();
        _selectStyle.normal.background = _selectTexture;
        
        _selectTexture.hideFlags = HideFlags.DontSave;
        
        _toolbarItemNames = Enum.GetNames(typeof(UtilType));
        foreach (UtilType type in Enum.GetValues(typeof(UtilType)))
        {
            if (scrollPositions.ContainsKey(type) == false)
                scrollPositions[type] = Vector2.zero;
            if (selectedItem.ContainsKey(type) == false)
                selectedItem[type] = null;
        }
        #endregion

        #region Pool Setting
        if (_poolTable == null)
        {
            _poolTable = AssetDatabase.LoadAssetAtPath<PoolingTableSO>
                ($"{_poolDirectory}/table.asset");
            if (_poolTable == null)
            {
                _poolTable = CreateInstance<PoolingTableSO>();
                string fileName = AssetDatabase.GenerateUniqueAssetPath
                    ($"{_poolDirectory}/table.asset");
                
                AssetDatabase.CreateAsset(_poolTable, fileName);
                Debug.Log($"Create Pooling Table at {fileName}");
            }
        }
        #endregion
        
        if (_powerUpTable == null)
        {
            _powerUpTable = CreateAssetTable<PowerUpListSO>(_powerUpDirectory);
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    
    private T CreateAssetTable<T>(string path) where T: ScriptableObject
    {
        T table =  AssetDatabase.LoadAssetAtPath<T>($"{path}/table.asset");
        if(table == null)
        {
            table = ScriptableObject.CreateInstance<T>();
            
            string fileName = AssetDatabase.GenerateUniqueAssetPath($"{path}/table.asset");
            AssetDatabase.CreateAsset(table, fileName);
            Debug.Log($"{typeof(T).Name} Table Created At : {fileName}");
            
        }
        return table;
    }

    private void OnGUI()
    {
        toolbarIndex = GUILayout.Toolbar(toolbarIndex, _toolbarItemNames);
        EditorGUILayout.Space(5f);

        DrawContent(toolbarIndex);
    }

    private void DrawContent(int toolbarIndex)
    {
        switch (toolbarIndex)
        {
            case 0:
                DrawPoolItems();
                break;
            case 1:
                DrawItems();
                break;
            case 2:
                DrawPowerUpItems();
                break;
        }
    }

    private void DrawItems()
    {
        #region Item Table Input
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(_itemTable == null ? "Item Table" : _itemTable.name);
            _itemTable = EditorGUILayout.ObjectField("", _itemTable, typeof(ItemTableSO), false) as ItemTableSO;
        }
        EditorGUILayout.EndHorizontal();    
        #endregion

        if (_itemTable == null)
            return;

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
                    foreach (ItemSO itemSo in _itemTable.itemSOList)
                    {
                        GUIStyle style = selectedItem[UtilType.Item] == itemSo
                            ? _selectStyle
                            : GUIStyle.none;
                        
                        EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
                        {
                            EditorGUILayout.LabelField(itemSo.itemName, 
                                GUILayout.Height(40f), GUILayout.Width(240f));

                            #region Delete Button
                            EditorGUILayout.BeginVertical();
                            {
                                EditorGUILayout.Space(10f);
                                GUI.color = Color.red;
                                if (GUILayout.Button("X", GUILayout.Width(20f)))
                                {
                                    _itemTable.itemSOList.Remove(itemSo);
                                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(itemSo));
                                    EditorUtility.SetDirty(_itemTable);
                                    AssetDatabase.SaveAssets();
                                }
                                GUI.color = Color.white;
                            }
                            EditorGUILayout.EndVertical();
                            #endregion
                        }
                        EditorGUILayout.EndHorizontal();
                        
                        // 마지막으로 그린 사각형 정보를 알아옴
                        Rect lastRect = GUILayoutUtility.GetLastRect();

                        if (Event.current.type == EventType.MouseDown
                            && lastRect.Contains(Event.current.mousePosition)) 
                        {
                            inspectorScroll = Vector2.zero;
                            selectedItem[UtilType.Item] = itemSo;
                            Event.current.Use();
                        }
                        
                        // 삭제 확인 break;
                        if (itemSo == null)
                            break;
                    }
                }
                EditorGUILayout.EndScrollView();
                #endregion
            }
            EditorGUILayout.EndVertical();
            #endregion
            
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
        EditorGUILayout.EndHorizontal();
        #endregion
    }

    private void CreateItemTable()
    {
        _itemTable = CreateInstance<ItemTableSO>();
        string fileName = AssetDatabase.GenerateUniqueAssetPath($"{_itemDirectory}/ItemTable.asset");
        AssetDatabase.CreateAsset(_itemTable, fileName);
        EditorUtility.SetDirty(_itemTable);
        AssetDatabase.SaveAssets();
    }

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

    #region Pooling
    private void DrawPoolItems()
    {
        #region Pool Menu Set
        //상단에 메뉴 2개를 만들자.
        EditorGUILayout.BeginHorizontal();
        {
            GUI.color = new Color(0.19f, 0.76f, 0.08f);
            if(GUILayout.Button("Generate Item"))
            {
                GeneratePoolItem();
            }

            GUI.color = new Color(0.81f, 0.13f, 0.18f);
            if(GUILayout.Button("Generate enum file"))
            {
                GenerateEnumFile();
            }
        }
        EditorGUILayout.EndHorizontal();
        #endregion

        GUI.color = Color.white; //원래 색상으로 복귀.

        EditorGUILayout.BeginHorizontal();
        {
            #region Pooling List
            // 왼쪽 풀리스트 출력부분
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
            {
                EditorGUILayout.LabelField("Pooling list");
                EditorGUILayout.Space(3f);
                
                
                scrollPositions[UtilType.Pool] = EditorGUILayout.BeginScrollView
                    (scrollPositions[UtilType.Pool], false, true, 
                        GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                {
                    foreach (PoolingItemSO item in _poolTable.datas)
                    {
                        // 현재 그릴 item이 선택아이템과 동일하면 스타일 지정
                        GUIStyle style = selectedItem[UtilType.Pool] == item
                            ? _selectStyle
                            : GUIStyle.none;
                        EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
                        {
                            EditorGUILayout.LabelField(item.enumName, 
                                GUILayout.Height(40f), GUILayout.Width(240f));

                            #region Delete Button
                            EditorGUILayout.BeginVertical();
                            {
                                EditorGUILayout.Space(10f);
                                GUI.color = Color.red;
                                if (GUILayout.Button("X", GUILayout.Width(20f)))
                                {
                                    _poolTable.datas.Remove(item);
                                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(item));
                                    EditorUtility.SetDirty(_poolTable);
                                    AssetDatabase.SaveAssets();
                                }
                                GUI.color = Color.white;
                            }
                            EditorGUILayout.EndVertical();
                            #endregion
                            
                        }
                        EditorGUILayout.EndHorizontal();
                        
                        // 마지막으로 그린 사각형 정보를 알아옴
                        Rect lastRect = GUILayoutUtility.GetLastRect();

                        if (Event.current.type == EventType.MouseDown
                            && lastRect.Contains(Event.current.mousePosition)) 
                        {
                            inspectorScroll = Vector2.zero;
                            selectedItem[UtilType.Pool] = item;
                            Event.current.Use();
                        }
                        
                        // 삭제 확인 break;
                        if (item == null)
                            break;
                    }
                    // end of foreach
                    
                }
                EditorGUILayout.EndScrollView();
                
                
            }
            EditorGUILayout.EndVertical();
            #endregion
            
            // 인스펙터 그리기
            if (selectedItem[UtilType.Pool] != null)
            {
                inspectorScroll = EditorGUILayout.BeginScrollView(inspectorScroll);
                {
                    EditorGUILayout.Space(2f);
                    Editor.CreateCachedEditor(
                        selectedItem[UtilType.Pool], null, ref _cachedEditor);
                        
                    _cachedEditor.OnInspectorGUI();
                }
                EditorGUILayout.EndScrollView();
            }
        }
        EditorGUILayout.EndHorizontal();
    }
    
    private void GeneratePoolItem()
    {
        Guid guid = Guid.NewGuid(); // 고유한 문자열 키 반환
        
        PoolingItemSO item = CreateInstance<PoolingItemSO>(); // 메모리에만 생성
        item.enumName = guid.ToString();
        
        AssetDatabase.CreateAsset(item, $"{_poolDirectory}/PoolItems/Pool_{item.enumName}.asset");
        _poolTable.datas.Add(item);
        
        EditorUtility.SetDirty(_poolTable);
        AssetDatabase.SaveAssets();
    }

    private void GenerateEnumFile()
    {
        StringBuilder codeBuilder = new StringBuilder();

        foreach (PoolingItemSO item in _poolTable.datas)
        {
            codeBuilder.Append(item.enumName);
            codeBuilder.Append(",");
        }
        
        string code = string.Format(CodeFormat.PoolingTypeFormat, codeBuilder.ToString());
        
        string path = $"{Application.dataPath}/01.Scripts/Utils/PoolManager/Core/ObjectPool/PoolingType.cs";
        
        
        File.WriteAllText(path, code);
        AssetDatabase.Refresh();
    }
    #endregion

    #region PowerUp

     private void DrawPowerUpItems()
    {
        EditorGUILayout.BeginHorizontal();
        {
            GUI.color = new Color(0.19f, 0.76f, 0.08f);
            if (GUILayout.Button("New PowerUp Item"))
            {
                Guid guid = Guid.NewGuid();
                PowerUpSO newData = CreateInstance<PowerUpSO>();
                newData.code = guid.ToString();
                AssetDatabase.CreateAsset(newData, $"{_powerUpDirectory}/PowerUp_{newData.code}.asset");
                _powerUpTable.list.Add(newData);

                EditorUtility.SetDirty(_powerUpTable);
                AssetDatabase.SaveAssets();
            }
        }
        EditorGUILayout.EndHorizontal();
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

                    foreach (var so in _powerUpTable.list)
                    {
                        float labelWidth = so.icon != null ? 200f : 240f;
                        GUIStyle style = selectedItem[UtilType.PowerUp] == so
                            ? _selectStyle
                            : GUIStyle.none;

                        //한줄 그린다.
                        EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
                        {
                            if (so.icon != null)
                            {
                                //아이콘 그려준다.
                                Texture2D previewTexture = AssetPreview.GetAssetPreview(so.icon);
                                GUILayout.Label(
                                    previewTexture, GUILayout.Width(40f), GUILayout.Height(40f));
                            }

                            EditorGUILayout.LabelField(
                                so.code, GUILayout.Width(labelWidth), GUILayout.Height(40f));

                            EditorGUILayout.BeginVertical();
                            {
                                EditorGUILayout.Space(10f);
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
                            EditorGUILayout.EndVertical();

                        }
                        EditorGUILayout.EndHorizontal();
                        if (so == null)
                            break;

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
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            if (selectedItem[UtilType.PowerUp] != null)
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
        }
        EditorGUILayout.EndHorizontal();
    }

    #endregion
}
