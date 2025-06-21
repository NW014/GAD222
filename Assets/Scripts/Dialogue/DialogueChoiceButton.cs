using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueChoiceButton : MonoBehaviour, ISelectHandler
{
    [Header("Components")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI choiceText;

    private int choiceIndex = -1;

    public void SetChoiceText(string choiceTextString)
    {
        choiceText.text = choiceTextString;
    }

    public void SetChoiceIndex(int choiceIndex)
    {
        this.choiceIndex = choiceIndex;
        // Debug.Log($"{choiceText.text} has an index of {choiceIndex}.");
    }

    public void SelectButton()
    {
        GameEventsManager.Instance.dialogueEvents.UpdateChoiceIndex(choiceIndex);
        // Debug.Log("A new selection has been made.");
        button.Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameEventsManager.Instance.dialogueEvents.UpdateChoiceIndex(choiceIndex);
        // Debug.Log("A new selection has been made.");
    }
}
    
