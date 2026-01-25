using UnityEngine;
using System.Collections.Generic;

public class AlexGameManager : MonoBehaviour
{
    public static AlexGameManager Instance { get; private set; }
    public OrderManager orderManager;
    public InventoryManager inventoryManager;
    public CustomerManager customerManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
