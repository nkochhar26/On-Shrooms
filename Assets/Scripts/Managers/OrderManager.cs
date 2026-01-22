using UnityEngine;
using System.Collections.Generic;


public class OrderManager : MonoBehaviour
{
    // public Dictionary<int, Recipe> currentOrders = new Dictionary<int, Recipe>();
    public Recipe[] currentOrders = new Recipe[5]; // hardcoded at 5 rn - can be changed if needed
    public Recipe heldOrder;

    public void AddOrder(int tableNum, Recipe recipe)
    {
        currentOrders[tableNum] = recipe;
    }

    public void RemoveOrder(int tableNum)
    {
        currentOrders[tableNum] = null;
    }

    public void SetHeldOrder(Recipe heldOrder)
    {
        this.heldOrder = heldOrder;
    }

    public void RemoveHeldOrlder()
    {
        heldOrder = null;
    }

    public Recipe GetRecipe(int tableNum)
    {
        return currentOrders[tableNum];
    }

    public Recipe GetHeldOrder()
    {
        return heldOrder;
    }
}
