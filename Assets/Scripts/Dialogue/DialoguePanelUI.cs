using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialoguePanelUI : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueChoiceButton[] choiceButtons;
    // [SerializeField] private float textSpeed = 10;

    private void Awake()
    {
        ResetPanel();
        contentParent.SetActive(false); 
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.onDialogueStarted += DialogueStarted;
        GameEventsManager.Instance.dialogueEvents.onDialogueEnded += DialogueEnded;
        GameEventsManager.Instance.dialogueEvents.onDisplayDialogue += DisplayDialogue;

        contentParent = gameObject.transform.GetChild(0).gameObject;
        
        //  Player player = other.gameObject.GetComponent<Player>();
        // thoughtBubble = player.transform.GetChild(1).GetChild(0).gameObject;
    }

    private void OnDisable()
    {
        // disabled to prevent error messages when stopping game.
        // GameEventsManager.Instance.dialogueEvents.onDialogueStarted -= DialogueStarted;
        // GameEventsManager.Instance.dialogueEvents.onDialogueEnded -= DialogueEnded;
        // GameEventsManager.Instance.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
    }
    
    private void DialogueStarted()
    {
        contentParent.SetActive(true);
        GameEventsManager.Instance.playerEvents.DisablePlayerMovement();
    }

    private void DialogueEnded()
    {
        ResetPanel();
        contentParent.SetActive(false);
        GameEventsManager.Instance.playerEvents.EnablePlayerMovement();
    }

    private void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        // attempted to display text one by one.
        // char[] characters = dialogueLine.ToCharArray();
        // StartCoroutine(TypeLine(characters));
        
        dialogueText.text = dialogueLine;

        if (dialogueChoices.Count > choiceButtons.Length)
        {
            Debug.LogError($"More dialogue choices ({dialogueChoices.Count}) came through than are supported ({choiceButtons.Length}).");
        }

        // disables all buttons at start
        foreach (DialogueChoiceButton choiceButton in choiceButtons)
        {
            choiceButton.gameObject.SetActive(false);
        }

        int choiceButtonIndex = dialogueChoices.Count -1;
        for (int inkChoiceIndex = 0; inkChoiceIndex < dialogueChoices.Count; inkChoiceIndex++)
        {
            Choice dialogueChoice = dialogueChoices[inkChoiceIndex];
            DialogueChoiceButton choiceButton = choiceButtons[choiceButtonIndex];

            choiceButton.gameObject.SetActive(true);
            choiceButton.SetChoiceText(dialogueChoice.text);
            choiceButton.SetChoiceIndex(inkChoiceIndex);

           // Selects the first option 
           if (inkChoiceIndex == 0)
           {
               choiceButton.SelectButton();
               GameEventsManager.Instance.dialogueEvents.UpdateChoiceIndex(0);
           }

           choiceButtonIndex--;
        }
    }

    private void ResetPanel()
    {
        dialogueText.text = "";
    }
    
    // IEnumerator TypeLine(char[] characters)
    // {
    //     int index = characters.Length;
    //     
    //     foreach (char c in characters)
    //     {
    //         dialogueText.text += c;
    //         yield return new WaitForSeconds(textSpeed);
    //     }
    // }
}
