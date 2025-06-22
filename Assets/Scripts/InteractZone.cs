using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractZone : MonoBehaviour, IInteractable
{
    [Header("Dialogue (optional)")] 
    [SerializeField] private string dialogueKnotName;
    
    [SerializeField] UnityEvent onTriggerEnter2D;
    [SerializeField] UnityEvent onTriggerStay2D;
    [SerializeField] UnityEvent onTriggerExit2D;
    [SerializeField] private string tagCheck;
    [SerializeField] private string bagTagCheck = "Bag";
    [SerializeField] private bool bagFound = false;
    [SerializeField] private Vector2 selfPosition;
    [SerializeField] private Vector2 otherPosition;
    private GameObject PlayerObject;
    
    private bool bagCollected = false;

    // To check if interacted object can move to face the player
    // public bool CanMove;
    public bool inDialogueRange = false;
    private bool isActive;
    // private bool isInteracting = false;

    public void OnEnable()
    {
        
        GameEventsManager.Instance.inventoryEvents.bagCheck += BagCheck;
        GameEventsManager.Instance.inventoryEvents.bagFound += BagCollected;
        GameEventsManager.Instance.inventoryEvents.itemCollect += ItemCollected;
    }

    public void OnDisable()
    {
        GameEventsManager.Instance.inventoryEvents.bagFound -= BagCollected;
        GameEventsManager.Instance.inventoryEvents.bagCheck -= BagCheck;
        GameEventsManager.Instance.inventoryEvents.itemCollect += ItemCollected;
    }

    public void Awake()
    {
        selfPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        otherPosition = other.transform.position;

        if (player != null)
        {
            player.nearInteractable = this;
            PlayerObject = other.gameObject;
        }
        
        if (isActive)
            return;
        
        if (!string.IsNullOrEmpty(tagCheck) && (other.gameObject.CompareTag(tagCheck)))
        {
            onTriggerEnter2D.Invoke();
            inDialogueRange = true;
            isActive = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // gets the position of the other object within the zone.
        otherPosition = other.transform.position;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        // clears out the otherPosition variable
        otherPosition = new Vector2(); 

        if (player != null)
        {
            player.nearInteractable = null;
            PlayerObject = null;
        }
        
        if (!string.IsNullOrEmpty(tagCheck) && (other.gameObject.CompareTag(tagCheck)))
        {
            onTriggerExit2D.Invoke();
            inDialogueRange = false;
            isActive = false;
        }
    }
    
    public void DoZoneStuff()
    {
        GameEventsManager.Instance.dialogueEvents.EnterDialogue(dialogueKnotName);
        GameEventsManager.Instance.dialogueEvents.FaceOtherThing(otherPosition, selfPosition);
    }

    public void ClickToContinue()
    {
        GameEventsManager.Instance.dialogueEvents.EnterDialogue(dialogueKnotName);
        GameEventsManager.Instance.dialogueEvents.FaceOtherThing(otherPosition, selfPosition);
    }

    /// <summary>
    /// This is the ONLY way my player code can access this code
    /// </summary>
    public void Interact()
    {
        // Do whatever this particular object needs. Different for every object
        DoZoneStuff();
    }

    public void ContinueInteract()
    {
        ClickToContinue();
    }
    
    public void BagCheck()
    {
        if (PlayerObject != null)
        {
            if (!bagCollected)
            {
                if (bagTagCheck != null)
                {
                    if (this.gameObject.CompareTag(bagTagCheck))
                    {
                        bagCollected = true;
                        GameEventsManager.Instance.inventoryEvents.ItemCollect(1);
                        GameEventsManager.Instance.inventoryEvents.BagFound();
                        Debug.Log("You have picked up a bag!");
                    }
                    else
                    {
                        return;
                    } 
                }
                else
                {
                    Debug.Log("There is no tag to be checked.");
                }
            }
            else
            {
                if (!gameObject.CompareTag("Door"))
                {
                    GameEventsManager.Instance.inventoryEvents.ItemCollect(1);
                }
            }
        }
    }
    
    public void BagCollected()
    {
        bagCollected = true;
    }

    public void ItemCollected(float amount)
    {
        if (PlayerObject != null)
        {
            Destroy(gameObject);
        }
    }
    
}
