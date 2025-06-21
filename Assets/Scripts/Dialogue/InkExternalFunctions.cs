using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("AddCoin", (float coinCount) => AddCoin(coinCount));
        story.BindExternalFunction("AddApple", (float appleCount) => AddApple(appleCount));
        story.BindExternalFunction("StartCombat", () => StartBattle());
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("AddCoin");
        story.UnbindExternalFunction("AddApple");
    }
    
    private void AddCoin(float coinCount)
    {
        GameEventsManager.Instance.coinEvents.OnCoinGained(coinCount);
    }

    private void AddApple(float appleCount)
    {
        GameEventsManager.Instance.inventoryEvents.OnAppleGained(appleCount);
    }

    private void StartBattle()
    {
        GameEventsManager.Instance.battleEvents.StartBattle();
    }
}
