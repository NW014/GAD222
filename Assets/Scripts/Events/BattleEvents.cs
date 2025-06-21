using UnityEngine;
using System;

public class BattleEvents
{
    public event Action onSceneRevert;
    public void OnSceneRevert()
    {
        onSceneRevert?.Invoke();
    }

    public event Action startBattle;
    public void StartBattle()
    {
        startBattle?.Invoke();
    }
}
