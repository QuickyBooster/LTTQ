using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] GameObject _explosion;

    // Update is called once per frame
    private void Start()
    {
        _explosion.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 explosionPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (explosionPos.x > 5.15 && explosionPos.x < 8.65)
                if (explosionPos.y > -3 && explosionPos.y < 0.5)
                {
                    _explosion.SetActive(true);
                    _explosion.transform.position = new Vector2(explosionPos.x, explosionPos.y);
                }
        }
        if(Input.GetMouseButtonUp(0))
        {
            StartCoroutine(delayTime());
        }
    }
    IEnumerator delayTime()
    {
        yield return new WaitForSeconds(1f);
        _explosion.SetActive(false);
    }
}
