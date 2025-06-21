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
    [SerializeField] private Vector2 selfPosition;
    [SerializeField] private Vector2 otherPosition;

    // To check if interacted object can move to face the player
    // public bool CanMove;
    public bool inDialogueRange = false;
    private bool isActive;
    // private bool isInteracting = false;

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
    
    
    

    
}
