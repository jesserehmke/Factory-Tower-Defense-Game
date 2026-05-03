using UnityEngine;
using System.Collections.Generic;

public class construction_bar_behavior : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject BpIcon;
    public GameObject SelectedBuilding;
    public Camera Maincam;
    //Templates---------------------
    public GameObject TransportBeltTemplate;
    public float size_offset = 0;
    public List<Vector3> blockedtiles = new List<Vector3>();
    public List<Vector3> selectedTiles = new List<Vector3>();

    
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

    public void Scan_for_Obstacles()
    {    
        for(float x = -11.0f; x < 12.0f; x += 0.5f)
        {
            for(float z = -9; z < 10; z += 0.5f)
            {
                if(Physics.Raycast(new Vector3(x, 0, z), Vector3.up, 5, LayerMask.GetMask("Default", "Mirrors")))
                {
                    blockedtiles.Add(new Vector3(x, 2.25f, z));
                }
            }
        }
    }

    // Functions for all Construction Icons -------------------------------------------------------------------------------------------------------------
    public void SelectTransportBelt()
    {

        size_offset = 0;
        Vector3 mouse_position = Input.mousePosition;
        Vector3 instantiate_position = Maincam.ScreenToWorldPoint(new Vector3(mouse_position.x, mouse_position.y, Maincam.transform.position.y - 2f));
        SelectedBuilding = Instantiate(TransportBeltTemplate, new Vector3(instantiate_position.x, 2, instantiate_position.z), Quaternion.identity);
        //covered tiles starting from transform.position;
        selectedTiles.Add(new Vector3(0,0,0));
        
    }



    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scan_for_Obstacles();
    }

    // Update is called once per frame
    void Update()
    {
        if(SelectedBuilding != null)
        {
            Vector3 mouse_position = Input.mousePosition;
            Vector3 mouse2world = Maincam.ScreenToWorldPoint(new Vector3(mouse_position.x, mouse_position.y, Maincam.transform.position.y - 2f));
            float snap_x = 0.5f * Mathf.Round(mouse2world.x*2) + size_offset;
            float snap_z = 0.5f * Mathf.Round(mouse2world.z*2) + size_offset;
            SelectedBuilding.transform.position = new Vector3(snap_x, 2, snap_z);
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                SelectedBuilding.transform.Rotate(0,90,0);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                SelectedBuilding.SetActive(false);
                SelectedBuilding = null;
                selectedTiles.Clear();
            }

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                bool tilesAreFree = true;
                foreach(Vector3 tile in selectedTiles)
                {
                    Vector3 neededTile = SelectedBuilding.transform.position + tile;
                    neededTile.y = 2.25f;
                    if(blockedtiles.Contains(neededTile))
                    {
                        tilesAreFree = false;
                        break;
                    }
                }
                if(tilesAreFree)
                {
                    SelectedBuilding = null;
                    selectedTiles.Clear();
                    Scan_for_Obstacles();   
                }

            }

        }
    }
}


