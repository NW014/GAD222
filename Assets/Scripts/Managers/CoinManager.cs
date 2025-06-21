using System;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public float coinAmount = 0;
    public TextMeshProUGUI coinText;
    
    void OnEnable()
    {
        GameEventsManager.Instance.coinEvents.onCoinGained += AddCoin;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.coinEvents.onCoinGained -= AddCoin;
    }

    public void AddCoin(float coinCount)
    {
        coinAmount += coinCount;
        coinText.text = $"Coin count: {coinAmount}";
        Debug.Log($"Current coin count: {coinAmount}");
    }
}
