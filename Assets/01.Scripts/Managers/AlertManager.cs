using UnityEngine;

public struct AlertContent
{
    public string content;
    public float duration;
}
public class AlertManager : MonoSingleton<AlertManager>
{
    [SerializeField] private AlertPanel _alertPrefab;
    [SerializeField] private RectTransform _generatePointTrm;
    
    public void Alert(AlertContent info)
    {
        AlertPanel _panel = Instantiate(_alertPrefab, _generatePointTrm);
        _panel.Initialize(info.content, info.duration);
    }
}