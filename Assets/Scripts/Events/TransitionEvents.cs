using UnityEngine;
using System;

public class TransitionEvents
{
    public event Action<int> onEnterBuilding;
    public void EnterBuilding(int SceneValue)
    {
        onEnterBuilding?.Invoke(SceneValue);
    }
}
