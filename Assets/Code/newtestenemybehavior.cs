// using UnityEngine;
// using System.Collections.Generic;
// using System;
// public class newtestenemybehavior : MonoBehaviour
// {
//     public GameObject Heart;
//     public class PathNode
//     {
//     public Vector3 pos;
//     public PathNode parent;
//     public bool open;
//     public float gCost;
//     public float hCost;
//     public float xCost;
//     public float fCost;
//     public void Create()
//     {
//         fCost = gCost + hCost + xCost;
//     }
// }
//     public List<PathNode> AStarPathFinder()
//     {
//         Vector3 startTile = new Vector3(Mathf.Round(transform.position.x * 2)/2, transform.position.y, Mathf.Round(transform.position.z * 2)/2);
//         Vector3 endTile = Heart.transform.position;
//         PathNode pathNodeZero = new PathNode
//         {
//             pos = startTile,
//             gCost = 0,
//             hCost = 0,
//             xCost = 0,
//             open = true
//         };
//         List<PathNode> pathNodes = new List<PathNode> {pathNodeZero};
//         while(true)
//         {
//             List<Vector3> occupiedPositions = new List<Vector3>();
//             foreach(PathNode v3node in pathNodes)
//             {
//                 occupiedPositions.Add(v3node.pos);
//             }

//             PathNode bestNode = new PathNode
//             {
//                 fCost = 10^10,
//             };
//             foreach(PathNode node in pathNodes)
//             {
//                 if(node.open &&  node.fCost < bestNode.fCost)
//                 {
//                     bestNode = node;
//                 }
//             }

//             List<Vector3> surroundingTiles = new List<Vector3> {new Vector3(-0.5f, 0f, 0.5f), new Vector3(0f,0f,0.5f), new Vector3(0.5f,0f,0.5f), new Vector3(0.5f,0f,0f), new Vector3(0.5f,0f,-0.5f), new Vector3(0f,0f,-0.5f), new Vector3(-0.5f,0f,-0.5f), new Vector3(-0.5f,0f,0f)};
//             foreach(Vector3 neighborPos in surroundingTiles)
//             {
//                 if(!occupiedPositions.Contains(bestNode.pos) && !Physics.Raycast(bestNode.pos + neighborPos - new Vector3(0, bestNode.pos.y, 0), Vector3.up, 5, LayerMask.GetMask("Default", "Mirrors")))
//                 {
//                     Vector3 tilePos = bestNode.pos + neighborPos;
//                     float Dx = Vector3.Distance(tilePos.x, endTile.x);
//                     float Dz = Vector3.Distance(tilePos.z, endTile.z);
//                     PathNode newpathnode = new PathNode
//                     {
//                         pos = tilePos,

//                         hCost = Math.Sqrt(2*Math.Pow(Math.Min(Dx, Dz), 2)) + Math.Abs(Dx-Dz),

                     
//                     };
//                 }
             
//             }
//         }
     
//     }
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {

//     }
//     // Update is called once per frame
//     void Update()
//     {

//     }
// }

