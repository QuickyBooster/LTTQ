using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;
    public Transform cardTransform;
    [SerializeField] CanvasGroup myUIGroup;
    bool showPannel;
    bool hidePannel;
    bool isCardInPanel;
    void Start()
    {
        
        panel.SetActive(false);
        showPannel = false; 
        hidePannel = false;
        myUIGroup.alpha = 0;
    }
    private void Update()
    {
        if (showPannel)
        {
            if (myUIGroup.alpha<1)
            {
                panel.SetActive(true);
                myUIGroup.alpha += Time.deltaTime;
                if (myUIGroup.alpha>=1)
                {
                    showPannel = false;
                }
            }
        }
        if (hidePannel)
        {
            if (myUIGroup.alpha>=0)
            {
                myUIGroup.alpha -= Time.deltaTime;
                if (myUIGroup.alpha ==0)
                {
                    hidePannel = false;
                    panel.SetActive(false);
                }
            }
        }
    }
    public void toggleShowPannel()
    {
       showPannel = !showPannel;
    }
    public void toggleHidePannel()
    {
         hidePannel = !hidePannel;
    }

    //public void Use()
    //{
    //    showPannel = false;
    //    hidePannel = true;
    //}

    //public void Get()
    //{
    //    showPannel = false;
    //    hidePannel = true;
    //}
    public bool isActive()
    {
        return showPannel;
    }
    public void toggleActive()
    {
        showPannel=!showPannel;
    }

    public void SetHidePannel(bool panelStatus)
    {
        hidePannel = panelStatus;
    }

    public void SetShowPannel(bool panelStatus)
    {
        showPannel = panelStatus;
    }

    public bool isCardInPanelNow()
    {
        return isCardInPanel;
    }

    public void setCardInPanel(bool cardStatus)
    {
        isCardInPanel = cardStatus;
    }
}
