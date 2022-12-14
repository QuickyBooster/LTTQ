using System.Collections;
using System.ComponentModel;
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
    bool _vertical;
    //life
    int life;

    //array for enemy ship to spawn
    GameObject[,] _ourPoints = new GameObject[5, 5];
    GameObject[,] _enemyPoints = new GameObject[5, 5];
    int firstID = 0;
    int HPleft;
    int cardID;
    int turnNumber;
    int turnWillAdd1Life;
    //so turn de torpedo no?
    int turns_left = 3;

    private void Start()
    {
        turnWillAdd1Life = -1;
        turnNumber = 0;
        _vertical = false;
        life = 1;
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
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
                _ourPoints[i, j] = destroyPoint;
                y-= 0.7197f;
                id++;
            }
            x+= 0.7198f;
        }
        _ourPoints[firstID/5, firstID%5].GetComponent<Point>().setShipField(true);
        _ourPoints[firstID/5+1, firstID%5].GetComponent<Point>().setShipField(true);
        _ourPoints[firstID/5+2, firstID%5].GetComponent<Point>().setShipField(true);
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
                _enemyPoints[i, j] = pointCreated;
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
    public void toggleEnemyTurnWithText()
    {
        ++turnNumber;
        if (turnWillAdd1Life==turnNumber)
            ++life;
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
    IEnumerator textChangeWhenLoseLife(float time)
    {
        yield return new WaitForSeconds(time);
        uIManagerBattle.setTextTurn(
                    "Adjust your ship location then press ready to continue.");
        _enemyTurn = true;
    }
    public bool returnPointHit(int idHit)
    {
        if (idHit == -1)
            return false;
        if (_ourPoints[idHit/5, idHit%5].GetComponent<Point>().isBeingAttack())
        {
            HPleft--;
            if (HPleft ==0)
            {
                if (--life ==-1)
                {
                    uIManagerBattle.endMatch();
                    return true;
                }
                StartCoroutine(textChangeWhenLoseLife(0.5f));

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
        _enemyPoints[id/5, id%5].GetComponent<PointEnemy>().displayDestroy(status);
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


    public void deleteOurPoints()
    {
        for (int i = 0; i<5; i++)
            for (int j = 0; j<5; j++)
                Destroy(_ourPoints[i, j]);
    }
    public void deleteEnemyPoints()
    {
        for (int i = 0; i<5; i++)
            for (int j = 0; j<5; j++)
                Destroy(_enemyPoints[i, j]);
    }

    public void deletePoint()
    {
        for (int i = 0; i<5; i++)
        {
            for (int j = 0; j<5; j++)
            {
                Destroy(_ourPoints[i, j]);
                Destroy(_enemyPoints[i, j]);
            }
        }

    }
    public void exitGame()
    {
        ship.exitGame();
        deletePoint();
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
    public bool card001(int fakeID)
    {
        int id = -fakeID-1;
        _enemyPoints[id/5, id%5].GetComponent<SpriteRenderer>().sprite = _bracket;
        if (id/5 <4)
        {
            _enemyPoints[id/5+1, id%5].GetComponent<SpriteRenderer>().sprite = _bracket;
            if (id/5 <3)
            {
                _enemyPoints[id/5+2, id%5].GetComponent<SpriteRenderer>().sprite = _bracket;
            }
        }
        cardManager.toggleActiveDrawButton();
        return true;
    }
    public bool card004(int fakeID)
    {
        int id = -fakeID - 1;
        _enemyPoints[id / 5, id % 5].GetComponent<SpriteRenderer>().sprite = _bracket;
        if (turns_left == 3)
        {

        }
        cardManager.toggleActiveDrawButton();
        return true;
    }
    public bool card101()
    {
        life++;
        return true;
    }
    public bool card102()
    {
        deleteOurPoints();
        ship.toggleCollider();
        uIManagerBattle.setTextTurn("move your ship to your favorite position");
        return true;
    }
    public bool card103()
    {
        for (int i = 0; i<5; i++)
            for (int j = 0; j<5; j++)
                if (_ourPoints[i, j].GetComponent<Point>().isBarrier())
                {
                    _ourPoints[i, j].GetComponent<Point>().setBarrier(false);
                }
        return true;
    }
    public void card103_receive()
    {
        for (int i = 0; i<5; i++)
            for (int j = 0; j<5; j++)
                if (_enemyPoints[i, j].GetComponent<PointEnemy>().isBarrier())
                {
                    _enemyPoints[i, j].GetComponent<PointEnemy>().setBarrier(false);
                }
    }
    public bool card104()
    {
        for (int i = 0; i<5; i++)
            for (int j = 0; j<5; j++)
                if (_ourPoints[i, j].GetComponent<Point>().isTorpedo())
                {
                    _ourPoints[i, j].GetComponent<Point>().setTorpedo(false);
                }
        return true;
    }
    public void card104_receive()
    {
        for (int i = 0; i<5; i++)
            for (int j = 0; j<5; j++)
                if (_enemyPoints[i, j].GetComponent<PointEnemy>().isTorpedo())
                {
                    _enemyPoints[i, j].GetComponent<PointEnemy>().setTorpedo(false);
                }
    }
    public bool card201()
    {
        uIManagerBattle.setTextTurn("You have another turn to draw card");
        return true;
    }
    public bool card202(int id)
    {
        bool vertical = false;
        if (id>100)
        {
            vertical = true;
            id -=100;
        }
        _enemyPoints[id/5,id%5].GetComponent<PointEnemy>().displayRedCausedByCard202();
        if (vertical)
        {
            id++;
            _enemyPoints[id/5, id%5].GetComponent<PointEnemy>().displayRedCausedByCard202();
            id++;
            _enemyPoints[id/5,id%5].GetComponent<PointEnemy>().displayRedCausedByCard202();
        }else
        {
            id+=5;
            _enemyPoints[id/5,id%5].GetComponent<PointEnemy>().displayRedCausedByCard202();
            id+=5;
            _enemyPoints[id/5,id%5].GetComponent<PointEnemy>().displayRedCausedByCard202();
        }
        return true;
    }
    public int card202_receive()
    {
        int k=0;
        for (int i = 0; i<5; i++)
            for (int j = 0; j<5; j++)
                if (_ourPoints[i, j].GetComponent<Point>().isShipField())
                {
                    k= i*5+j;
                    if (_vertical)
                        k+=100;
                    return k;
                }
        return k;
    }
    public bool card203()
    {
         turnWillAdd1Life = turnNumber+5;
        return true;
    }
}
