using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ComputerManager : MonoSingleton<ComputerManager>
{
    [SerializeField] private Light2D _Light;
    public event Action<int, int> OnInfectionLevelChangedEvent;
    public event Action OnInfectionMaxEvent;
    public int InfectionLevel { get; private set; } = 0;
    // 감염도 알림을 어떻게 띄울 것인가?
    // 팝업을 통해 띄울 것인가?

    private void Awake()
    {
        
    }

    public void Infect(int amount)
    {
        if (amount < 0) return;
        int before = InfectionLevel;
        InfectionLevel += amount;
        CheckMaxInfection();
        OnInfectionLevelChangedEvent?.Invoke(before, InfectionLevel);
    }

    private void CheckMaxInfection()
    {
        if (InfectionLevel < 100) return;
        InfectionLevel = 100;
        OnInfectionMaxEvent?.Invoke();
    }
    

}