using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportDoor : MonoBehaviour, IInteractable
{
    public int DoorXValue;
    public int DoorYValue;
    private Vector2 otherPosition;
    private GameObject thoughtBubble;
    private GameObject playerObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        otherPosition = other.transform.position;
        thoughtBubble = player.transform.GetChild(1).GetChild(0).gameObject;
        
        thoughtBubble.gameObject.SetActive(true);
        
        if (player != null)
        {
            player.nearInteractable = this;
            playerObject = other.gameObject;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        // gets the position of the other object within the zone.
        otherPosition = other.transform.position;
        
        thoughtBubble.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        
        thoughtBubble.gameObject.SetActive(false);
        
        // clears out the otherPosition variable
        otherPosition = new Vector2(); 
        
        if (player != null)
        {
            player.nearInteractable = null;
            playerObject = null;
        }
    }
    
    public void Interact()
    {
        // GameEventsManager.Instance.transitionEvents.EnterBuilding(SceneValue);
        // Debug.Log("Jumping to scene " + SceneValue);
        playerObject.transform.position = new Vector2(DoorXValue, DoorYValue);
    }

    public void ContinueInteract()
    {
        // GameEventsManager.Instance.transitionEvents.EnterBuilding(SceneValue);
        // Debug.Log("Going to scene " + SceneValue);
        playerObject.transform.position = new Vector2(DoorXValue, DoorYValue);
    }
    
}
