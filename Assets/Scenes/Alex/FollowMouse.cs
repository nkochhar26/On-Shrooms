using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousepos.x, mousepos.y, transform.position.z);
    }
}
