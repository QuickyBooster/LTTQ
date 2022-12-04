using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCard : MonoBehaviour
{
    float cardPoxX;
    float cardPoxY;

    // Update is called once per frame
    void Update()
    {
        this.cardPoxX = transform.position.x - 4.69f;
        this.cardPoxY = transform.position.y - 1.08f;

        if (cardPoxX == -4.69f && cardPoxY == -1.08f) 
            transform.localScale = new Vector2(0.8f, 0.8f);
        else transform.localScale = new Vector2(0.3f, 0.25f);
    }

}
