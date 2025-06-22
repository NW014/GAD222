using System;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public float appleAmount = 0;
    public float itemAmount = 0;
    public TextMeshProUGUI appleText;
    
    void OnEnable()
    {
        GameEventsManager.Instance.inventoryEvents.onAppleGained += AddApple;
        GameEventsManager.Instance.inventoryEvents.itemCollect += AddItem;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.inventoryEvents.onAppleGained -= AddApple;
        GameEventsManager.Instance.inventoryEvents.itemCollect -= AddItem;
    }

    public void AddApple(float appleCount)
    {
        appleAmount += appleCount;
        Debug.Log($"Current apple count: {appleAmount}");
        appleText.text = $"Apple count: {appleAmount}";
    }

    public void AddItem(float itemCount)
    {
        itemAmount += itemCount;
        Debug.Log($"You have picked up an item! You now have {itemAmount} items");
    }

    
}

