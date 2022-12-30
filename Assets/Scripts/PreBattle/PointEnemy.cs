using UnityEngine;
using System.Collections;

public class PointEnemy : MonoBehaviour
{
    [SerializeField] Sprite _iconDestroyed;
    [SerializeField] Sprite _iconSquare;
    [SerializeField] Sprite _iconPoint;
    [SerializeField] Sprite _iconBarrier;
    [SerializeField] Sprite _iconTorpedo;
    [SerializeField] Sprite _iconRed;
    Controller _controller;

    SpriteRenderer _renderer;
    bool _destroyed;
    int _id;
    bool _barrier;
    bool _torpedo;

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
    public void setBarrier(bool state)
    {
        // can animation
        _barrier = state;
        if (state)
            _renderer.sprite = _iconBarrier;
        else
            _renderer.sprite = _iconPoint;
    }
    public bool isBarrier()
    {
        return _barrier;
    }
    public void setTorpedo(bool state)
    {
        //can animation
        _torpedo = state;
        if (!state)
            _renderer.sprite = _iconTorpedo;
        else
            _renderer.sprite = _iconPoint;
    }
    public bool isTorpedo()
    {
        return (_torpedo);
    }
    IEnumerator displayRed()
    {
        _renderer.sprite = _iconRed;
        yield return new WaitForSeconds(0.5f);
        _renderer.sprite = _iconPoint;
        yield return new WaitForSeconds(0.5f);
        _renderer.sprite = _iconRed;
        yield return new WaitForSeconds(0.5f);
        _renderer.sprite = _iconPoint;
        yield return new WaitForSeconds(0.5f);
        _renderer.sprite = _iconRed;
        yield return new WaitForSeconds(0.5f);
        _renderer.sprite = _iconPoint;
    }
    public void displayRedCausedByCard202()
    {
        displayRed();
    }
}
