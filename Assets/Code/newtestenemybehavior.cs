using UnityEngine;
using System.Collections.Generic;
public class newtestenemybehavior : MonoBehaviour
{
    public GameObject Heart;
    public class PathNode
    {
    public Vector3 pos;
    public PathNode parent;
    public bool open;
    public float gCost;
    public float hCost;
    public float xCost;
    public float fCost;
    public void Create()
    {
        fCost = gCost + hCost + xCost;
    }
}
    public List<PathNode> AStarPathFinder()
    {
        Vector3 startTile = new Vector3(Mathf.Round(transform.position.x * 2)/2, transform.position.y, Mathf.Round(transform.position.z * 2)/2);
        Vector3 endTile = Heart.transform.position;

        PathNode pathNodeZero = new PathNode
        {
            pos = startTile,
            gCost = 0,
            hCost = 0,
            xCost = 0,
            open = true
        };
        List<PathNode> pathNodes = new List<PathNode> {pathNodeZero};
        while(true)
        {
            PathNode bestNode = new PathNode
            {
                fCost = 10^10,
            };
            foreach(PathNode node in pathNodes)
            {
                if(node.open &&  node.fCost < bestNode.fCost)
                {
                    bestNode = node;
                }
            }
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }
    // Update is called once per frame
    void Update()
    {
  
    }
}

