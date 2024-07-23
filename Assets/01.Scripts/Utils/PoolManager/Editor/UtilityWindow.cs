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
    PowerUp,
    Part
}

public partial class UtilityWindow : EditorWindow
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
        EditorStyleSetting();
        PoolSetting();
        
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
            case 3:
                DrawPartItems();
                break;
        }
    }
}
