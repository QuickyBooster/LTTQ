using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject _point;
    [SerializeField] GameObject _shipSmall;
    [SerializeField] GameObject _shipBig;
    [SerializeField] GameObject _shipMedium;

    bool _enemyTurn; //false = player , true = enemy
    bool _locked;
    int[] _id = new int[6] ;
    bool[] _status = new bool[6];
    int _shipID;


    private void Awake()
    {
        _enemyTurn = true;
        _shipID=1000;
        
    }
    private void Start()
    {
        createShips();
    }
    void createShips()
    {
        float x;
        float y;
        //generate ships
        x=3.65f;
        y= 3.05f;
        GameObject shipBig = Instantiate(_shipBig, new Vector2(x, y), Quaternion.identity);
        x = 4.15f;
        y = 0.05f;
        GameObject shipMedium1 = Instantiate(_shipMedium, new Vector2(x, y), Quaternion.identity);
        GameObject shipMedium2 = Instantiate(_shipMedium, new Vector2(x, y), Quaternion.identity);
        x=5.12f;
        y =-2.91f;
        GameObject shipSmall1 = Instantiate(_shipSmall, new Vector2(x, y), Quaternion.identity);
        GameObject shipSmall2 = Instantiate(_shipSmall, new Vector2(x, y), Quaternion.identity);
        GameObject shipSmall3 = Instantiate(_shipSmall, new Vector2(x, y), Quaternion.identity);

    }

        //generate points in table
    void createTable() 
    { 
        int id = 0;
        float x, y;
        x = -8.216f;
        for (int i = 0; i < 21; i++)
        {
            y = 2.739f;
            for (int j = 0; j < 20; j++)
            {
                GameObject destroyPoint = Instantiate(_point, new Vector2(x, y), Quaternion.identity);
                destroyPoint.name = id.ToString();
                y-= 0.3792f;
                id++;
            }
            x+= 0.37825f;
        }
    }

    public bool isLocked() { return _locked; }
    public void setLocked(bool set)
    {
        _locked = set;
    }
    public bool isEnemyTurn()
    {
        return _enemyTurn;
    }
    public void setEnemyTurn(bool set)
    {
        _enemyTurn =set;
    }
    public void setID(int ship,int id,bool status)
    {
        _id[ship] = id;
        _status[ship] = status;
    }
    public int getShipID()
    {
        return _shipID++;
    }

}