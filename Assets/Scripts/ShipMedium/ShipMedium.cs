using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMedium : MonoBehaviour
{
    Vector3 point;
    float x;
    float y;
    float z;
    private void OnMouseDrag()
    {
        Debug.Log("mouse is dragging it now");
        x = Input.mousePosition.x;
        y = Input.mousePosition.y;
        z = (Camera.main.transform.position-gameObject.transform.position).magnitude;

        point = Camera.main.ScreenToWorldPoint(new Vector3(x, y, z));
        gameObject.transform.position = point;

    }
    private void OnMouseOver()
    {
    }
}
