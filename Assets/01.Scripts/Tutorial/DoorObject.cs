using UnityEngine;
using UnityEngine.Events;

public class DoorObject : MonoBehaviour
{
    public UnityEvent OnDoorOpenEvent;
    
    public void OpenDoor()
    {
        OnDoorOpenEvent?.Invoke();
        gameObject.SetActive(false);    
    }
    
}