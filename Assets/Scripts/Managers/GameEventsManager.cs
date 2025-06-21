using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    private static GameEventsManager _instance;

    public static GameEventsManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<GameEventsManager>();
            }
            return _instance;
        }
    }
    
    public DialogueEvents dialogueEvents;
    public PlayerEvents playerEvents;
    public CoinEvents coinEvents;
    public InventoryEvents inventoryEvents;
    public BattleEvents battleEvents;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        // initialize all events
        dialogueEvents = new DialogueEvents();
        playerEvents = new PlayerEvents();
        coinEvents = new CoinEvents();
        inventoryEvents = new InventoryEvents();
        battleEvents = new BattleEvents();
    }
}
