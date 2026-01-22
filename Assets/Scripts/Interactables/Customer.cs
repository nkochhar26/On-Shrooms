using UnityEngine;

public class Customer : MonoBehaviour, IInteractable
{
    [SerializeField] private int tableNum;

    //TODO: change recipe based on day, remove serialized field
    [SerializeField] private Recipe recipe;
    [SerializeField] private CustomerType customerType;

    public void OnInteract()
    {
        Debug.Log("Interacted with! " + this.gameObject.name);
        if (GameManager.Instance.customerManager.GetTakenOrder(tableNum))
        {
            Debug.Log("You've taken my order");
            //TODO: check held dish matching here - reputation and money calculation
            Destroy(this.gameObject);
        }
        else
        {
            GameManager.Instance.orderManager.AddOrder(tableNum, recipe);      
            SetTakenOrder(true);
        }
    }

    public CustomerType GetCustomerType()
    {
        return customerType;
    }

    public void SetTakenOrder(bool value)
    {
        GameManager.Instance.customerManager.SetTakenOrder(tableNum, value);
    }

    public void SetTableNum(int tableNum)
    {
        this.tableNum = tableNum;
    }
}