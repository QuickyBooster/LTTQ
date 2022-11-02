using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverTipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;
    public static Action <string,Vextor2> OnMouseHover;
    public static Action OnMouseLoseFocus;
    
    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }

    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }

    void Start()
    {
        HideTip();
    }

    private void ShowTip(string tip, Vector2 mousePos)
    {
        tipText.text = tip;
        tipWindow.sizeDelra = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWitch, tipText.preferredHeight);
        tipWindow.gameObject.SetActive(true);
        tipWindow.tranform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x * 2, mousePos.y);

    }

    private void HideTip()
    {
        tipText = default;
        tipWindow.gameObject.SetActive(false);
    }



}
