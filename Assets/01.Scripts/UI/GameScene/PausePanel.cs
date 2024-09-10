using DG.Tweening;
using UnityEngine;

public class PausePanel : UIPanel
{
    public override void Open()
    {
        base.Open();
        Time.timeScale = 0; 
        UIManager.Instance.isPause = true;
    }

    public override void Close()
    {
        
        if (!UIManager.Instance.isEffectSelecting)
            Time.timeScale = 1;
        
        base.Close();
        UIManager.Instance.isPause = false;
    }
    
}
