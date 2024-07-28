using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SaveSystem
{
    public class TestSave : MonoBehaviour
    {
        public InGameData playerData;

        private void Awake()
        {
            SaveManager.Instance.Save(playerData, "SaveData");  
        }
    }
}