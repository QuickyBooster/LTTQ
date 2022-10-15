using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool _turn = true; //false = player , true = enemy
    [SerializeField] GameObject _point;
    [SerializeField] GameObject _shipSmall;
    [SerializeField] GameObject _shipBig;
    [SerializeField] GameObject _shipMedium;
    private void Start()
    {
        createTable();
        _turn = true;
    }
    void createTable()
    {
        int id = 0;
        float x = -8.216f;
        float y;
        for (int i = 0; i < 21; i++)
        {
            y = 2.739f;
            for (int j = 0; j < 20; j++)
            {
                GameObject destroyPoint = Instantiate(_point,new Vector2(x, y), Quaternion.identity);
                destroyPoint.name = id.ToString();
                y-= 0.3792f;
                id++;
            }
            x+= 0.37825f;
        }
        x=3.65f;
        y= 3.05f;
        GameObject shipBig = Instantiate(_shipBig, new Vector2(x, y), Quaternion.identity);
        shipBig.name = "shipBig";     
        x = 4.15f;
        y = 0.05f;
        GameObject shipMedium1 = Instantiate(_shipMedium, new Vector2(x, y), Quaternion.identity);
        shipMedium1.name ="shipMedium1";
        GameObject shipMedium2 = Instantiate(_shipMedium, new Vector2(x, y), Quaternion.identity);
        shipMedium2.name ="shipMedium2";
        x=5.12f;
        y =-2.91f;
        GameObject shipSmall1 = Instantiate(_shipSmall,new Vector2(x,y),Quaternion.identity);
        shipSmall1.name = "shipSmall1";
        GameObject shipSmall2 = Instantiate(_shipSmall, new Vector2(x, y), Quaternion.identity);
        shipSmall2.name = "shipSmall2";
        GameObject shipSmall3 = Instantiate(_shipSmall, new Vector2(x, y), Quaternion.identity);
        shipSmall3.name = "shipSmall3";
    }
}
