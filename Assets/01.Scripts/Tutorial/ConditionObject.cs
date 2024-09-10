using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ConditionObject : MonoBehaviour
{
    public UnityEvent OnActiveEvent;
    protected bool _isActive;

}