using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEditor;

public class PathNode
{
    public string id;
    public Vector3 pos;
    public PathNode parent;
    public PathNode child;
    public bool open;
    public float gCost;
    public float hCost;
    public float xCost;
    public float fCost;
}
public class newtestenemybehavior : MonoBehaviour
{
    public GameObject Heart;
    public hp_manager HpManager;
    public string behavior_mode = "wait";
    public PathNode targetPath;
    public Rigidbody rb;
    public GameObject travelator;
    public int travelator_impact = 0;
    public float pathTime = 0;


    //A-Star Algorythm ----------------------------------------------------------
    public List<List<PathNode>> aStarDisplay = new List<List<PathNode>>();
    public int snapShotIndex = -1;
    public GameObject AStarPrefab;
    public PathNode theOne;

    //Visualizer for Debugging (vis) --------------------------------------------
    public List<GameObject> visTiles = new List<GameObject>();
    public List<int> visTilesCountList = new List<int>(); 
    

    public PathNode AStarPathFinder()
    {
        Vector3 startTile = new Vector3(Mathf.Round(transform.position.x * 2)/2, transform.position.y, Mathf.Round(transform.position.z * 2)/2);
        Vector3 endTile = Heart.transform.position;
        PathNode pathNodeZero = new PathNode
        {
            id = "zero",
            pos = startTile,
            gCost = 0,
            hCost = 0,
            xCost = 0,
            open = true
        };

        List<PathNode> pathNodes = new List<PathNode> {pathNodeZero};
        List<Vector3> occupiedPositions = new List<Vector3>();

        for(int i = 0; i < 5000; i++)
        {
            aStarDisplay.Add(new List<PathNode>());

            PathNode bestNode = new PathNode
            {
                fCost = 1000000000,
                id = "noNode"
            };

            Debug.Log(i);
            foreach(PathNode node in pathNodes)
            {
                Debug.Log("foreach pathnode " + i);
                if(node.open && node.fCost < bestNode.fCost)
                {
                    Debug.Log("fcost: " + node.fCost + " nodespos: " + node.pos);
                    bestNode = node;
                }
            }
            if(bestNode.parent != null)
            {
                Debug.Log("bestnode: " + bestNode.pos + "parent: " + bestNode.parent.pos);
            }

            if(bestNode.id == "noNode")
            {
                Debug.Log("eeeeeeeee");
                Debug.Log(pathNodes.Count);
                foreach(PathNode node in pathNodes)
                {
                    Debug.Log(node.open);
                }
                return pathNodeZero;
            }

            bestNode.open = false;  
            List<Vector3> surroundingTiles = new List<Vector3> {new Vector3(-0.5f, 0f, 0.5f), new Vector3(0f,0f,0.5f), new Vector3(0.5f,0f,0.5f), new Vector3(0.5f,0f,0f), new Vector3(0.5f,0f,-0.5f), new Vector3(0f,0f,-0.5f), new Vector3(-0.5f,0f,-0.5f), new Vector3(-0.5f,0f,0f)};
            
            // for non diagonal movement: List<Vector3> surroundingTiles = new List<Vector3> {new Vector3(0f,0f,0.5f), new Vector3(0.5f,0f,0f), new Vector3(0f,0f,-0.5f), new Vector3(-0.5f,0f,0f)};

            foreach(Vector3 neighborPos in surroundingTiles)
            {
                Debug.Log("surround");



                if(!Physics.Raycast(bestNode.pos + neighborPos - new Vector3(0, bestNode.pos.y, 0), Vector3.up, 5, LayerMask.GetMask("Default", "Mirrors")))
                {
                    Vector3 tilePos = bestNode.pos + neighborPos;
                    float Dx = Math.Abs(tilePos.x - endTile.x);
                    float Dz = Math.Abs(tilePos.z - endTile.z);
                    float tempHCost = Mathf.Sqrt(2*Mathf.Pow(Mathf.Min(Dx, Dz), 2)) + Mathf.Abs(Dx-Dz);
                    Debug.Log("bestnodepos " + bestNode.pos);

                    PathNode newPathNode = new PathNode
                    {
                        pos = tilePos,
                        gCost = 0,
                        hCost = tempHCost,
                        parent = bestNode,
                        open = true
                    };

                    if(newPathNode.hCost < 1)
                    {
                        Debug.Log("found");
                        PathNode pathStep = newPathNode;

                        while(pathStep.parent != null)
                        {
                            pathStep.parent.child = pathStep;
                            pathStep = pathStep.parent;
                            if(pathStep == theOne)
                            {
                                Debug.Log("still in");
                            }
                        }

                        return pathStep;
                    }

                    pathNodes.Add(newPathNode);

                    PathNode step = newPathNode;
                    while(step.parent != null)
                    {
                        newPathNode.gCost += Vector3.Distance(step.pos, step.parent.pos);
                        step = step.parent;
                    }

                    newPathNode.fCost = newPathNode.gCost + newPathNode.hCost;


                    // if(occupiedPositions.Contains(newPathNode.pos))
                    // {
                    //     foreach(PathNode occupiedTile in pathNodes)
                    //     {
                    //         Debug.Log("jjjjj");
                    //         if(occupiedTile.pos == newPathNode.pos)
                    //         {
                    //             if(newPathNode.fCost < occupiedTile.fCost)
                    //             {
                    //                 occupiedTile.fCost = newPathNode.fCost;
                    //             }
                    //         }
                    //         occupiedTile.open = true;
                    //     }
                    //     pathNodes.Remove(newPathNode);

                    // }
                    if(occupiedPositions.Contains(newPathNode.pos))
                    {
                        PathNode nodeToRemove = null;
                        foreach(PathNode occupiedTile in pathNodes)
                        {
                            if(occupiedTile.pos == newPathNode.pos)
                            {
                                if(newPathNode.fCost < occupiedTile.fCost)
                                {
                                    Instantiate(Heart, newPathNode.pos, Quaternion.Euler(90, 0, 0));
                                    Debug.Log("altes wird gelöscht");
                                    if(newPathNode.pos == new Vector3(10,2.25f,2))
                                    {
                                        theOne = newPathNode;
                                    }
                                    nodeToRemove = occupiedTile;
                                }
                                else
                                {
                                    Debug.Log("neues wird gelöscht");
                                    nodeToRemove = newPathNode;
                                }
                            }
                        }

                        pathNodes.Remove(nodeToRemove);
                    }

                }

                
             
            }
            
            occupiedPositions.Clear();
            foreach(PathNode v3node in pathNodes)
            {
                occupiedPositions.Add(v3node.pos);
            }

            foreach(PathNode snapshotNode in pathNodes)
            {
                PathNode copyNode = new PathNode
                {
                    pos = snapshotNode.pos,
                    fCost = snapshotNode.fCost,
                    parent = snapshotNode.parent,
                    open = snapshotNode.open
                };
                aStarDisplay[i].Add(copyNode);
            }
        }

        return pathNodeZero;
     
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Refresh_Path()
    {
        if(Time.time - pathTime > 0.1)
        {
            pathTime = Time.time;
            targetPath = AStarPathFinder();
        }
    }
    void Behavior()
    {
        switch(behavior_mode){
            case "wait":
                if(Time.time > 0.1f)
                {
                    Debug.Log("kkkkkkk");
                    targetPath = AStarPathFinder();
                    Debug.Log("targetpath pos " + targetPath.pos);
                    behavior_mode = "move";
                }
                break;
            case "move":
                Debug.Log("targetPath " + targetPath);
                Debug.Log("targetpos " + targetPath.pos);
                if(Vector3.Distance(transform.position, targetPath.pos) < 0.2f)
                {
                    Debug.Log("ääääääääää");
                    targetPath = targetPath.child;
                }

                Vector3 intendedMovement = transform.position + (targetPath.pos - transform.position).normalized * 0.02f;
                Vector3 manipulatingMovement = travelator.transform.forward * travelator_impact * 0.08f;
                rb.MovePosition(intendedMovement + manipulatingMovement);
                

                // //next path index when close enough to the current one
                // if(path_index < targetpath.Count-1 && Vector3.Distance(transform.position, targetpath[path_index]) < 0.2f)
                // {
                //     path_index += 1;
                // }
                // //switching to attack modde when reaching the target
                // else if(path_index == targetpath.Count -1 && Vector3.Distance(transform.position, targetpath[path_index]) < 0.5f)
                // {
                //     behavior_mode = "attack";
                // }
                // Vector3 intendedmovement = transform.position + (targetpath[path_index] - transform.position).normalized * 0.06f;
                // Vector3 manipulatingmovement = travelator.transform.forward * travelator_impact * 0.08f;            

                // rb.MovePosition(intendedmovement + manipulatingmovement);
                // Debug.Log(intendedmovement + manipulatingmovement + " distance: " + Vector3.Distance(transform.position, intendedmovement + manipulatingmovement));
                
                // travelator_impact = 0;
                break;
            case "attack":
                // code block
                break;
        }
    }
    
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        HpManager = GetComponent<hp_manager>();
        HpManager.hp = 100;

        // PathNode foundPath = AStarPathFinder();
        // while(foundPath.parent != null)
        // {
        //Debug.DrawLine(foundPath.pos, foundPath.pos + Vector3.up, Color.green, 5f);
        //     foundPath = foundPath.parent;
        // }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) && aStarDisplay.Count-1 > snapShotIndex)
        {
            Debug.Log("aroow");
            int visTilesCount = 0; 
            snapShotIndex++;
            foreach(PathNode tileNode in aStarDisplay[snapShotIndex])
            {
                GameObject TileObj = Instantiate(AStarPrefab, tileNode.pos, Quaternion.Euler(90, 0, 0));
                TileObj.SetActive(true);
                TextMeshPro tmp = TileObj.transform.Find("Text (TMP)").GetComponent<TextMeshPro>();
                tmp.text = tileNode.fCost.ToString();
                visTilesCount++;
                visTiles.Add(TileObj);
            }
            visTilesCountList.Add(visTilesCount);

        }
    }


    void FixedUpdate()
    {
        Behavior();

        

       

    }

    void OnTriggerStay(Collider other_collider)
    {
        

        if(other_collider.gameObject.tag == "Travelator")
        {
            travelator = other_collider.gameObject;
            travelator_impact = 1;
            Refresh_Path();

        }
                
        if(other_collider.gameObject.tag == "Shredder")
        {
            HpManager.hp -= 1;
        }
    }
}

