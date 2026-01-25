using UnityEngine;

public class WorkStation : MonoBehaviour
{
    public GameObject UIElement;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIElement.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIElement.SetActive(false);
        }
    }
}
