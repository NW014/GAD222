using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleportDoor : MonoBehaviour, IInteractable
{
    public int DoorXValue;
    public int DoorYValue;
    public int LeadsToScene;
    private Vector2 otherPosition;
    private GameObject thoughtBubble;
    private GameObject playerObject;
    public Image FadeScreen;
    public Player player;
    
    public Animator sceneChanger;

    public void OnEnable()
    {
        FadeScreen = GameObject.Find("FadeScreen").GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject.GetComponent<Player>();
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
        player = other.gameObject.GetComponent<Player>();
        
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
        
        StartCoroutine(FadeToMain());
        //playerObject.transform.position = new Vector2(DoorXValue, DoorYValue);
    }

    public void ContinueInteract()
    {
        // GameEventsManager.Instance.transitionEvents.EnterBuilding(SceneValue);
        // Debug.Log("Going to scene " + SceneValue);
        
        StartCoroutine(FadeToMain());
        //playerObject.transform.position = new Vector2(DoorXValue, DoorYValue);
    }

    IEnumerator FadeToMain()
    {
        player.movementDisabled = true;
        
        GameEventsManager.Instance.audioEvents.DoorOpen();
        // Called enterhome but is just a music update based on area entered
        GameEventsManager.Instance.audioEvents.EnterHome(LeadsToScene);
        
        Debug.Log("Starting fade.");
        sceneChanger.SetTrigger("Start");
        
        yield return new WaitForSeconds(1f);
        
        playerObject.transform.position = new Vector2(DoorXValue, DoorYValue);
        
        // Resets player back to original location before scene change.
        //cameraScript.enabled = true;
        //player.transform.position = locationBeforeChange;
        //player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        //player.movementDisabled = false;
        
        // Adds UI and overworld graphic back
        //overworldCanvas.SetActive(true);
        //overworldGraphics.SetActive(true);
        //playerCanvas.transform.position = playerUILocation;
        
        yield return new WaitForSeconds(1f);
        
        GameEventsManager.Instance.audioEvents.DoorClose();
        
        yield return new WaitForSeconds(0.5f);
        
        sceneChanger.SetTrigger("Continue");
        Debug.Log("Teleported.");
        
        yield return new WaitForSeconds(1f);
        
        player.movementDisabled = false;
    }
}
