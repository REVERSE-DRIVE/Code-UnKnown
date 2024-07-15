using UnityEngine;
using UnityEngine.UI;

public class LockZipFileObject : ZipFileObject
{
    [SerializeField] private Image _lockPanel;
    [SerializeField] private string _password;
    private bool _isLocked = true;
    protected override void HandleInteract()
    {
        if (_isLocked)
        {
            _lockPanel.gameObject.SetActive(true);
        }
        else
        {
            base.HandleInteract();
        }
    }
    
    private bool CheckPassword(string password)
    {
        return _password == password;
    }
}