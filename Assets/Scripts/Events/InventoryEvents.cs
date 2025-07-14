using System;
using UnityEngine;

public class InventoryEvents
{
    public event Action<float> onAppleGained;
    public void OnAppleGained(float appleCount)
    {
        onAppleGained?.Invoke(appleCount);
    }

    public event Action<float> itemCollect;
    public void ItemCollect(float itemCount)
    {
        itemCollect?.Invoke(itemCount);
    }

    public event Action bagCheck;

    public void BagCheck()
    {
        bagCheck?.Invoke();
    }

    public event Action bagFound;

    public void BagFound()
    {
        bagFound?.Invoke();
    }

    public event Action<string> addItemName;

    public void AddItemName(string name)
    {
        addItemName?.Invoke(name);
    }
}