using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialFailurePanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI _contentText;
    [SerializeField] private string[] _contents;
    [SerializeField] private float _contentInsertTerm;
    private WaitForSeconds ws;
    private bool _isActive;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Open()
    {
        if (_isActive) return;
        base.Open();
        _isActive = true;

        StartCoroutine(ApplyContentCoroutine());
    }
    
    private IEnumerator ApplyContentCoroutine()
    {
        ws = new WaitForSeconds(_contentInsertTerm);

        _contentText.text = "";

        for (int i = 0; i < _contents.Length; i++)
        {
            yield return ws;
            _contentText.text += $"\n{_contents[i]}";
            
        }

        yield return new WaitForSeconds(3f);
        TutorialManager.Instance.Retry();
    }
    
}
