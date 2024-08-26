using UnityEditor;
using UnityEngine;

namespace Discord.Scripts.Editor
{
    [CustomEditor(typeof(DiscordSetupSO))]
    public class DiscordSetUpCustomEditor : UnityEditor.Editor
    {
        private SerializedProperty _applicationID;
        private SerializedProperty _largeImageKey;
        private SerializedProperty _smallImageKey;
        
        private GUIStyle _style;
        
        private void OnEnable()
        {
            _applicationID = serializedObject.FindProperty("applicationID");
            _largeImageKey = serializedObject.FindProperty("largeImageKey");
            _smallImageKey = serializedObject.FindProperty("smallImageKey");
        }
        
        public override void OnInspectorGUI()
        {
            SetStyle();
            serializedObject.Update();
            EditorGUILayout.LabelField("Discord Rich Presence Setup", _style);
            EditorGUILayout.Space(10);
            EditorGUILayout.PropertyField(_applicationID);
            EditorGUILayout.PropertyField(_largeImageKey);
            EditorGUILayout.PropertyField(_smallImageKey);

            if (GUILayout.Button("Setup Discord Rich Presence"))
            {
                DiscordSettingSave._discordSetting = new DiscordSetting
                {
                    applicationID = _applicationID.stringValue,
                    largeImageKey = _largeImageKey.stringValue,
                    smallImageKey = _smallImageKey.stringValue
                };
                DiscordSettingSave.SaveSetting();
                DiscordController.SetUp(_applicationID.stringValue, _largeImageKey.stringValue, _smallImageKey.stringValue);
            }

            if (EditorGUILayout.LinkButton("How To Setting"))
            {
                Application.OpenURL("https://youtu.be/WRKQOaIrqXg?si=3O4MoMf-VHDH96Ny&t=22");
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void SetStyle()
        {
            _style = new GUIStyle();
            _style.fontSize = 20;
            _style.fontStyle = FontStyle.Bold;
            _style.alignment = TextAnchor.MiddleCenter;
            _style.normal.textColor = Color.white;
        }
    }

}