using UnityEngine;
using System.Collections;

public class PointEnemy : MonoBehaviour
{
    [SerializeField] Sprite _iconDestroyed;
    [SerializeField] Sprite _iconSquare;
    [SerializeField] Sprite _iconPoint;
    [SerializeField] Sprite _iconTorpedo;
    [SerializeField] Sprite _iconRed;
    Controller _controller;
    //[SerializeField] ParticleSystem explosion;
    SpriteRenderer _renderer;
    bool _destroyed;
    int _id;
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
        if (!_controller.isEnemyTurn())
        {
            isBeingAttack();
        }
    }
    public bool isBeingAttack()
    {
        if (_controller.isUsingCard())
        {
            if (_controller.IDCardUsing()==2)
            {

                StartCoroutine(delayID3(1f));
                _destroyed= true;
                return true;
            }
            else if(_controller.IDCardUsing()==3)
            {
                if (_destroyed) return false;
                StartCoroutine(delayID3(0.7f));
                return true;
            }
        }
        if (_destroyed) return false;
        _controller.sendIDToAttack(-_id-1);
        return true;
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
        _destroyed = false;
    }
    public void displayDestroy(bool status)
    {
        if (status)
        {
            //explosion.Play();
            _renderer.sprite =_iconDestroyed;
            _destroyed = true;

        }else
        {
            //explosion.Play();
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
