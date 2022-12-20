using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [SerializeField] UIManager _manager;
    [SerializeField] GameObject _point;
    [SerializeField] GameObject _pointEnemy;
    [SerializeField] Sprite _bracket;
 
    CardFunction cardFunction;
    CardManager cardManager;
    Ship ship;
    PointFunction pointFunction;    


    bool _enemyTurn;
    bool _cardChose;
    bool _disabledShip;


    //array for enemy ship to spawn
    GameObject[,] _pointToAttack = new GameObject[5, 5];
    GameObject[,] _enemyPointAttack = new GameObject[5, 5];
    int firstID = 0;
    int HPleft;
    bool usingCard;
    int cardID;
    //so turn de torpedo no?
    int turns_left = 3;
    private void Start()
    {
        HPleft = 3;
        usingCard = false;
        ship = FindObjectOfType<Ship>();
        DontDestroyOnLoad(this.gameObject);
        _manager = FindObjectOfType<UIManager>();

        _cardChose = true;
        _disabledShip = false;
        firstID = 0;
        Application.targetFrameRate = 60;
        _manager.setArrangeText("Put ship into table and choose your card you will bring!");
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable called!");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        Debug.Log("OnDisable called!");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        if (scene.buildIndex == 3)
        {
            ship.setLockedCoordinate(true);
            createTable();
            createTableEnemy();
            ship.toggleCollider();
            while(!cardManager)
                cardManager = FindObjectOfType<CardManager>();
            while (!cardFunction)
                cardFunction = FindObjectOfType<CardFunction>();
            while (!pointFunction)
                pointFunction = FindObjectOfType<PointFunction>();
        }
    }
    private void Update()
    {
        //if (SceneManager.GetActiveScene().name.Equals("PreBattle"))
        //{
        //    if (_shipInPlace && _cardChose)
        //        _manager.showButtonBattle(true);
        //    else
        //        _manager.showButtonBattle(false);
        //}


        //if (SceneManager.GetActiveScene().name.Equals("Battle") && !_tableCreated && !_disabledShip)
        //{
        //    createTable();
        //    createTableEnemy();
        //    _tableCreated=true;
        //    ship.toggleCollider();
        //    _disabledShip = true;
        //}
        //if (!cardManager && SceneManager.GetActiveScene().name.Equals("Battle"))
        //{
        //    cardManager = FindObjectOfType<CardManager>();
        //}
        //if (!cardFunction && SceneManager.GetActiveScene().name.Equals("Battle"))
        //{
        //    cardFunction = FindObjectOfType<CardFunction>();
        //}
        //if (!pointFunction && SceneManager.GetActiveScene().name.Equals("Battle"))
        //{
        //    pointFunction = FindObjectOfType<PointFunction>();
        //}

    }

    //void LoadedScence() ??
    //{
    //    while (!_tableCreated && !_disabledShip)
    //        if (SceneManager.GetActiveScene().name.Equals("Battle"))
    //        {
    //            createTable();
    //            createTableEnemy();
    //            _tableCreated = true;
    //            ship.toggleCollider();
    //            _disabledShip = true;
    //        }
    //    while (!cardManager)
    //        if (SceneManager.GetActiveScene().name.Equals("Battle"))
    //            cardManager = FindObjectOfType<CardManager>();
    //    while (!cardFunction)
    //        if (SceneManager.GetActiveScene().name.Equals("Battle"))
    //            cardFunction = FindObjectOfType<CardFunction>();
    //    while (!pointFunction)
    //        if (SceneManager.GetActiveScene().name.Equals("Battle"))
    //            pointFunction = FindObjectOfType<PointFunction>();
    //}

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
                _enemyPointAttack[i, j] = pointCreated;
                y-= 0.7197f;
                id--;
            }
            x+= 0.7202f;
        }
    }

    public bool isEnemyTurn()
    {
        return _enemyTurn;
    }
    public void toggleEnemyTurn()
    {
        _enemyTurn =!_enemyTurn;

    }
    public void setShipInPlace(bool status, int where)
    {
        if (status)
        {
            firstID = where;
        }
    }
    public bool returnPointHit(int idHit)
    {
        if (idHit == -1)
            return false;
        if (_pointToAttack[idHit/5, idHit%5].GetComponent<Point>().isBeingAttack())
        {
            HPleft--;
            return true;
        }
        return false;
    }
    public void isEnemyDown(int id, bool status)
    {
        if (id == -1) return;
        _enemyPointAttack[id/5, id%5].GetComponent<PointEnemy>().displayDestroy(status);
    }
    public void sendIDToAttack(int id)
    {
        pointFunction.managerAllPoint(id);
        cardFunction.setNextTurn();
    }
    public void toggleUsingCard(int id)
    {
        usingCard = !usingCard;
        cardID   = id;
    }
    public bool isUsingCard()
    {
        return usingCard;
    }
    public int IDCardUsing()
    {
        return cardID;
    }
    public bool card001(int fakeID)
    {
        int id = -fakeID-1;
        _enemyPointAttack[id/5, id%5].GetComponent<SpriteRenderer>().sprite = _bracket;
        if (id/5 <4)
        {
            _enemyPointAttack[id/5+1, id%5].GetComponent<SpriteRenderer>().sprite = _bracket;
            if (id/5 <3)
            {
                _enemyPointAttack[id/5+2, id%5].GetComponent<SpriteRenderer>().sprite = _bracket;
            }
        }
        cardManager.toggleActiveDrawButton();
        return true;
    }
    public bool card004(int fakeID)
    {
        int id = -fakeID - 1;
        _enemyPointAttack[id / 5, id % 5].GetComponent<SpriteRenderer>().sprite = _bracket;
        if (turns_left == 3)
        {

        }
        cardManager.toggleActiveDrawButton();
        return true;
    }

    public int HP()
    {
        return HPleft;
    }
    public void isEnded()
    {
        for (int i = 0; i<5; i++)
        {
            for (int j = 0; j<5; j++)
            {
                Destroy(_pointToAttack[i, j]);
                Destroy(_enemyPointAttack[i, j]);
            }
        }

    }
    public void exitGame()
    {
        ship.exitGame();
        Destroy(this.gameObject);
    }

    public bool isFinishedChoosingCard()
    {
        return _cardChose;
    }



}
