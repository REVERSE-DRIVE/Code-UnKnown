using System;
using ObjectManage;
using QuestManage;
using UnityEngine.SceneManagement;

public class ExitObject : ClickableObject
{
    private void Awake()
    {
        OnClickEvent += HandleTitleMoveScene;
    }

    private void HandleTitleMoveScene()
    {
        SceneManager.LoadScene("TitleScene");
        Destroy(QuestManager.Instance.gameObject);
        Destroy(PlayerPartManager.Instance.gameObject);
    }
}