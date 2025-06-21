using System;
using UnityEngine;

public class CoinEvents
{
    public event Action<float> onCoinGained;
    public void OnCoinGained(float coinCount)
    {
        onCoinGained?.Invoke(coinCount);
    }
}
