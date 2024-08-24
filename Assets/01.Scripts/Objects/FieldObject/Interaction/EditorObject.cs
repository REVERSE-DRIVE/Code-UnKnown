using System.Collections;
using System.Collections.Generic;
using ObjectManage;
using UnityEngine;

public class EditorObject : InteractObject
{
    protected override void Start()
    {
        base.Start();
        OnInteractEvent += HandleActiveEditorPanel;
    }

    private void HandleActiveEditorPanel()
    {
        UIManager.Instance.Open(WindowEnum.Editor);
    }
}
