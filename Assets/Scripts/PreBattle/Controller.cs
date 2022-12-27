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
    UIManagerBattle uIManagerBattle;


    bool _enemyTurn;
    bool _cardChose;
    bool _usingCard;
    //life
    int life;

    //array for enemy ship to spawn
    GameObject[,] _pointToAttack = new GameObject[5, 5];
    GameObject[,] _enemyPointAttack = new GameObject[5, 5];
    int firstID = 0;
    int HPleft;
    int cardID;
    //so turn de torpedo no?
    int turns_left = 3;

    private void Start()
    {
        life = 3;
        HPleft = 3;
        _usingCard = false;
        DontDestroyOnLoad(this.gameObject);
        ship = FindObjectOfType<Ship>();
        _manager = FindObjectOfType<UIManager>();

        _cardChose = true;
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
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 3)
        {
            createTable();
            createTableEnemy();
            ship.toggleCollider();
            while (!cardManager)
                cardManager = FindObjectOfType<CardManager>();
            while (!cardFunction)
                cardFunction = FindObjectOfType<CardFunction>();
            while (!pointFunction)
                pointFunction = FindObjectOfType<PointFunction>();
            while (!uIManagerBattle)
                uIManagerBattle = FindObjectOfType<UIManagerBattle>();
        }
    }
    private void Update()
    {


    }



    public void createTable()
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
    public void createTableEnemy()
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
        if (_enemyTurn)
            uIManagerBattle.setTextTurn("Enemy turn: ");
        else
            uIManagerBattle.setTextTurn("Your turn: ");
    }
    public void setShipInPlace(bool status, int where)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (status)
        {
            firstID = where;
            if (scene.buildIndex ==2)
            {
                if (isFinishedChoosingCard())
                    _manager.showButtonBattle(true);
                else
                    _manager.showButtonBattle(false);
            }
            else
                uIManagerBattle.showButtonReadyToCountinue(true);
        }
        else
        {
            if (scene.buildIndex ==2)
            {
                if (isFinishedChoosingCard())
                    _manager.showButtonBattle(true);
                else
                    _manager.showButtonBattle(false);
            }
            else
            {
                uIManagerBattle.setTextTurn("Enemy is waiting for you!\n"
                    +"Adjust your ship location then press ready to continue.");
                uIManagerBattle.showButtonReadyToCountinue(false);
            }
        }
    }
    public void setBattleAgain()
    {
        createTable();
        ship.toggleCollider();
        pointFunction.resumeBattle();
    }
    public bool returnPointHit(int idHit)
    {
        if (idHit == -1)
            return false;
        if (_pointToAttack[idHit/5, idHit%5].GetComponent<Point>().isBeingAttack())
        {
            HPleft--;
            if (HPleft ==0)
            {
                if (--life ==-1)
                {
                    deletePoint();
                    uIManagerBattle.endMatch();
                    return true;
                }
                toggleEnemyTurn();
                uIManagerBattle.setTextTurn("Enemy is waiting for you!\n"
                    +"Adjust your ship location then press ready to continue.");
                // delete our points and send a message to enemy
                pointFunction.pauseBattle();
                deleteOurPoints();

                HPleft = 3;
                ship.toggleCollider();

            }
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
        pointFunction.attackPoint(id);
        cardFunction.setNextTurn();
    }
    public void toggleUsingCard(int id)
    {
        _usingCard = !_usingCard;
        cardID   = id;
    }
    public bool isUsingCard()
    {
        return _usingCard;
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

    public void deleteOurPoints()
    {
        for (int i = 0; i<5; i++)
            for (int j = 0; j<5; j++)
                Destroy(_pointToAttack[i, j]);
    }
    public void deleteEnemyPoints()
    {
        for (int i = 0; i<5; i++)
            for (int j = 0; j<5; j++)
                Destroy(_enemyPointAttack[i, j]);
    }

    public void deletePoint()
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

    public int lifeCount()
    {
        return life;
    }
    public void textForWaitingEnemy()
    {
        uIManagerBattle.setTextTurn("Please wait for enemy!");
    }

}
