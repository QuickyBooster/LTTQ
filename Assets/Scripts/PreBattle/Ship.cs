using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    Controller _controller;
    Scene currentScene;
    [SerializeField] GameObject _buttonPlay;

    float _x;
    float _y;

    string sceneName;

    float _deltaX, _deltaY;
    Vector2 _mousePosition;


    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        _buttonPlay.SetActive(false);
        _controller = FindObjectOfType<Controller>();
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnMouseDown()
    {
        if (true)
        {
            sceneName = currentScene.name;
            _deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            _deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            _controller.setShipInPlace(false, 0);
            if (sceneName == "PreBattle") 
                _buttonPlay.SetActive(false);
        }
    }
    private void OnMouseDrag()
    {
        if (true)
        {
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(_mousePosition.x - _deltaX, _mousePosition.y - _deltaY);
        }
    }
    private void OnMouseUp()
    {
        if (true)
        {
            sceneName = currentScene.name;
            // find the standard position of it
            int id = 0;
            float vX, vY, tX, tY;
            vX = transform.position.x - 0.7224f;
            vY = transform.position.y;
            tX = -8.3325f;
            for (int i = 0; i < 3; i++)
            {
                tY = 0.172f;
                for (int j = 0; j < 5; j++)
                {
                    if (Mathf.Abs(vX - tX) <= 0.35f && Mathf.Abs(vY - tY) <= 0.35f)
                    {
                        transform.position = new Vector2(tX + 0.7224f, tY);
                        _controller.setShipInPlace(true, i * 5 + j);
                        if (sceneName == "PreBattle")
                            _buttonPlay.SetActive(true);
                        return;
                    }
                    id++;
                    tY -= 0.7197f;
                }
                id++;
                tX += 0.7198f;
            }
        }
    }
    public void toggleCollider()
    {
        GetComponent<Collider2D>().enabled = !GetComponent<Collider2D>().enabled;
    }
    public void exitGame()
    {
        Destroy(this.gameObject);
    }
}
