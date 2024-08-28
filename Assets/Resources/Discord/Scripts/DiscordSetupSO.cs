using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Discord.Scripts.Editor
{
    [CreateAssetMenu(menuName = "Discord/DiscordSetupSO")]
    public class DiscordSetupSO : ScriptableObject
    {
        [Tooltip("디스코드 봇의 client ID")]
        public string applicationID;
        [Tooltip("RPC에 뜰 대형 이미지 이름")]
        public string largeImageKey;
        [Tooltip("RPC에 뜰 작은 이미지 이름")]
        public string smallImageKey;
    }
}

