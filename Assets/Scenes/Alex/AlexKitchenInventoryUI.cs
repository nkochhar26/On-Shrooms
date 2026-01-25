using UnityEngine;
using System.Collections.Generic;

public class AlexKitchenInventoryUI : MonoBehaviour
{
    public GameObject itemUI;

    void OnEnable()
    {
        UpdateItems();
    }

    public void UpdateItems()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("Loading Items");
        Dictionary<Item, int> items = GameManager.Instance.inventoryManager.GetItems();
        foreach (Item item in items.Keys)
        {
            Instantiate(itemUI, this.gameObject.transform);
        }
    }
}
