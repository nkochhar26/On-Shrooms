using UnityEngine;

public class Cooking : MonoBehaviour
{
    //temp just for demo and debug probably
    public void MakeFirstDish()
    {
        Recipe recipe = GameManager.Instance.orderManager.GetRecipe(0);
        GameManager.Instance.orderManager.SetHeldOrder(recipe);
    }
}
