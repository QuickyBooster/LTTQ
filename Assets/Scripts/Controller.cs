using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    [SerializeField] UIManager _manager;
    [SerializeField] GameObject _point;
    [SerializeField] GameObject _pointEnemy;
    Ship ship;



    bool _enemyTurn;
    bool _lockedShipCoordinate;
    bool _shipInPlace;
    bool _cardChose;
    bool _disabledShip;
    // scence = 0 pre battle
    // scence = 1 battle
    int _scence;

    //ship manager

    // timer
    float timer;

    bool _tableCreated;

    //array for enemy ship to spawn
    float[,] _enemySpawnPointX = new float[5, 5];
    float[,] _enemySpawnPointY = new float[5, 5];
    GameObject[,] _pointToAttack = new GameObject[5, 5];
    int firstID = 0;
    

    private void Start()
    {
        ship = FindObjectOfType<Ship>();
        DontDestroyOnLoad(this.gameObject);
        _manager = FindObjectOfType<UIManager>();
        _scence = 0;
        _enemyTurn = false;
        _tableCreated = false;
        _shipInPlace = false;
        _cardChose = true;
        _disabledShip = false;
        firstID = 0;
        Application.targetFrameRate = 60;
        if (_scence == 0)
        {
            _manager.setArrangeText("Put ship into table and choose your card you will bring!");
        }
    }
    private void Update()
    {
        if (_scence == 0 )
        {
            if (_shipInPlace && _cardChose)
                _manager.showButtonBattle(true);
            else
                _manager.showButtonBattle(false);
        }
        

        if (_scence == 1 && !_tableCreated)
        {
            createTable();
            createTableEnemy();
            _tableCreated=true;
            ship.toggleCollider();
            _disabledShip = true;
        }

    }
    
    
    
    void createTable()
    {
        int id = 0;
        float x, y;
        x = -8.3325f;
        for (int i = 0; i < 5; i++)
        {
            y = 0.172f;
            for (int j = 0; j < 5; j++)
            {
                GameObject destroyPoint = Instantiate(_point, new Vector2(x, y), Quaternion.identity);
                destroyPoint.name = id.ToString();
                _pointToAttack[i, j] = destroyPoint;
                if (_pointToAttack[i, j].GetComponent<Point>()!= null)
                    print("true");
                y-= 0.7197f;
                id++;
            }
            x+= 0.7198f;
        }
        _pointToAttack[firstID/5, firstID%5].GetComponent<Point>().setShipField(true);
        _pointToAttack[firstID/5+1, firstID%5].GetComponent<Point>().setShipField(true);
        _pointToAttack[firstID/5+2, firstID%5].GetComponent<Point>().setShipField(true);
    }
    void createTableEnemy()
    {
        int id = -1;
        float x, y;
        x = 5.4734f;
        for (int i = 0; i < 5; i++)
        {
            y = 0.1798f;
            for (int j = 0; j < 5; j++)
            {
                GameObject pointCreated = Instantiate(_pointEnemy, new Vector2(x, y), Quaternion.identity);
                pointCreated.name = id.ToString();
                y-= 0.7197f;
                _enemySpawnPointX[i, j] = x;
                _enemySpawnPointY[i, j] = y;
                id--;
            }
            x+= 0.7202f;
        }
    }


    public bool isLocked() { return _lockedShipCoordinate; }
    public void setLockedCoordinate(bool set)
    {
        _lockedShipCoordinate = set;
    }
    public bool isLockedCoordinate()
    {
        return _lockedShipCoordinate;
    }
    public bool isEnemyTurn()
    {
        return _enemyTurn;
    }
    public void toggleEnemyTurn(bool playerHit)
    {
        _enemyTurn =!_enemyTurn;
        //true means player hit a enemy ship, false means opposite
        if (playerHit)
        {
            _enemyTurn = !_enemyTurn;
        }
    }
    public void setShipInPlace(bool status,int where)
    {
        
        _shipInPlace = status;
        if (status)
            firstID = where;
    }
    public bool isShipInPlace()
    {
        return _shipInPlace;
    }
    public void navigateBattle()
    {
        if (_shipInPlace && _cardChose)
        {

            _scence = 1;
            //_lockedShipCoordinate = true;
            SceneManager.LoadScene("Battle");
            timer = 0.5f;

            
        }
    }
    public void returnPointHit(int idHit)
    {
        for (int i = 0; i<126; i++)
        {
            //if (idHit == _enemyCoodinateID[i])
            //{
            //    _enemyCoordinateDestroyed[i] = true;
            //    checkEnemyShipDestroyed(i);
            //    return;
            //}
        }
    }


}
