using System;
using UnityEngine;

public class PlayerEvents
{
    public event Action onDisablePlayerMovement;
    public void DisablePlayerMovement()
    {
        onDisablePlayerMovement?.Invoke();
    }

    public event Action onEnablePlayerMovement;
    public void EnablePlayerMovement()
    {
        onEnablePlayerMovement?.Invoke();
    }
}
