using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool _turn = true; //false = player , true = enemy
    [SerializeField] GameObject _point;
    private void Start()
    {
        createTable();
    }
    void createTable()
    {
        int id = 0;
        float x = -8.216f;
        for (int i = 0; i < 21; i++)
        {
            float y = 2.739f;
            for (int j = 0; j < 20; j++)
            {
                GameObject destroyPoint = Instantiate(_point,new Vector2(x, y), Quaternion.identity);
                destroyPoint.name = id.ToString();
                y-= 0.3792f;
                id++;
            }
            x+= 0.37825f;
        }

    }
}
