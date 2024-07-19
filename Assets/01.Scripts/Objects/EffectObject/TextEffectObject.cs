using ObjectPooling;
using TMPro;
using UnityEngine;

public struct TextContent
{
    public string content;
    public Color color;
    public float lifeTime;
}

public class TextEffectObject : MonoBehaviour, ILifeTimeLimited, IPoolable
{
    [field:SerializeField] public PoolingType type { get; set; }
    public GameObject ObjectPrefab => gameObject;
    
    [SerializeField] private TextMeshPro _tmp;
    [SerializeField] private float _lifeTime;
    [SerializeField]
    private float _currentLifeTime;
    private bool _isActive;

    float ILifeTimeLimited.CurrentLifeTime
    {
        get => _currentLifeTime;
        set => _currentLifeTime = value;
    }

    private void Update()
    {
        _currentLifeTime += Time.deltaTime;
    }

    public void Initialize(TextContent content)
    {
        _tmp.text = content.content;
        _tmp.color = content.color;
        _lifeTime = content.lifeTime;
        
        
    }

    public void Play()
    {
    }

    public void CheckDie()
    {
        if (_currentLifeTime > _lifeTime)
        {
            
        }
    }

    public void HandleDie()
    {
        PoolingManager.Instance.Push(this);
    }

    
    public void ResetItem()
    {
        
    }
}