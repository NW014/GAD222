using UnityEngine;

public class Recipe : MonoBehaviour
{
    public string foodName;
    public string foodDescription;
    public int cookTimeNeeded;
    public int currentCooldown;
    public int turnsTillBurnt;
    public string ingredientsNeeded;
    public int buffTimeLeft;
    
    public bool isConsumed = false;
    
    // OnClick > spawn prefab/scriptable object?

    public void OnEnable()
    {
        // subscribe to player turn start event

        currentCooldown = cookTimeNeeded;
    }

    public void OnDisable()
    {
        // unsub from player turn start event
    }

    public void AdvanceCountdown()
    {
        currentCooldown -= 1;
        if (currentCooldown <= -turnsTillBurnt)
        {
            // food is burnt and must be discarded
        }
        else if (currentCooldown <= 0 && currentCooldown > -turnsTillBurnt)
        {
            // food is ready to be consumed
        }
        
        
        // if food is consumed AND has buff effects:
        buffTimeLeft -= 1;

    }
}
