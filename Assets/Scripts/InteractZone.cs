using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    [SerializeField] private bool isTravelPoint;
    [SerializeField] private int SceneValue;

    [SerializeField] private GameObject thoughtBubble;
    
    
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
        
        // thoughtBubble = GameObject.FindWithTag("Bubble");
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
        thoughtBubble = player.transform.GetChild(1).GetChild(0).gameObject;
        
        thoughtBubble.gameObject.SetActive(true);

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
        
        thoughtBubble.gameObject.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        
        thoughtBubble.gameObject.SetActive(false);
        
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
        Debug.Log(dialogueKnotName);
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
        
        if (isTravelPoint)
        {
            GameEventsManager.Instance.transitionEvents.EnterBuilding(SceneValue);
        }
        else
        {
            DoZoneStuff();
        }
        
    }

    public void ContinueInteract()
    {
        ClickToContinue();
        
        if (isTravelPoint && bagFound)
        {
            GameEventsManager.Instance.transitionEvents.EnterBuilding(SceneValue);
            Debug.Log("Test");
        }
        
    }
    
    public void BagCheck()
    {
        if (PlayerObject != null)
        {
            if (!bagCollected)
            {
                if (bagTagCheck != null)
                {
                    if (gameObject.CompareTag(bagTagCheck))
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
                if (!gameObject.CompareTag("Door") && !gameObject.CompareTag("NotItem"))
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
            GameEventsManager.Instance.inventoryEvents.AddItemName(gameObject.name);
            Debug.Log(gameObject.name + " has been added to your inventory.");
            Destroy(gameObject);
        }
    }
    
    
    
}
