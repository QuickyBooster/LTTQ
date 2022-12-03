using UnityEngine;

public class PointEnemy : MonoBehaviour
{
    [SerializeField] Sprite _iconDestroyed;
    [SerializeField] Sprite _iconSquare;
    [SerializeField] Sprite _iconPoint;
    Controller _controller;

    SpriteRenderer _renderer;
    bool _destroyed;
    int _id;

    private void Start()
    {
        _destroyed = false;
        int.TryParse(this.name, out _id);
    }
    private void Awake()
    {
        _controller = FindObjectOfType<Controller>();
        _renderer = GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnMouseDown()
    {
        if (_destroyed) return;
        if (!_controller.isEnemyTurn())
        {
            _controller.sendIDToAttack(-_id-1);
        }
    }
    public bool isBeingAttack()
    {
        if (_destroyed) return false;
        if (_controller.isUsingCard())
        {
            if (_controller.IDCardUsing()==1)
            {
                _controller.card001(_id);
                _controller.toggleUsingCard(-1);
            }
            return true;
        }
        return false;
    }
    public void displayDestroy(bool status)
    {
        if (status)
        {
            _renderer.sprite =_iconDestroyed;
            _destroyed = true;

        }else
        {
            _renderer.sprite =_iconSquare;
            _destroyed = true;
        }

    }
    public bool isDestroyed() { return _destroyed; }
    public void resetAllElement()
    {
        _destroyed = false;
        _renderer.sprite = _iconPoint;
    }
}
