using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace EnemyManage
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "SO/Enemy/EnemySO")]
    public class EnemySO : ScriptableObject
    {
        private const string ScriptsFolderPath = "Assets/01.Scripts/Agent/Enemy/EnemyType/EnemyStateType";
        public int id;
        public EnemyType enemyType;

#if UNITY_EDITOR
        private void OnValidate()
        {
            id = (int)enemyType + 1001;
            string assetPath = AssetDatabase.GetAssetPath(this);
            AssetDatabase.RenameAsset(assetPath, $"{enemyType}SO");

            EditorApplication.delayCall += SaveAssets;
        }

        private void SaveAssets()
        {
            EditorApplication.delayCall -= SaveAssets;
            AssetDatabase.SaveAssets();
        }
        
        public void GenerateStateEnumScript(string enumName)
        {
            string scriptName = $"{enumName}StateEnum";
            string scriptPath = Path.Combine(ScriptsFolderPath, $"{scriptName}.cs");

            if (!File.Exists(scriptPath))
            {
                string scriptContent = $"namespace EnemyManage\n{{\n    public enum {scriptName}\n    {{\n        Idle,\n        Attack,\n        Dead\n    }}\n}}";
                File.WriteAllText(scriptPath, scriptContent);

                AssetDatabase.Refresh();
                Debug.Log($"Generated script at: {scriptPath}");
            }
            else
            {
                Debug.LogWarning($"Script {scriptName}.cs already exists at {scriptPath}");
            }
        }
#endif
    }
}