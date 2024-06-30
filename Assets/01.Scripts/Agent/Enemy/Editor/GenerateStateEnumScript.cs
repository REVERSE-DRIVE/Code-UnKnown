using System.Collections.Generic;
using UnityEditor;

namespace EnemyManage
{
    public class GenerateStateEnumScript
    {
        public static string ScriptsFolderPath = "Assets/08.SO/Enemy/EnemySO/EnemyListSO.asset";
        [MenuItem("EnemyManage/Generate State Enum Script")]
        public static void GenerateStateEnum()
        {
            EnemyListSO enemyListSO = AssetDatabase.LoadAssetAtPath<EnemyListSO>(ScriptsFolderPath);
            
            if (enemyListSO == null)
            {
                UnityEngine.Debug.LogError("EnemyListSO.asset not found");
                return;
            }
            
            foreach (EnemySO enemySO in enemyListSO.list)
            {
                enemySO.GenerateStateEnumScript(enemySO.enemyType.ToString());
            }
        }
    }
}