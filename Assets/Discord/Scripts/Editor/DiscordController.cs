using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using System;

namespace Discord.Scripts.Editor
{
    [InitializeOnLoad]
    public class DiscordController
    {
        public static Discord discord;
        private static string projectName => Application.productName;
        private static string version => Application.unityVersion;
        private static RuntimePlatform platform => Application.platform;
        private static string activeSceneName => EditorSceneManager.GetActiveScene().name;
        private static long lastTimestamp;

        private static string applicationId = "1277410863736688680";
        private static string largeImageKey = "icon";
        private static string smallImageKey = "reverse";
        
        private static DiscordSetting _discordSetting;

        static DiscordController()
        {
            DelayInit();
        }

        private static async void DelayInit(int delay = 1000)
        {
            await Task.Delay(delay);
            SetUpDiscord();
        }

        public static void SetUp(string applicationId, string largeImageKey, string smallImageKey)
        {
            DiscordController.applicationId = applicationId;
            DiscordController.largeImageKey = largeImageKey;
            DiscordController.smallImageKey = smallImageKey;
            
            DiscordSaveGet();
        }

        private static void SetUpDiscord()
        {
            DiscordSaveGet();
            discord = new Discord(long.Parse(applicationId), (ulong)CreateFlags.Default);
            lastTimestamp = GetTimestamp();
            UpdateActivity();

            EditorApplication.update += EditorUpdate;
            EditorSceneManager.sceneOpened += SceneOpened;
        }

        private static void DiscordSaveGet()
        {
            _discordSetting = DiscordSettingSave.LoadSetting();
            Debug.Log("Discord Rich Presence Setup Complete");
            Debug.Log($"Application ID: {_discordSetting.applicationID}");
            Debug.Log($"Large Image Key: {_discordSetting.largeImageKey}");
            Debug.Log($"Small Image Key: {_discordSetting.smallImageKey}");
            
            DiscordController.applicationId = _discordSetting.applicationID;
            DiscordController.largeImageKey = _discordSetting.largeImageKey;
            DiscordController.smallImageKey = _discordSetting.smallImageKey;
        }

        private static long GetTimestamp()
        {
            long unixTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            return unixTimeStamp;
        }

        private static void EditorUpdate()
        {
            discord.RunCallbacks();
        }

        private static void UpdateActivity()
        {
            ActivityManager activityManager = discord.GetActivityManager();
            Activity activity = new Activity
            {
                Details = $"Editing {projectName}",
                State = $"{activeSceneName} | {platform}",
                Timestamps =
                {
                    Start = lastTimestamp
                },
                Assets =
                {
                    LargeImage = largeImageKey,
                    LargeText = version,
                    SmallImage = smallImageKey,
                    SmallText = version
                }
            };

            activityManager.UpdateActivity(activity, result =>
            {
                if (result == Result.Ok)
                {
                    Debug.Log("Discord Rich Presence Updated");
                }
                else
                {
                    Debug.LogError("Discord Rich Presence Update Failed");
                }
            });
        }

        private static void SceneOpened(Scene scene, OpenSceneMode mode)
        {
            UpdateActivity();
        }
    }
}
