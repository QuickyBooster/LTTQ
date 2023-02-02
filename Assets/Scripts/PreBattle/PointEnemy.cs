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
        isBeingAttack();
    }
    public bool isBeingAttack()
    {
        if (_destroyed) return false;
        if (_controller.isUsingCard())
        {
            if (_controller.IDCardUsing()==2)
            {

                StartCoroutine(delayID2(1f));
            }
            else if(_controller.IDCardUsing()==3)
            {
                if (_destroyed) return false;
                StartCoroutine(delayID3(0.7f));  
            }
            return true;
        }
        _controller.sendIDToAttack(-_id-1);
        return false;
    }
    IEnumerator delayID2(float time)
    {
        yield return new WaitForSeconds(time);
        setTorpedo(true);
        _controller.card002(-_id-1);
    }
    IEnumerator delayID3(float time)
    {
        yield return new WaitForSeconds(time);
        _controller.card003(-_id-1);
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
    public void setTorpedo(bool state)
    {
        //can animation
        _torpedo = state;
        if (state)
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
        StartCoroutine(displayRed());
    }
}
