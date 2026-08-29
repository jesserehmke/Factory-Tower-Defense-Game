using UnityEngine;
using System.Collections.Generic;



public class testenemybehavior : MonoBehaviour
{
    //Setup:----------------------------------------------------------------
        public GameObject heart;
        public Rigidbody rb;
        public string behavior_mode = "wait";
        public bool refreshpath = false;
        public hp_manager hp_manager; 

        //shredder:
        public GameObject shredder;
        public bool get_shredded = false;
        //moving tiles:
        public GameObject travelator;
        public List<GameObject> travelators;
        public int travelator_impact = 0;
        //pathfinding:
        public List<Vector3> targetpath;
        public List<Vector3> blockedtiles = new List<Vector3>();
        public float path_creation_time = 0;

        //movement:
        public float speed;
        public int path_index = 0;
    //----------------------------------------------------------------------
    public void Scan_for_Obstacles()
    {
        blockedtiles.Clear();   
        for(float x = -11.0f; x < 12.0f; x+=0.5f)
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
    public List<Vector3> DijkstraPathfinder(){
        Scan_for_Obstacles();

    //Setup for the Paths:----------------------------------------------------------------------------------------------------------------------
        Vector3 currenttile = new Vector3(Mathf.Round(transform.position.x * 2)/2, transform.position.y, Mathf.Round(transform.position.z * 2)/2);
        List<List<Vector3>> paths = new List<List<Vector3>>();
        List<List<Vector3>> newpaths = new List<List<Vector3>>();
        List<Vector3> firstpath = new List<Vector3>();
        firstpath.Add(currenttile);
        paths.Add(firstpath);
        newpaths.Add(firstpath);
        List<Vector3> nextblocked = new List<Vector3>();
        int outdated_paths = 0;
        int test = 0;
    //------------------------------------------------------------------------------------------------------------------------------------------

        while(true){
            //With every run this loop adds new paths to the newPaths list and deletes the outdated paths
            for(int h = 0; h < outdated_paths; h++)
            {
                paths.RemoveAt(0);
                newpaths.RemoveAt(0);
            }
            if(newpaths.Count == 0)
            {
                Debug.Log("no path found");
                return new List<Vector3>{transform.position};
            }

            outdated_paths = paths.Count;
            
            for(int i = 0; i < paths.Count; i++ )
            {  
                Vector3 starttile = paths[i][^1];
                List<Vector3> nexttiles = new List<Vector3>{starttile + new Vector3(-0.5f,0,0), starttile + new Vector3(0,0,0.5f), starttile + new Vector3(0.5f,0,0), starttile + new Vector3(0,0,-0.5f)};
                //List<Vector3> nexttiles = new List<Vector3>{starttile + new Vector3(-0.5f,0,0), starttile + new Vector3(-0.5f,0,0.5f) , starttile + new Vector3(0,0,0.5f), starttile + new Vector3(0.5f,0,0.5f), starttile + new Vector3(0.5f,0,0), starttile + new Vector3(0.5f,0,-0.5f), starttile + new Vector3(0,0,-0.5f), starttile + new Vector3(-0.5f,0,-0.5f)};
                foreach(Vector3 nexttile in nexttiles)
                {
                    test++;
                    // foreach(Vector3 item in blockedtiles)
                    // {
                    //     Debug.Log("blockedtiles: "+item);
                    // }
                    //Debug.Log("nexttile "+ nexttile);
                
                    //Debug.Log("distance: " + Vector3.Distance(nexttile, heart.transform.position));
                    if(Vector3.Distance(nexttile, new Vector3(heart.transform.position.x, nexttile.y, heart.transform.position.z)) <= 0.6)
                    {
                        
                        paths[i].Add(nexttile);
                        Debug.Log(test);
                        return paths[i];
                    }
                    if(blockedtiles.Contains(nexttile) == false)
                    {
                        newpaths.Add(new List<Vector3>(paths[i]));
                        newpaths[^1].Add(nexttile);
                        
                        
                        //nextblocked.Add(nexttile);
                        blockedtiles.Add(nexttile);
                    }

                }


                

            }
            
            paths = new List<List<Vector3>>(newpaths);

        }

    }
    public void Refresh_Path(){
         
        if(Time.time - path_creation_time > 0.1)
        {
            if(refreshpath)
            {
                path_creation_time = Time.time;
                targetpath = DijkstraPathfinder();
                path_index = 0;
            } 
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hp_manager = GetComponent<hp_manager>();
        hp_manager.hp = 100;

    }

    void FixedUpdate(){

        switch(behavior_mode){
            case "wait":
                if(Time.time > 0.1f)
                {
                    targetpath = DijkstraPathfinder();
                    behavior_mode = "move";
                }
                break;
            case "move":
                //next path index when close enough to the current one
                if(path_index < targetpath.Count-1 && Vector3.Distance(transform.position, targetpath[path_index]) < 0.2f)
                {
                    path_index += 1;
                }
                //switching to attack modde when reaching the target
                else if(path_index == targetpath.Count -1 && Vector3.Distance(transform.position, targetpath[path_index]) < 0.5f)
                {
                    behavior_mode = "attack";
                }
                Vector3 intendedmovement = transform.position + (targetpath[path_index] - transform.position).normalized * 0.06f;
                Vector3 manipulatingmovement = travelator.transform.forward * travelator_impact * 0.08f;            

                rb.MovePosition(intendedmovement + manipulatingmovement);
                Debug.Log(intendedmovement + manipulatingmovement + " distance: " + Vector3.Distance(transform.position, intendedmovement + manipulatingmovement));
                
                travelator_impact = 0;
                break;
            case "attack":
                // code block
                break;
        }

       

    }

    void OnTriggerStay(Collider other_collider)
    {
        

        if(travelators.Contains(other_collider.gameObject))
        {
            travelator = other_collider.gameObject;
            travelator_impact = 1;
            Refresh_Path();
        }
                
        if(other_collider.gameObject == shredder)
        {
            hp_manager.hp -= 1;
        }
    }
}
