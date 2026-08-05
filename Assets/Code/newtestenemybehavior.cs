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
    public bool open;
    public float gCost;
    public float hCost;
    public float xCost;
    public float fCost;
}
public class newtestenemybehavior : MonoBehaviour
{
    public GameObject Heart;
    public Vector3 debugpos;
    public float debugf = 3;
    public List<List<PathNode>> aStarDisplay = new List<List<PathNode>>();
    public int snapShotIndex;
    public GameObject AStarPrefab;
    

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

                    debugpos = newPathNode.pos;

                    debugf += 1;

                    Debug.DrawLine(debugpos, debugpos + Vector3.up*0.5f, Color.red, 1 + debugf);
                
                    if(newPathNode.hCost == 0)
                    {
                        return newPathNode;
                    }

                    pathNodes.Add(newPathNode);

                    PathNode step = newPathNode;
                    while(step.parent != null)
                    {
                        newPathNode.gCost += Vector3.Distance(step.pos, step.parent.pos);
                        step = step.parent;
                    }

                    newPathNode.fCost = newPathNode.gCost + newPathNode.hCost;

                    if(occupiedPositions.Contains(newPathNode.pos))
                    {
                        PathNode nodeToRemove = null;
                        foreach(PathNode occupiedTile in pathNodes)
                        {
                            if(occupiedTile.pos == newPathNode.pos)
                            {
                                if(newPathNode.fCost < occupiedTile.fCost)
                                {
                                    nodeToRemove = occupiedTile;
                                }
                                else
                                {
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
    void Start()
    {

        PathNode foundPath = AStarPathFinder();
        while(foundPath.parent != null)
        {
            Debug.DrawLine(foundPath.pos, foundPath.pos + Vector3.up, Color.green, 5f);
            foundPath = foundPath.parent;
        }


    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) && aStarDisplay.Count-1 > snapShotIndex)
        {
            Debug.Log("aroow");

            snapShotIndex++;
            foreach(PathNode tileNode in aStarDisplay[snapShotIndex])
            {
                GameObject TileObj = Instantiate(AStarPrefab, tileNode.pos, Quaternion.Euler(90, 0, 0));
                TileObj.SetActive(true);
                TextMeshPro tmp = TileObj.transform.Find("Text (TMP)").GetComponent<TextMeshPro>();
                tmp.text = tileNode.fCost.ToString();
            }
        }

    }
}

