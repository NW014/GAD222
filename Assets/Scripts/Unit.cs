using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Enums and States
    public enum CurrentState { IDLE, ATTACKING, DAMAGED }
    public CurrentState currentState;
        
    public enum BellyState { BLOATED, SATISFIED, NEUTRAL, HUNGRY, FAMISHED}
    public BellyState bellyState;
    #endregion
    
    #region Stats
    public string unitName;
    public string unitLevel;

    public float maxHealth;
    public float currentHealth;
    
    [SerializeField] private float strength;
    public float currentStrength;
    [SerializeField] private float changedStrength;
    [SerializeField] private float speed; // Currently has no use; determines unit's turn order.
    public int energy; // the hunger meter 
    public int maxEnergy = 100;
    #endregion

    #region Animations
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // private readonly int animIdle;
    // private readonly int animAttack;
    // private readonly int animDamaged;

    private bool isAnimPlaying = false;
    #endregion

    public void OnEnable()
    {
        currentStrength = strength;
    }

    public void Update()
    {
        BellyEffect();
    }

    public bool ChangeHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    // For temporary strength increases during battle
    public virtual void ChangeStrength(float amount)
    {
        currentStrength = strength + amount;
    }
    
    // Not currently used but will be useful for permanent strength changes.
    public virtual void IncreaseStrength(float amount)
    {
        strength += amount;
    }
    
    public virtual void ChangeEnergy(int amount)
    {
        energy += amount;
    }
    
    // Increase/reduce strength based on current energy level. 
    protected void BellyEffect()
    {
        if (energy > 90) // Bloated
        {
            currentStrength = strength - 5;
            bellyState = BellyState.BLOATED;
        }
        
        if (energy > 70 && energy <= 90) // Just right
        {
            currentStrength = strength + 5;
            bellyState = BellyState.SATISFIED;
        }

        if (energy > 40 && energy <= 70) // Neutral
        {
            currentStrength = strength;
            bellyState = BellyState.NEUTRAL;
        }

        if (energy >10 && energy <= 40) // Hungry
        {
            currentStrength = strength - 5;
            bellyState = BellyState.HUNGRY;
        }

        if (energy <= 10) // Very hungry
        {
            currentStrength = strength - 8;
            bellyState = BellyState.FAMISHED;
        }
    }
}
