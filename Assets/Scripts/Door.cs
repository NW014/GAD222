using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public int SceneValue;
    private Vector2 otherPosition;
    private GameObject thoughtBubble;
    private GameObject playerObject;

    void OnEnable()
    {
        // GameEventsManager.Instance.transitionEvents.onEnterBuilding += EnterBuilding;
    }

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
        SceneManager.LoadScene(SceneValue);
    }

    public void ContinueInteract()
    {
        // GameEventsManager.Instance.transitionEvents.EnterBuilding(SceneValue);
        // Debug.Log("Going to scene " + SceneValue);
        SceneManager.LoadScene(SceneValue);
    }
}
