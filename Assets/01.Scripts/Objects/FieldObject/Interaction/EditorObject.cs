using ObjectManage;
using UnityEngine;

public class EditorObject : InteractObject
{
    protected override void Start()
    {
        base.Start();
        OnInteractEvent += HandleActiveEditorPanel;
    }

    private void OnEnable()
    {
        FixEditorPanel.leftFixCount = 1;
    }

    private void HandleActiveEditorPanel()
    {
        UIManager.Instance.Open(WindowEnum.Editor);
    }
}
