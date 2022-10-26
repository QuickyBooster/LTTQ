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
    [SerializeField] GameObject _shipSmall;
    [SerializeField] GameObject _shipBig;
    [SerializeField] GameObject _shipMedium;


    int _playerTargetLeft;
    int _enemyTargetLeft;
    bool _enemyTurn;
    bool _lockedShipCoordinate;
    // scence = 0 pre battle
    // scence = 1 battle
    int _scence;

    //  0  = big ship
    //  1,2 = medium ship
    //  3,4,5 = small ship
    int[] _id = new int[6];
    bool[] _status = new bool[6];
    int _shipID;
    int _shipArranged;

    //ship manage
    GameObject shipBig;
    GameObject shipMedium1;
    GameObject shipMedium2;
    GameObject shipSmall1;
    GameObject shipSmall2;
    GameObject shipSmall3;
    GameObject shipBigEnemy;
    GameObject shipMediumEnemy1;
    GameObject shipMediumEnemy2;
    GameObject shipSmallEnemy1;
    GameObject shipSmallEnemy2;
    GameObject shipSmallEnemy3;

    //for disable collision2D on ships
    float _time;
    bool _disabledEnemy;
    bool _tableEnemyCreated;
    float _timeAttack;

    //array for anemy ship to spawn
    float[,] _enemySpawnPointX = new float[21, 20];
    float[,] _enemySpawnPointY = new float[21, 20];
    GameObject[,] _pointToAttack = new GameObject[21, 20];
    /// <summary>
    /// From 0 to 41 is big ship
    /// From 42 to 89 is medium ship 1 & 2
    /// From 89 to 125 is small ship 1 & 2 & 3
    /// </summary>
    int[] _enemyCoodinateID = new int[126];
    bool[] _enemyCoordinateDestroyed = new bool[126];


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        _manager = FindObjectOfType<UIManager>();
        _scence = 0;
        _shipID = 1000;
        _playerTargetLeft = _enemyTargetLeft = 126;
        _timeAttack = 2f;
        _enemyTurn = false;
        _disabledEnemy = false;
        _tableEnemyCreated = false;
        if (_scence == 0)
        {

            createShips();
            _manager.setErrortext("");
            _manager.setArrangeText("Arrange the ships");
            _shipArranged = 0;
            _time = 0.2f;
        }
    }
    private void Update()
    {
        if (_scence == 0)
        {

            _shipArranged = 0;
            for (int i = 0; i<6; i++)
            {
                if (_status[i])
                {
                    _shipArranged++;
                }

            }
            _manager.setArrangeText("Arrange the ships ("+_shipArranged+"/6)");
            _manager.setErrortext("Put ships table or rearrange (if anything is right"+
                " but the battle button doesn't appeared, you can click on ship to refresh)");
            if (_shipArranged == 6)
            {
                _manager.setErrortext("");
                _manager.showButtonBattle(true);
            }
            else
            {
                _manager.showButtonBattle(false);
            }
        }
        else if (_scence ==1)
        {
            _time -=Time.deltaTime;
            if (!_tableEnemyCreated )
            {
                createTableEnemy();
                createShipEnemy();
                _tableEnemyCreated = true;
                toggleDisplayAllShipEnemy();
            }
            if (!_disabledEnemy && _time <0)
            {
                toggleColliderShip();
                turnOffColliderShipEnemy();
                _disabledEnemy = true;
            }
            if (!(_enemyTargetLeft == 0 || _playerTargetLeft == 0))
            {
                if (_enemyTurn)
                {
                    if (_timeAttack < 0)
                    {
                        enemyAttack();
                        _timeAttack = 0.5f;
                    }
                    else
                    {
                        _timeAttack -= Time.deltaTime;
                    }
                }
                else
                    _timeAttack = 0.5f;


            }
            else
            {
                if (_enemyTargetLeft == 0)
                {
                    // enemy win
                    print("enemy win");
                }
                else
                {
                    print("player win");
                    //player win
                }
            }
        }
    }
    bool enemyAttack()
    {
        int rdX = Random.Range(0, 20);
        int rdY = Random.Range(0, 19);
        if (_pointToAttack[rdX, rdY])
        {

            if (_pointToAttack[rdX, rdY].GetComponent<Point>().Destroyed())
                return false;
            else
            {
                if (_pointToAttack[rdX, rdY].GetComponent<Point>().isBeingAttack())
                {
                    int idHit = rdX*20+rdY;
                    _enemyTargetLeft--;
                    return true;
                }
            }
        }
        return false;

    }
    bool checkEnemyShipDestroyed(int id)
    {

        int start, end,idShip;
        if (id < 42)
        {
            start = 0;
            end =42;
            idShip = 0;
        }else
        {
            if (id <66)
            {
                start =42;
                end = 66;
                idShip =1;
            }else
            {
                if (id <89)
                {
                    start = 66;
                    end = 89;
                    idShip =2;
                }else
                {
                    if (id <101)
                    {
                        start = 89;
                        end =102;
                        idShip =3;
                    }else
                    {
                        if (id < 113)
                        {
                            start = 102;
                            end = 113;
                            idShip =4;
                        }else{
                            start = 113;
                            end = 126;
                            idShip =5;
                        }
                    }
                }
            }
        }

        for (int i = start; i < end; i++)
            if (!_enemyCoordinateDestroyed[i])
                return false;
        toggleDisplayShipEnemy(idShip);
        return true;
    }
    void toggleColliderShip()
    {
        shipBig.GetComponent<Collider2D>().enabled =  !shipBig.GetComponent<Collider2D>().enabled;
        shipMedium1.GetComponent<Collider2D>().enabled = !shipMedium1.GetComponent<Collider2D>().enabled;
        shipMedium2.GetComponent<Collider2D>().enabled = !shipMedium2.GetComponent<Collider2D>().enabled;
        shipSmall1.GetComponent<Collider2D>().enabled = !shipSmall1.GetComponent<Collider2D>().enabled;
        shipSmall2.GetComponent<Collider2D>().enabled = !shipSmall2.GetComponent<Collider2D>().enabled;
        shipSmall3.GetComponent<Collider2D>().enabled = !shipSmall3.GetComponent<Collider2D>().enabled;
    }
    void toggleDisplayAllShipEnemy()
    {
        shipBigEnemy.GetComponent<SpriteRenderer>().enabled =
            !shipBigEnemy.GetComponent<SpriteRenderer>().enabled;
        shipMediumEnemy1.GetComponent<SpriteRenderer>().enabled =
            !shipMediumEnemy1.GetComponent<SpriteRenderer>().enabled;
        shipMediumEnemy2.GetComponent<SpriteRenderer>().enabled =
            !shipMediumEnemy2.GetComponent<SpriteRenderer>().enabled;
        shipSmallEnemy1.GetComponent<SpriteRenderer>().enabled =
            !shipSmallEnemy1.GetComponent<SpriteRenderer>().enabled;
        shipSmallEnemy2.GetComponent<SpriteRenderer>().enabled =
            !shipSmallEnemy2.GetComponent<SpriteRenderer>().enabled;
        shipSmallEnemy3.GetComponent<SpriteRenderer>().enabled =
            !shipSmallEnemy3.GetComponent<SpriteRenderer>().enabled;
    }
    void toggleDisplayShipEnemy(int id)
    {
        switch (id)
        {
            case 0:
                {
                    shipBigEnemy.GetComponent<SpriteRenderer>().enabled =
                        !shipBigEnemy.GetComponent<SpriteRenderer>().enabled;
                    break;
                }
            case 1:
                {
                    shipMediumEnemy1.GetComponent<SpriteRenderer>().enabled =
                        !shipMediumEnemy1.GetComponent<SpriteRenderer>().enabled;
                    break;
                }
            case 2:
                {
                    shipMediumEnemy2.GetComponent<SpriteRenderer>().enabled =
                        !shipMediumEnemy2.GetComponent<SpriteRenderer>().enabled;
                    break;
                }
            case 3:
                {
                    shipSmallEnemy1.GetComponent<SpriteRenderer>().enabled =
                        !shipSmallEnemy1.GetComponent<SpriteRenderer>().enabled;
                    break;
                }
            case 4:
                {
                    shipSmallEnemy2.GetComponent<SpriteRenderer>().enabled =
                        !shipSmallEnemy2.GetComponent<SpriteRenderer>().enabled;
                    break;
                }
            case 5:
                {
                    shipSmallEnemy3.GetComponent<SpriteRenderer>().enabled =
            !shipSmallEnemy3.GetComponent<SpriteRenderer>().enabled;
                    break;
                }
        }
        print("bat roi");
    }
    void turnOffColliderShipEnemy()
    {
        //diable collider2D
        shipBigEnemy.GetComponent<Collider2D>().enabled = false;
        shipMediumEnemy1.GetComponent<Collider2D>().enabled = false;
        shipMediumEnemy2.GetComponent<Collider2D>().enabled = false;
        shipSmallEnemy1.GetComponent<Collider2D>().enabled = false;
        shipSmallEnemy2.GetComponent<Collider2D>().enabled = false;
        shipSmallEnemy3.GetComponent<Collider2D>().enabled = false;

    }
    void createShips()
    {
        float x;
        float y;
        //generate ships
        x=3.65f;
        y= 1.25f;
        shipBig = Instantiate(_shipBig, new Vector2(x, y), Quaternion.identity);
        x = 4.15f;
        y = 0.05f;
        shipMedium1 = Instantiate(_shipMedium, new Vector2(x, y), Quaternion.identity);
        shipMedium2 = Instantiate(_shipMedium, new Vector2(x, y), Quaternion.identity);
        x=5.12f;
        y =-2.91f;
        shipSmall1 = Instantiate(_shipSmall, new Vector2(x, y), Quaternion.identity);
        shipSmall2 = Instantiate(_shipSmall, new Vector2(x, y), Quaternion.identity);
        shipSmall3 = Instantiate(_shipSmall, new Vector2(x, y), Quaternion.identity);

    }
    void createShipEnemy()
    {
        Vector2 temp;
        int[] deniedPosX = new int[6];
        int[] deniedPosY = new int[6];
        for (int i = 0; i < 6; i++)
        {
            deniedPosX[i] = -1;
            deniedPosY[i] = -1;
        }
        // create big ship
        temp = generatePoint(deniedPosX, deniedPosY, 7, 17, 0);
        deniedPosX[0]  = (int)temp.x;
        deniedPosY[0]  = (int)temp.y;
        shipBigEnemy =Instantiate(_shipBig, new Vector2(_enemySpawnPointX[deniedPosX[0], deniedPosY[0]] + 2.4445f,
            _enemySpawnPointY[deniedPosX[0], deniedPosY[0]] - 0.3745f), Quaternion.identity);
        for (int i = 0; i < 14; i++)
            for (int j = 0; j<3; j++)
                _enemyCoodinateID[i*3+j] = -((deniedPosX[0]+i)*20+deniedPosY[0]+j+2);

        //create medium ship

        temp = generatePoint(deniedPosX, deniedPosY, 9, 18, 1);
        deniedPosX[1]  = (int)temp.x;
        deniedPosY[1] = (int)temp.y;
        shipMediumEnemy1 = Instantiate(_shipMedium, new Vector2(_enemySpawnPointX[deniedPosX[1], deniedPosY[1]] + 2.0835f,
            _enemySpawnPointY[deniedPosX[1], deniedPosY[1]] - 0.1885f), Quaternion.identity);
        for (int i = 14; i < 26; i++)
            for (int j = 0; j<2; j++)
                _enemyCoodinateID[i*2+j+14] = -((deniedPosX[1]+i-14)*20+deniedPosY[1]+j+2);


        temp = generatePoint(deniedPosX, deniedPosY, 9, 18, 1);
        deniedPosX[2]  = (int)temp.x;
        deniedPosY[2]  = (int)temp.y;
        shipMediumEnemy2 = Instantiate(_shipMedium, new Vector2(_enemySpawnPointX[deniedPosX[2], deniedPosY[2]] + 2.0835f,
            _enemySpawnPointY[deniedPosX[2], deniedPosY[2]] - 0.1885f), Quaternion.identity);
        for (int i = 26; i < 38; i++)
            for (int j = 0; j<2; j++)
                _enemyCoodinateID[i*2+j+14] = -((deniedPosX[2]+i-26)*20+deniedPosY[2]+j+2);


        //create small ship

        temp = generatePoint(deniedPosX, deniedPosY, 15, 18, 2);
        deniedPosX[3]  = (int)temp.x;
        deniedPosY[3]  = (int)temp.y;
        shipSmallEnemy1 = Instantiate(_shipSmall, new Vector2(_enemySpawnPointX[deniedPosX[3], deniedPosY[3]] + 0.94f,
            _enemySpawnPointY[deniedPosX[3], deniedPosY[3]] - 0.1885f), Quaternion.identity);
        for (int i = 38; i < 44; i++)
            for (int j = 0; j<2; j++)
                _enemyCoodinateID[i*2+j+14] = -((deniedPosX[3]+i-38)*20+deniedPosY[3]+j+2);


        temp = generatePoint(deniedPosX, deniedPosY, 15, 18, 2);
        deniedPosX[4]  = (int)temp.x;
        deniedPosY[4]  = (int)temp.y;
        shipSmallEnemy2 = Instantiate(_shipSmall, new Vector2(_enemySpawnPointX[deniedPosX[4], deniedPosY[4]] + 0.94f,
            _enemySpawnPointY[deniedPosX[4], deniedPosY[4]] - 0.1885f), Quaternion.identity);
        for (int i = 44; i < 50; i++)
            for (int j = 0; j<2; j++)
                _enemyCoodinateID[i*2+j+14] = -((deniedPosX[4]+i-44)*20+deniedPosY[4]+j+2);


        temp = generatePoint(deniedPosX, deniedPosY, 15, 18, 2);
        deniedPosX[5]  = (int)temp.x;
        deniedPosY[5]  = (int)temp.y;
        shipSmallEnemy3 = Instantiate(_shipSmall, new Vector2(_enemySpawnPointX[deniedPosX[5], deniedPosY[5]] + 0.94f,
            _enemySpawnPointY[deniedPosX[5], deniedPosY[5]] - 0.1885f), Quaternion.identity);
        for (int i = 50; i < 56; i++)
            for (int j = 0; j<2; j++)
                _enemyCoodinateID[i*2+j+14] = -((deniedPosX[5]+i-50)*20+deniedPosY[5]+j+2);


    }
    Vector2 generatePoint(int[] valueX, int[] valueY, int rangeX, int rangeY, int typeShip)
    {
        Vector2 result;
        // 0 = width, 1 = height
        result.x = Random.Range(0, rangeX);
        result.y = Random.Range(0, rangeY);
        bool flag = false;
        for (int i = 0; i<6; i++)
        {
            flag = false;
            // create shipMediumEnemy 1 & 2
            if (typeShip == 1)
            {

                if (i ==0)
                {
                    while ((result.x - valueX[i] >= 0 && result.x - valueX[i] < 15 &&
                        result.y - valueY[i] >= 0 && result.y - valueY[i] < 4)||
                        (result.x + 12 - valueX[i] >=0 && result.x + 12 - valueX[i] < 15 &&
                        result.y - valueY[i] >= 0 && result.y - valueY[i] < 4)||
                        (result.x - valueX[i] >=0 && result.x - valueX[i] < 15 &&
                        result.y + 2 - valueY[i] >= 0 && result.y + 2 - valueY[i] < 4) ||
                        (result.x + 12 - valueX[i] >=0 && result.x + 12 - valueX[i] < 15 &&
                        result.y + 2 - valueY[i] >= 0 && result.y + 2 - valueY[i] < 4))
                    {
                        result[0] = Random.Range(0, rangeX);
                        result[1] = Random.Range(0, rangeY);
                        flag = true;
                    }
                }
                else if (i <3)
                {
                    while ((result.x - valueX[i] >= 0 && result.x - valueX[i] < 13 &&
                        result.y - valueY[i] >= 0 && result.y - valueY[i] < 3)||
                        (result.x + 12 - valueX[i] >=0 && result.x + 12 - valueX[i] < 13 &&
                        result.y - valueY[i] >= 0 && result.y - valueY[i] < 3) ||
                        (result.x - valueX[i] >=0 && result.x - valueX[i] < 13 &&
                        result.y + 2 - valueY[i] >= 0 && result.y + 2 - valueY[i] < 3)||
                        (result.x + 12 - valueX[i] >=0 && result.x + 12 - valueX[i] < 13 &&
                        result.y + 2 - valueY[i] >= 0 && result.y + 2 - valueY[i] < 3))
                    {
                        result[0] = Random.Range(0, rangeX);
                        result[1] = Random.Range(0, rangeY);
                        flag = true;
                    }
                }
            }

            //generate shipSmallEnemy 1&2&3
            else if (typeShip ==2)
            {
                if (i ==0)
                {
                    while ((result.x - valueX[i] >= 0 && result.x - valueX[i] < 15 &&
                        result.y - valueY[i] >= 0 && result.y - valueY[i] < 4) ||
                        (result.x + 6 - valueX[i] >=0 && result.x + 6 - valueX[i] < 15 &&
                        result.y - valueY[i] >= 0 && result.y - valueY[i] < 4) ||
                        (result.x - valueX[i] >=0 && result.x - valueX[i] < 15 &&
                        result.y + 2 - valueY[i] >= 0 && result.y + 2 - valueY[i] < 4) ||
                        (result.x + 6 - valueX[i] >=0 && result.x + 6 - valueX[i] < 15 &&
                        result.y + 2 - valueY[i] >= 0 && result.y + 2 - valueY[i] < 4))
                    {
                        result[0] = Random.Range(0, rangeX);
                        result[1] = Random.Range(0, rangeY);
                        flag = true;
                    }
                }
                else if (i <3)
                {
                    while ((result.x - valueX[i] >= 0 && result.x - valueX[i] < 13 &&
                        result.y - valueY[i] >= 0 && result.y - valueY[i] < 3)||
                        (result.x + 6 - valueX[i] >=0 && result.x + 6 - valueX[i] < 13 &&
                        result.y - valueY[i] >= 0 && result.y - valueY[i] < 3)||
                        (result.x - valueX[i] >=0 && result.x - valueX[i] < 13 &&
                        result.y + 2 - valueY[i] >= 0 && result.y + 2 - valueY[i] < 3) ||
                        (result.x + 6 - valueX[i] >=0 && result.x + 6 - valueX[i] < 13 &&
                        result.y + 2 - valueY[i] >= 0 && result.y + 2 - valueY[i] < 3))
                    {
                        result[0] = Random.Range(0, rangeX);
                        result[1] = Random.Range(0, rangeY);
                        flag = true;
                    }
                }
                else
                {
                    while ((result.x - valueX[i] >= 0 && result.x - valueX[i] < 7 &&
                        result.y - valueY[i] >= 0 && result.y - valueY[i] < 3)||
                        (result.x + 6 - valueX[i] >=0 && result.x + 6 - valueX[i] < 7 &&
                        result.y - valueY[i] >= 0 && result.y - valueY[i] < 3)||
                        (result.x - valueX[i] >=0 && result.x - valueX[i] < 7 &&
                        result.y + 2 - valueY[i] >= 0 && result.y + 2 - valueY[i] < 3) ||
                        (result.x + 6 - valueX[i] >=0 && result.x + 6 - valueX[i] < 7 &&
                        result.y + 2 - valueY[i] >= 0 && result.y + 2 - valueY[i] < 3))
                    {
                        result[0] = Random.Range(0, rangeX);
                        result[1] = Random.Range(0, rangeY);
                        flag = true;
                    }

                }
            }
            if (flag)
                i = -1;
        }
        return result;
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
                _pointToAttack[i, j] = destroyPoint;
                y-= 0.3792f;
                id++;
            }
            x+= 0.37825f;
        }
    }
    void createTableEnemy()
    {
        int id = -1;
        float x, y;
        x = 0.937f;
        for (int i = 0; i < 21; i++)
        {
            y = 2.739f;
            for (int j = 0; j < 20; j++)
            {
                GameObject pointCreated = Instantiate(_pointEnemy, new Vector2(x, y), Quaternion.identity);
                pointCreated.name = id.ToString();
                y-= 0.3792f;
                _enemySpawnPointX[i, j] = x;
                _enemySpawnPointY[i, j] = y;
                id--;
            }
            x+= 0.37825f;
        }
    }


    public bool isLocked() { return _lockedShipCoordinate; }
    public void setLocked(bool set)
    {
        _lockedShipCoordinate = set;
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
            _playerTargetLeft--;
            _enemyTurn = !_enemyTurn;
        }
    }
    public void setID(int ship, int id, bool status)
    {
        _id[ship] = id;
        _status[ship] = status;
    }
    public int getShipID()
    {
        return _shipID++;
    }
    public void navigateBattle()
    {
        if (_shipArranged == 6)
        {

            createTable();
            _scence = 1;
            _lockedShipCoordinate = true;
            SceneManager.LoadScene("Battle");


        }
    }
    public void returnPointHit(int idHit)
    {
        for (int i = 0; i<126; i++)
        {
            if (idHit == _enemyCoodinateID[i])
            {
                _enemyCoordinateDestroyed[i] = true;
                checkEnemyShipDestroyed(i);
                return;
            }
        }
    }
}
