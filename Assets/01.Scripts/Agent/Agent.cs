using UnityEngine;

public class Agent : MonoBehaviour
{
    public AgentMovement MovementCompo { get; protected set; }
    public AgentVFX VFXCompo { get; protected set; }
    public AgentEffectController EffectCompo { get; protected set; }

    // Agent
    [field: SerializeField] public AgentStat Stat { get; protected set; }
    
    protected virtual void Awake()
    {
        MovementCompo = GetComponent<AgentMovement>();
        VFXCompo = GetComponent<AgentVFX>();
        EffectCompo = GetComponent<AgentEffectController>();

        Stat = Instantiate(Stat);
    }
    
    
}
