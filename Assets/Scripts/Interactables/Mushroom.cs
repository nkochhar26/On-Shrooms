using UnityEngine;

public class Mushroom : MonoBehaviour, IInteractable
{
    [SerializeField] private Item mushroomItem;
    public void OnInteract()
    {
        //DialogueSystem.TriggerDialogue();
        Debug.Log("Interacted with! " + this.gameObject.name);
    }


    //later make to on click
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //add to inventory
            GameManager.Instance.inventoryManager.AddItem(mushroomItem);
            Destroy(this.gameObject);
        }
    }
}