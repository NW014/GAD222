using UnityEngine;
using System;
using System.Collections.Generic;
using Ink.Runtime;

public class DialogueEvents
{
   public event Action<string> onEnterDialogue;
   public void EnterDialogue(string knotName)
   {
      onEnterDialogue?.Invoke(knotName);
   }

   public event Action onDialogueStarted;
   public void DialogueStarted()
   {
      onDialogueStarted?.Invoke();
   }

   public event Action onDialogueEnded;
   public void DialogueEnded()
   {
      onDialogueEnded?.Invoke();
   }

   public event Action<string, List<Choice>> onDisplayDialogue;
   public void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
   {
      onDisplayDialogue?.Invoke(dialogueLine, dialogueChoices);
   }

   public event Action<int> onUpdateChoiceIndex;
   public void UpdateChoiceIndex(int choiceIndex)
   {
      onUpdateChoiceIndex?.Invoke(choiceIndex);
   }
   
   public event Action<Vector2, Vector2> onFaceOtherThing;
   public void FaceOtherThing(Vector2 ownPosition, Vector2 otherPosition)
   {
      onFaceOtherThing?.Invoke(ownPosition, otherPosition);
   }
}
