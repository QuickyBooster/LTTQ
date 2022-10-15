using System.Collections;
using System.Collections.Generic;
using UnityEditor.DeviceSimulation;
using UnityEngine;

public class ShipSmall : MonoBehaviour
{

    Controller controller;

    float x;
    float y;
    int shipID;

    Transform shipBigPlace;
    float deltaX, deltaY;
    Vector2 mousePosition, initialPosition, standardPosition;
    bool touched;
    private void Awake()
    {
        controller = FindObjectOfType<Controller>();
        shipID = controller.getShipID();
        touched = false;
    }
    private void Start()
    {
        initialPosition = transform.position;
    }
    private void OnMouseDown()
    {
        if (!controller.isLocked())
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            controller.setID((shipID-1000), -1, false);
        }
    }
    private void OnMouseDrag()
    {
        if (!controller.isLocked())
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
        }
    }
    private void OnMouseUp()
    {
        if (!controller.isLocked())
        {
            // find the standard position of it
            int id = 0;
            float vX, vY, tX, tY, cX, cY;

            vX = transform.position.x -0.8955f;
            vY = transform.position.y +0.1885f;
            tX = -8.216f;
            for (int i = 0; i < 16; i++)
            {
                tY = 2.739f;
                for (int j = 0; j < 19; j++)
                {
                    cX= Mathf.Abs(vX-tX);
                    cY= Mathf.Abs(vY-tY);
                    if (cX<=0.185f && cY<=0.185f)
                    {
                        transform.position = new Vector2(tX+0.8955f, tY-0.1885f);
                        controller.setID(shipID-1000, id,true);
                        if (touched)
                        {
                            controller.setID(shipID-1000, -1, false);
                        }
                        return;
                    }
                    id++;
                    tY-= 0.3792f;
                }
                id++;
                tX+= 0.37825f;
            }
        }
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        touched = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
        touched = false;
    }
}
