using System;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public float appleAmount = 0;
    public TextMeshProUGUI appleText;
    
    void OnEnable()
    {
        GameEventsManager.Instance.inventoryEvents.onAppleGained += AddApple;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.inventoryEvents.onAppleGained -= AddApple;
    }

    public void AddApple(float appleCount)
    {
        appleAmount += appleCount;
        Debug.Log($"Current apple count: {appleAmount}");
        appleText.text = $"Apple count: {appleAmount}";
    }
}

