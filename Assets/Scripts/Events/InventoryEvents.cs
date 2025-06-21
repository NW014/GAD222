using System;
using UnityEngine;

public class InventoryEvents
{
    public event Action<float> onAppleGained;
    public void OnAppleGained(float appleCount)
    {
        onAppleGained?.Invoke(appleCount);
    }
}