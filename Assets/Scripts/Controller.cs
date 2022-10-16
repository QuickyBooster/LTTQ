using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [SerializeField] UIManager _manager;

    [SerializeField] GameObject _point;
    [SerializeField] GameObject _shipSmall;
    [SerializeField] GameObject _shipBig;
    [SerializeField] GameObject _shipMedium;


    bool _enemyTurn; //false = player , true = enemy
    //not allow to move ship
    bool _locked;
    // scence = 0 pre battle
    // scence = 1 battle
    int _scence;

    //  0  = big ship
    //  1,2 = medium ship
    //  3,4,5 = small ship
    int[] _id = new int[6];
    bool[] _status = new bool[6];
    int _shipID;
    int _done;

    //ship manage
    GameObject shipBig;
    GameObject shipMedium1;
    GameObject shipMedium2;
    GameObject shipSmall1;
    GameObject shipSmall2;
    GameObject shipSmall3;

    //for disable collision2D on ships
    float _time;
    bool _disabled;

    private void Awake()
    {
        _scence = 0;
        _enemyTurn = true;
        _shipID=1000;
        _manager = FindObjectOfType<UIManager>();
        DontDestroyOnLoad(this.gameObject);
        _disabled = false;
    }
    private void Start()
    {
        if (_scence == 0)
        {

            createShips();
            _manager.setErrortext("");
            _manager.setArrangeText("Arrange the ships");
            _done =0;
            _time = 0.5f;
        }
    }
    private void Update()
    {
        if (_scence == 0)
        {

            _done = 0;
            for (int i = 0; i<6; i++)
            {
                if (_status[i])
                {
                    _done++;
                }

            }
            _manager.setArrangeText("Arrange the ships ("+_done+"/6)");
            _manager.setErrortext("Put ships table or rearrange (if anything is right"+
                " but the battle button doesn't appeared, you can click on ship to refresh)");
            if (_done == 6)
            {
                _manager.setErrortext("");
                _manager.showButtonBattle(true);
            }
            else
            {
                _manager.showButtonBattle(false);
            }
        }else if (_scence ==1)
        {
            //time = 0.5f
            //disabled = false : kiem tra da disble collider2D chua
            _time -=Time.deltaTime;
            if (!_disabled  && _time <0)
            {
                print("yayy");
                disShip();
                _disabled = true;
            }
        }
    }

    void disShip()
    {
        shipBig.GetComponent<Collider2D>().enabled = false;
        shipMedium1.GetComponent<Collider2D>().enabled = false;
        shipMedium2.GetComponent<Collider2D>().enabled = false;
        shipSmall1.GetComponent<Collider2D>().enabled = false;
        shipSmall2.GetComponent<Collider2D>().enabled = false;
        shipSmall3.GetComponent<Collider2D>().enabled = false;
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
        if (_done == 6)
        {

            createTable();
            _scence = 1;
            _locked = true;
            SceneManager.LoadScene("Battle");
            

        }
    }
}
