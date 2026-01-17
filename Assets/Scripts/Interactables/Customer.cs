using UnityEngine;

public class Customer : MonoBehaviour, IInteractable
{
    [SerializeField] private int tableNum;
    [SerializeField] private Recipe recipe; 

    public void OnInteract()
    {
        //DialogueSystem.TriggerDialogue();
        Debug.Log("Interacted with! " + this.gameObject.name);
        if (GameManager.Instance.orderManager.GetHeldOrder() != null)
        {
            Destroy(this.gameObject);
        }
        GameManager.Instance.orderManager.AddOrder(tableNum, recipe);
    }
}