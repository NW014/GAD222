using System;
using UnityEngine;


public class Goblin : MonoBehaviour
{
    public enum Directions {Left, Right};
    
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    // [SerializeField] private GameObject player;
    // [SerializeField] private GameObject goblin;
    
    private readonly int animIdleRight = Animator.StringToHash("Anim_Goblin_Idle_Right");
    
    public Directions currentDirection = Directions.Right;
    private Vector2 direction = Vector2.zero;

    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.onFaceOtherThing += CalculateDirection;
    }

    private void OnDisable()
    {
        // Removed due to error that occurs when unsubscribing from an already destroyed object.
        // GameEventsManager.Instance.dialogueEvents.onFaceOtherThing -= CalculateDirection;
    }

    void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        // Debug.Log(direction);
        if (currentDirection == Directions.Left)
        {
            spriteRenderer.flipX = true;
        }
        else if (currentDirection == Directions.Right)
        {
            spriteRenderer.flipX = false;
        }
    }

    // makes the goblin face the player when interacted with.
    private void CalculateDirection(Vector2 other, Vector2 self)
    {
        if (self.x != 0)
        {
            if (self.x < other.x)
            {
                currentDirection = Directions.Right;
            }
            else if (self.x > other.x)
            {
                currentDirection = Directions.Left;
            }
        }
    }
}
