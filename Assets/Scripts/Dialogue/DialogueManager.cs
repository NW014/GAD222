using System;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")] 
    [SerializeField] private TextAsset inkJson;

    private Story story;
    private InkExternalFunctions inkExternalFunctions;

    [SerializeField] private int currentChoiceIndex = -1;
    
    public bool dialogueActive = false;

    private void Awake()
    {
        story = new Story(inkJson.text);
        inkExternalFunctions = new InkExternalFunctions();
        inkExternalFunctions.Bind(story);
    }

    private void OnDestroy()
    {
        inkExternalFunctions.Unbind(story);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue += EnterDialogue;
        GameEventsManager.Instance.dialogueEvents.onUpdateChoiceIndex += UpdateChoiceIndex;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
        GameEventsManager.Instance.dialogueEvents.onUpdateChoiceIndex -= UpdateChoiceIndex;
    }

    private void UpdateChoiceIndex(int choiceIndex)
    {
        this.currentChoiceIndex = choiceIndex;
        // Debug.Log($"Choice index: {choiceIndex}");
    }

    private void EnterDialogue(string knotName)
    {
        // prevents player from entering new dialogue while in dialogue.
        if (dialogueActive)
        {
            ContinueOrExitStory();
            return;
        }
        
        dialogueActive = true;
        GameEventsManager.Instance.dialogueEvents.DialogueStarted();

        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);
        }
        else
        {
            Debug.LogWarning("Knot name was the empty string when entering dialogue.");
        }

        // start the story
        ContinueOrExitStory();
        
        // Debug.Log("Testing dialogue.");
    }

    private void ContinueOrExitStory()
    {
        // make a choice when possible
        if (story.currentChoices.Count > 0 && currentChoiceIndex != -1)
        {
            GameEventsManager.Instance.playerEvents.DisablePlayerMovement();
            story.ChooseChoiceIndex(currentChoiceIndex);
            // resets choice index for next time
            currentChoiceIndex = -1;
        }
        
        if (story.canContinue)
        {
            string dialogueLine = story.Continue();
            GameEventsManager.Instance.dialogueEvents.DisplayDialogue(dialogueLine, story.currentChoices);
            // Debug.Log(dialogueLine);

            while (IsLineBlank(dialogueLine) && story.canContinue)
            {
                dialogueLine = story.Continue();
            }

            if (IsLineBlank(dialogueLine) && !story.canContinue)
            {
                ExitDialogue();
            }
            else
            {
                GameEventsManager.Instance.dialogueEvents.DisplayDialogue(dialogueLine, story.currentChoices);
            }
        }
        else if (story.currentChoices.Count == 0)
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        // Debug.Log("Exiting dialogue");

        // reset story state
        dialogueActive = false;
        GameEventsManager.Instance.dialogueEvents.DialogueEnded();
    }

    private bool IsLineBlank(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }
}
