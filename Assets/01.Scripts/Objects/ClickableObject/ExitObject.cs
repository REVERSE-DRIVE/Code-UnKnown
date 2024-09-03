using System;
using ObjectManage;
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
    }
}