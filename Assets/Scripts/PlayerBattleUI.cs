using UnityEngine;

public class PlayerBattleUI : BattleUI
{
    private static PlayerBattleUI Instance;

    private GameObject player;
    public PlayerUnit playerUnit;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject.transform.parent.gameObject);
        }
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerUnit = player.GetComponent<PlayerUnit>();
        
        //playerUnit = GetComponent<PlayerUnit>();

        if (playerUnit != null)
        {
            SetText(playerUnit);
            SetHealth(playerUnit.currentHealth, playerUnit.maxHealth);
            SetEnergy(playerUnit.energy);
        }
    }

    private void OnEnable()
    {
        
        
    }
}
