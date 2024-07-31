using EasySave.Json;

namespace SaveSystem
{
    public class SaveManager : MonoSingleton<SaveManager>
    {
        public void Save<T>(T obj, string fileName)
        {
            EasyToJson.ToJson(obj, fileName, true);
        }
        public T Load<T>(string fileName)
        {
            return EasyToJson.FromJson<T>(fileName);
        }
        
    }
}
