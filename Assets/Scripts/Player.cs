using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public IInteractable nearInteractable;
    
    #region Enums
    public enum Directions { Up, Down, Left, Right }
    #endregion

    #region Stats and Variables

    public Rigidbody2D rb;
    public Collider2D col;
    public TextMeshProUGUI eInteracttext;
    
    public GameObject interactHint;
    
    [SerializeField] Animator animator;
    public SpriteRenderer spriteRenderer;
    
    public float moveSpeed;
    public float standardSpeed = 5f;
    public float runSpeed = 10f;
    public float crawlSpeed = 2f;
    public Directions currentDirection = Directions.Right;
    
    public bool movementDisabled = false;
    public bool isCrouching = false;
    //public bool colliderActive = true;
    public bool isGrounded = true;
    public bool isIdleInConversation = false;
    
    public InputSystem_Actions playerControls;

    Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction interact;
    
    #endregion

    #region Animations

    private readonly int animRunRight = Animator.StringToHash("Anim_Player_Run_Right");
    private readonly int animWalkRight = Animator.StringToHash("Anim_Player_Walk_Right");
    private readonly int animIdleRight = Animator.StringToHash("Anim_Player_Idle_Right");
    private readonly int animJumpRight = Animator.StringToHash("Anim_Player_Jump_Right");
    private readonly int animFlyStart = Animator.StringToHash("Anim_Player_Fly_Right_Start");
    private readonly int animFlyEnd = Animator.StringToHash("Anim_Player_Fly_Right_End");

    #endregion
    
    private void Awake()
    {
        playerControls = new InputSystem_Actions();
        interactHint.SetActive(false);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.playerEvents.onDisablePlayerMovement += DisablePlayerMovement;
        GameEventsManager.Instance.playerEvents.onEnablePlayerMovement += EnablePlayerMovement;
        GameEventsManager.Instance.dialogueEvents.onFaceOtherThing += CalculateConvoDirection;
        
        moveSpeed = standardSpeed;
        
        move = playerControls.Player.Move;
        move.Enable();

        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
    }

    private void OnDisable()
    {
        // TODO: Figure out how to unsub from the destroyed managers
        // GameEventsManager.Instance.playerEvents.onDisablePlayerMovement -= DisablePlayerMovement;
        // GameEventsManager.Instance.playerEvents.onEnablePlayerMovement -= EnablePlayerMovement;
        // GameEventsManager.Instance.dialogueEvents.onFaceOtherThing -= CalculateConvoDirection;
        
        move.Disable();
        interact.Disable();
    }

    void Update()
    {
        if (nearInteractable != null)
        {
            if (InputSystem.GetDevice<Keyboard>().eKey.wasPressedThisFrame)
            {
                nearInteractable.Interact();
            }

            if (InputSystem.GetDevice<Mouse>().leftButton.wasPressedThisFrame)
            {
                nearInteractable.ContinueInteract();
            }
        }
        
        if (!movementDisabled)
        {
            isIdleInConversation = false;
            moveDirection = move.ReadValue<Vector2>();
        
            if (Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
            {
                moveSpeed = runSpeed;
            }
            else if (isCrouching == false)
            {
                moveSpeed = standardSpeed;
            }

            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                if (isCrouching == false)
                {
                    isCrouching = true;
                    moveSpeed = crawlSpeed;
                }
                else
                {
                    isCrouching = false;
                    moveSpeed = standardSpeed;
                }
            }

            if (!isGrounded)
            {
                Physics2D.IgnoreLayerCollision(8, 10, true);
            }
            else
            {
                Physics2D.IgnoreLayerCollision(8, 10, false);
            }
        
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                isGrounded = false;
                // if (colliderActive == true)
                // {
                //     colliderActive = false;
                // }
            
                StartCoroutine(playerJump());
            }
        
            // if (colliderActive == false)
            // {
            //     col.enabled = false;
            // }
            // else
            // {
            //     col.enabled = true;
            // }
        
            if (Input.GetKeyDown(KeyCode.F) && isGrounded)
            {
                isGrounded = false;
                animator.CrossFade(animFlyStart, 0);
                playerFly();
            }
            else if (Input.GetKeyDown(KeyCode.F) && !isGrounded)
            {
                StartCoroutine(playerLand());
            }
        }
        else if (movementDisabled)
        {
            moveSpeed = 0;
            if (isIdleInConversation == false)
            {
                // animator.CrossFade(animIdleRight, 0);
                isIdleInConversation = true;
            }
        }
        
        CalculateDirection();
        UpdateAnimation();
        
        // Produces a 3D effect by changing sprite layer sorting.
        GetComponent<SortingGroup>().sortingOrder = (int)(transform.position.y * -100);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.normalized.x * moveSpeed, moveDirection.normalized.y * moveSpeed);
    }
    
    private void DisablePlayerMovement() 
    {
        movementDisabled = true;
        // hides "E to interact" hint when in dialogue.
        eInteracttext.gameObject.SetActive(false);
    }

    private void EnablePlayerMovement() 
    {
        movementDisabled = false;
        // shows "E to interact" hint when done with dialogue.
        eInteracttext.gameObject.SetActive(true);
    }
    
    private void CalculateDirection()
    {
        if (moveDirection.x != 0)
        {
            if (moveDirection.x > 0)
            {
                currentDirection = Directions.Right;
            }
            else if (moveDirection.x < 0)
            {
                currentDirection = Directions.Left;
            }
        }
    }
    
    // Make the player face the object they're interacting with.
    private void CalculateConvoDirection(Vector2 self, Vector2 other)
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
    
    #region Animation Logic

    private void UpdateAnimation()
    {
        if (currentDirection == Directions.Left)
        {
            spriteRenderer.flipX = true;
        }
        else if (currentDirection == Directions.Right)
        {
            spriteRenderer.flipX = false;
        }

        if (isGrounded && !isIdleInConversation)
        {
            if (moveDirection.sqrMagnitude > 0 && Input.GetKey(KeyCode.LeftShift) && !isCrouching) // player moves
            {
                animator.CrossFade(animRunRight, 0);
            }
            else if (moveDirection.sqrMagnitude > 0)
            {
                animator.CrossFade(animWalkRight, 0);
            }
            else 
            {
                animator.CrossFade(animIdleRight, 0);
            }
        }
        else if (!isGrounded && !isIdleInConversation)
        {
            animator.CrossFade(animJumpRight, 0);
        }
        else
        {
            animator.CrossFade(animIdleRight, 0);
        }
    }

    IEnumerator playerJump()
    {
        animator.CrossFade(animJumpRight, 0);
        
        yield return new WaitForSeconds(0.5f);
        
        //colliderActive = true;
        isGrounded = true;
    }

    private void playerFly()
    {
        Debug.Log("The player has took flight.");
        //colliderActive = false;
        animator.CrossFade(animFlyStart, 0);
    }

    IEnumerator playerLand()
    {
        Debug.Log("Player has landed.");
        animator.CrossFade(animFlyEnd, 0);

        yield return new WaitForSeconds(0.3f);
        
        //colliderActive = true;
        isGrounded = true;
    }

    #endregion
    
    // For future interactions
    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted.");
    }
}
