using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI interactHintText;
    public string[] lines;
    public float textSpeed;
    private int index;

    [SerializeField] private GameObject startingDialogueBox;
    
    void Start()
    {
        startingDialogueBox.SetActive(true);
        GameEventsManager.Instance.playerEvents.DisablePlayerMovement();
        
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            startingDialogueBox.SetActive(false);
            GameEventsManager.Instance.playerEvents.EnablePlayerMovement();
            
            // manually hiding hint again since EnablePlayerMovement will re-enable it.
            interactHintText.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
