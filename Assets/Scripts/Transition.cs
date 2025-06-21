using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    private static Transition Instance;

    public Camera_Movement cameraScript;
    public Player player;
    public GameObject overworldGraphics;
    public GameObject overworldCanvas;
    public GameObject playerCanvas;
    
    public Vector3 locationBeforeChange;
    public Vector3 playerUILocation;

    public Animator sceneChanger;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);

        cameraScript = GetComponent<Camera_Movement>();
        
        playerUILocation = playerCanvas.gameObject.transform.position;
    }

    public void OnEnable()
    {
        GameEventsManager.Instance.battleEvents.onSceneRevert += TransitionToMain;
        GameEventsManager.Instance.battleEvents.startBattle += StartBattle;
    }

    public void OnDisable()
    {
        GameEventsManager.Instance.battleEvents.onSceneRevert -= TransitionToMain;
        GameEventsManager.Instance.battleEvents.startBattle -= StartBattle;
    }
    
    
    void Update()
    {
        // TODO: Enable dialogue to trigger battle.
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(TransitionToBattle());
        }
    }

    public void StartBattle()
    {
        Debug.Log("Battle starting...");
        StartCoroutine(TransitionToBattle());
    }

    IEnumerator TransitionToBattle()
    {
        sceneChanger.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        
        locationBeforeChange = player.transform.position;
        if (cameraScript != null)
        {
            cameraScript.enabled = false;
        }
        player.gameObject.transform.localScale = new Vector3(2, 2, 2);
        playerCanvas.transform.position = new Vector3(243, 437, 0);
        overworldCanvas.SetActive(false);
        overworldGraphics.SetActive(false);
        player.movementDisabled = true;
        SceneManager.LoadScene(1, LoadSceneMode.Additive);

        yield return new WaitForSeconds(0.5f);
        
        sceneChanger.SetTrigger("Continue");

        yield return new WaitForSeconds(1f);
    }
    
    
    public void TransitionToMain()
    {
        StartCoroutine(FadeToMain());
    }

    IEnumerator FadeToMain()
    {
        sceneChanger.SetTrigger("Start");
        
        yield return new WaitForSeconds(1f);
        
        // Resets player back to original location before scene change.
        cameraScript.enabled = true;
        player.transform.position = locationBeforeChange;
        player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        player.movementDisabled = false;
        
        // Adds UI and overworld graphic back
        overworldCanvas.SetActive(true);
        overworldGraphics.SetActive(true);
        playerCanvas.transform.position = playerUILocation;
        
        yield return new WaitForSeconds(0.5f);
        
        sceneChanger.SetTrigger("Continue");
        
        yield return new WaitForSeconds(1f);
        
        
    }
}
