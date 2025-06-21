using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleManager : MonoBehaviour
{
    // TODO: make the code automatically get all the units involved in combat
    public GameObject player;
    public GameObject enemyPrefab;

    public Transform playerLocation;
    public Transform enemyLocation; // for single enemy, need coordinates for double enemy battles
    public Transform attackLocation;
    public Transform enemyAttackLocation;

    private Unit playerUnit;
    private Unit enemyUnit;
    
    public BattleState state;

    public int turnCount = 0;
    public TextMeshProUGUI turnText;
    
    public TextMeshProUGUI battleDialogueText;
    public TextMeshProUGUI battleDialogueTextMenu;
    public GameObject battleButtonPanel;
    public GameObject battleAttackList;

    public GameObject playerBattleUI;

    public BattleUI playerUI;
    public BattleUI enemyUI;
    
    private UnityEvent OnSceneRevert;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerUnit = player.GetComponent<PlayerUnit>();
        
        playerBattleUI = GameObject.Find("PlayerInfo");
        playerUI = playerBattleUI.GetComponent<PlayerBattleUI>();
        
        
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        battleButtonPanel.SetActive(false);
        battleDialogueText.gameObject.SetActive(true);
        battleDialogueTextMenu.gameObject.SetActive(false);
        
        // GameObject playerObject = Instantiate(playerPrefab, playerLocation);
        player.gameObject.transform.position = playerLocation.position;
        // playerUnit = playerObject.GetComponent<Unit>();
        playerUnit = player.GetComponent<PlayerUnit>();
        
        GameObject enemyObject = Instantiate(enemyPrefab, enemyLocation);
        enemyUnit = enemyObject.GetComponent<Unit>();

        battleDialogueText.text = $"{enemyUnit.unitName} has challenged you to a battle!";
        
        playerUI.SetText(playerUnit);
        enemyUI.SetText(enemyUnit);

        yield return new WaitForSeconds(2f);
        
        // TODO: create array of all units in battle, arrange them via speed
        // TODO: unit with the highest speed will start first.
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void Update()
    {
        
    }

    void PlayerTurn()
    {
        UpdateTurn();
        battleDialogueTextMenu.gameObject.SetActive(true);
        battleDialogueTextMenu.text = battleDialogueText.text;
        battleDialogueText.gameObject.SetActive(false);
        battleButtonPanel.SetActive(true);
        battleDialogueTextMenu.text = "What will you do?";
    }

    IEnumerator EnemyTurn()
    {
        // for multiple enemies:
        // for x < number of enemies, loop code below?
        
        battleDialogueTextMenu.gameObject.SetActive(false);
        battleButtonPanel.SetActive(false);
        battleDialogueText.gameObject.SetActive(true);
        battleDialogueText.text = "The enemy starts attacking!";
        
        yield return new WaitForSeconds(0.5f);
        enemyUnit.currentState = Unit.CurrentState.ATTACKING;
        enemyUnit.gameObject.transform.position = enemyAttackLocation.position;
        enemyUnit.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        
        // play attack animation
        yield return new WaitForSeconds(2f);

        bool playerDead = playerUnit.ChangeHealth(-enemyUnit.currentStrength);
        // reduce enemy energy if needed
        battleDialogueText.text = $"You took {enemyUnit.currentStrength} damage!";
        playerUI.SetHealth(playerUnit.currentHealth, playerUnit.maxHealth);
        
        yield return new WaitForSeconds(0.5f);

        enemyUnit.gameObject.transform.position = enemyLocation.position;
        enemyUnit.gameObject.transform.localScale = new Vector3(1, 1, 1);
        
        yield return new WaitForSeconds(2f);

        if (playerDead)
        {
            state = BattleState.LOST;
            StartCoroutine(BattleEnd());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    
    #region Player Attack List
    public void OnNormalAttack()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
    
        battleDialogueTextMenu.gameObject.SetActive(false);
        battleButtonPanel.SetActive(false);
        battleDialogueText.gameObject.SetActive(true);
        battleDialogueText.text = "Attacking the enemy!";
            
        StartCoroutine(PlayerAttack(playerUnit.currentStrength));
    }
    
    public void OnSkill1()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
    
        battleDialogueTextMenu.gameObject.SetActive(false);
        battleButtonPanel.SetActive(false);
        battleDialogueText.gameObject.SetActive(true);
        battleDialogueText.text = "Activating Mighty Sword Slash!";
            
        StartCoroutine(PlayerAttack(playerUnit.currentStrength + 5));
        playerUnit.ChangeEnergy(-10);
        playerUI.SetEnergy(playerUnit.energy);
    }
    #endregion

    IEnumerator PlayerAttack(float strength)
    {
        yield return new WaitForSeconds(0.5f);
        // TODO: play attack animation
        // playerUnit.currentState = Unit.CurrentState.ATTACKING;
        playerUnit.gameObject.transform.position = attackLocation.position;
        playerUnit.gameObject.transform.localScale = new Vector3(1.5f, 1.55f, 1.5f);
        yield return new WaitForSeconds(2f);

        bool enemyDead = enemyUnit.ChangeHealth(-strength);
        battleDialogueText.text = $"The enemy took {strength} damage!";
        enemyUI.SetHealth(enemyUnit.currentHealth, enemyUnit.maxHealth);
        
        yield return new WaitForSeconds(0.5f);
        
        playerUnit.gameObject.transform.position = playerLocation.position;
        playerUnit.gameObject.transform.localScale = new Vector3(2, 2, 2);

        yield return new WaitForSeconds(2f);
        
        
        if (enemyDead)
        {
            state = BattleState.WON;
            StartCoroutine(BattleEnd());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnCook()
    {
        // if have cooking slots left, open menu
        // else, print message saying no more slots left.
    }
    
    IEnumerator BattleEnd()
    {
        if (state == BattleState.WON)
        {
            enemyUnit.gameObject.SetActive(false);
            enemyUI.gameObject.SetActive(false);
            battleDialogueText.text = "You have won the battle!";
            
            // TODO: Implement enemy loop drops
        }
        else if (state == BattleState.LOST)
        {
            playerUnit.gameObject.SetActive(false);
            battleDialogueText.text = "You have lost the battle!";
            // TODO: Add game over screen
        }
        
        yield return new WaitForSeconds(3f);

        GameEventsManager.Instance.battleEvents.OnSceneRevert();
        StartCoroutine(WaitOneSecond()); // So scene does not get unloaded immediately before transitions
    }

    private void UpdateTurn()
    {
        turnCount += 1;
        turnText.text = $"Turn {turnCount}";
    }

    IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1f);
        
        SceneManager.UnloadSceneAsync(1);
    }
}
