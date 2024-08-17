using System.Collections.Generic;
using EasySave.Json;

namespace SaveSystem
{
    public class SaveManager : MonoSingleton<SaveManager>
    {
        public void Save<T>(T obj, string fileName)
        {
            EasyToJson.ToJson(obj, fileName, true);
        }

        public void SaveToList<T>(List<T> obj, string fileName)
        {
            EasyToJson.ListToJson(obj, fileName, true);
        }
        
        public T Load<T>(string fileName)
        {
            return EasyToJson.FromJson<T>(fileName);
        }
        
        public List<T> LoadFromList<T>(string fileName)
        {
            return EasyToJson.ListFromJson<T>(fileName);
        }
        
        
    }
}
