using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Enums and States
    public enum CurrentMusicState 
    { 
      HOME = 0,
      OUTDOOR = 1, 
      TOWN = 2
    }
    public CurrentMusicState currentMusicState;
    #endregion
    
    #region Audio Sources and Clips
    public AudioSource menuClick;
    public AudioSource doorOpen;
    public AudioSource doorClose;
    public AudioSource backgroundMusic;
    
    public AudioClip homeMusic;
    public AudioClip outdoorMusic;
    public AudioClip townMusic;
    #endregion

    public void OnEnable()
    {
        GameEventsManager.Instance.audioEvents.onMenuClick += PlayMenuClick;
        GameEventsManager.Instance.audioEvents.onDoorOpen += OpenDoorSound;
        GameEventsManager.Instance.audioEvents.onDoorClose += CloseDoorSound;

        GameEventsManager.Instance.audioEvents.onEnterHome += SetMusicHome;
        GameEventsManager.Instance.audioEvents.onEnterOutdoor += SetMusicOutdoor;
        GameEventsManager.Instance.audioEvents.onEnterTown += SetMusicTown;

        currentMusicState = CurrentMusicState.HOME;
    }

    public void OnDisable()
    {
        GameEventsManager.Instance.audioEvents.onMenuClick -= PlayMenuClick;
        GameEventsManager.Instance.audioEvents.onDoorOpen -= OpenDoorSound;
        GameEventsManager.Instance.audioEvents.onDoorClose -= CloseDoorSound;
        
        GameEventsManager.Instance.audioEvents.onEnterHome -= SetMusicHome;
        GameEventsManager.Instance.audioEvents.onEnterOutdoor -= SetMusicOutdoor;
        GameEventsManager.Instance.audioEvents.onEnterTown -= SetMusicTown;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (currentMusicState == CurrentMusicState.HOME)
            {
                currentMusicState = CurrentMusicState.OUTDOOR;
            }
            else
            {
                currentMusicState = CurrentMusicState.HOME;
            }
        }
        
        
        // if (!menuClick.isPlaying)
        // {
        //     currentMusicState = CurrentMusicState.HOME;
        // }

        if (currentMusicState == CurrentMusicState.HOME && backgroundMusic.clip != homeMusic)
        {
            backgroundMusic.Stop();
            Debug.Log($"Stopped {backgroundMusic.clip}.");
            backgroundMusic.clip = homeMusic;
            backgroundMusic.Play();
            Debug.Log($"{backgroundMusic.clip} started.");
        }
        else if (currentMusicState == CurrentMusicState.OUTDOOR && backgroundMusic.clip != outdoorMusic)
        {
            backgroundMusic.Stop();
            Debug.Log($"Stopped {backgroundMusic.clip}.");
            backgroundMusic.clip = outdoorMusic;
            backgroundMusic.Play();
        }
        else if (currentMusicState == CurrentMusicState.TOWN && backgroundMusic.clip != townMusic)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = townMusic;
            backgroundMusic.Play();
        }
    }

    public void SetMusicHome(int scene)
    {
        // currentMusicState = CurrentMusicState.HOME;
        
        // changes state depending on where the door leads to.
        currentMusicState = (CurrentMusicState)scene;
    }

    public void SetMusicOutdoor()
    {
        currentMusicState = CurrentMusicState.OUTDOOR;
    }

    public void SetMusicTown()
    {
        currentMusicState = CurrentMusicState.TOWN;
    }


    public void PlayMenuClick()
    {
        menuClick.Play();
    }

    public void OpenDoorSound()
    {
        doorOpen.Play();
    }

    public void CloseDoorSound()
    {
        doorClose.Play();
    }
}
