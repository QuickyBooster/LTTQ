using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngin.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
    public string tipToShow;
    private float timeToWait = 0.5f;
   public void OnPointerEnter(PointerEvenData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());

    }
    public voif OnPointExit(PointEvenData eventData)
    {
        StopAllCoroutines();
        HoverTipManager.OnMouseFocus();
    }

    private void ShowMessage()
    {
        HoverTipManager.OnMouseOver(tipToShow, Input.mousePosition);
    }

    private IEnumerator StarTimer()
    {
        yield return new WaitForSeconds(timeToWait);

        ShowMessage();
    }
}
