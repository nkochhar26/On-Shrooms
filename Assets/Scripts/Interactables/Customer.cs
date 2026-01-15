using UnityEngine;

public class Customer : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        //DialogueSystem.TriggerDialogue();
        Debug.Log("Interacted with! " + this.gameObject.name);
    }
}