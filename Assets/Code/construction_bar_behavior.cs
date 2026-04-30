using UnityEngine;

public class construction_bar_behavior : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject BpIcon;
    
    public void MaxMinBlueprint()
    {
        RectTransform rTransform = GetComponent<RectTransform>();
        if(isOpen)
        {
            rTransform.anchoredPosition = new Vector2(20, -270);
            isOpen = false;
        }
        else
        {
            rTransform.anchoredPosition = new Vector2(20, 220);
            isOpen = true;
        }
        
    }

    public void CloseBlueprint()
    {
        BpIcon.SetActive(true);
        this.gameObject.SetActive(false);
    }

    // Functions for all Construction Icons -------------------------------------------------------------------------------------------------------------
    public void SelectTransportBelt()
    {
        
    }


    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


