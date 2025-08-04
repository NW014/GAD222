using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioEvents : MonoBehaviour
{
    public event Action onMenuClick;
    public void MenuClick()
    {
        onMenuClick?.Invoke();
    }

    public event Action onDoorOpen;

    public void DoorOpen()
    {
        onDoorOpen?.Invoke();
    }
    
    public event Action onDoorClose;

    public void DoorClose()
    {
        onDoorClose?.Invoke();
    }

    public event Action<int> onEnterHome;

    public void EnterHome(int scene)
    {
        onEnterHome?.Invoke(scene);
    }
    
    public event Action onEnterOutdoor;

    public void EnterOutdoor()
    {
        onEnterOutdoor?.Invoke();
    }
    
    public event Action onEnterTown;

    public void EnterTown()
    {
        onEnterTown?.Invoke();
    }
}
