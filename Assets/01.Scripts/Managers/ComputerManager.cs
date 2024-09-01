using System;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class ComputerManager : MonoSingleton<ComputerManager>
{
    [SerializeField] private Light2D _Light;
    public event Action<int, int> OnInfectionLevelChangedEvent;
    public event Action OnInfectionMaxEvent;
    public int InfectionLevel { get; private set; } = 0;

    private List<ErrorPanelObject> _errorPanels = new List<ErrorPanelObject>();
    // 감염도 알림을 어떻게 띄울 것인가?
    // 팝업을 통해 띄울 것인가?

    private void Awake()
    {
        
    }

    [ContextMenu("DebugInfect")]
    public void DebugInfect()
    {
        Infect(80);
    }

    public void Infect()
    {
        Infect(Random.Range(3,6));
    }
    
    public void Infect(int amount)
    {
        if (amount < 0) return;
        SetInfect(InfectionLevel + amount);
    }

    public void SetInfect(int value) {
        if (value < 0) return;
        
        print($"Changed InfectionLevel = {value}");
        int before = InfectionLevel;
        InfectionLevel = value;
        CheckMaxInfection();
        OnInfectionLevelChangedEvent?.Invoke(before, InfectionLevel);
    }

    private void CheckMaxInfection()
    {
        if (InfectionLevel < 100) return;
        InfectionLevel = 100;
        OnInfectionMaxEvent?.Invoke();
    }

    public void GenerateErrorPanels(Vector2 center, float radius, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 position = (Random.insideUnitCircle * radius) + center;
            GenerateErrorPanel(position);
            
        }
    }
    private void GenerateErrorPanel(Vector2 pos)
    {
         ErrorPanelObject errorPanel = PoolingManager.Instance.Pop(PoolingType.ErrorPanel) as ErrorPanelObject;
         _errorPanels.Add(errorPanel);
         errorPanel.Initialize(pos);
    }
    

}