using System;
using System.IO;
using UnityEngine;

public static class DiscordSettingSave
{
    public static DiscordSetting _discordSetting;
    private static string _path;
    public static void SaveSetting()
    {
        _path = Application.dataPath + "/Discord/Save/DiscordSetting.json";
        string json = JsonUtility.ToJson(_discordSetting);
        File.WriteAllText(_path, json);
    }
    
    public static DiscordSetting LoadSetting()
    {
        _path = Application.dataPath + "/Discord/Save/DiscordSetting.json";
        if (File.Exists(_path))
        {
            string json = File.ReadAllText(_path);
            _discordSetting = JsonUtility.FromJson<DiscordSetting>(json);
        }
        else
        {
            _discordSetting = new DiscordSetting();
        }

        return _discordSetting;
    }
}

[Serializable]
public struct DiscordSetting
{
    public string applicationID;
    public string largeImageKey;
    public string smallImageKey;
}
