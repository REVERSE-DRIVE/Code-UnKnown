using UnityEngine;
using UnityEngine.Events;

public class DoorObject : MonoBehaviour
{
    public UnityEvent OnDoorOpenEvent;
    public UnityEvent OnDoorCloseEvent;
    
    public void OpenDoor()
    {
        OnDoorOpenEvent?.Invoke();
        gameObject.SetActive(false);    
    }

    public void CloseDoor()
    {
        OnDoorCloseEvent?.Invoke();
        gameObject.SetActive(true);  
    }
    
}