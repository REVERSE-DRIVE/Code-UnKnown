using System;
using UnityEngine;
using UnityEngine.UI;

namespace TitleScene
{
    public class PlayContinuePanel : MonoBehaviour
    {
        [SerializeField] private Button _confirmBtn, _cancelBtn;

        private void Awake()
        {
            
        }

        private void HandleConfirm()
        {
            // 계속해서 플레이하도록 인게임으로 이동
        }

        private void HandleCancel()
        {
            // 플레이 기록을 삭제하고 의뢰를 포기신청.
        }
    }
}