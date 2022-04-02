using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD50
{
    public class GameManager : MonoBehaviour
    {
        public static Grid2D grid;
        [SerializeField] private GameObject playerPrefab;
        
        public void Reset()
        {
        }
        
        public void Awake()
        {
            grid = new Grid2D(40, 40);
        }
        
        public void Start()
        {
            BuildGrid();
        }
        
        public void OnEnable()
        {
        }
        
        public void OnDisable()
        {
        }
        
        public void Update()
        {
        }
        
        public void OnDestroy()
        {
        }
        public void BuildGrid()
        {
            GameObject[] grassTiles = GameObject.FindGameObjectsWithTag("Grass");
            GameObject[] treeTiles = GameObject.FindGameObjectsWithTag("Tree");
            foreach (var grassTile in grassTiles)
            {
                //Debug.Log("Grass Tile: " + grassTile.transform.position);
            } 

            
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    GridNode node = new GridNode();
                    node.Position = new Vector2Int(i, j);
                    node.IsBurned = false;
                    node.IsOnFire = false;
                    node.IsWalkable = true;
                    foreach (GameObject treeTile in treeTiles)
                    {
                        if ((int)treeTile.transform.position.x == (int)node.Position.x && (int)treeTile.transform.position.y == (int)node.Position.y)
                        {
                            node.IsWalkable = false;
                            node.Occupied = true;
                        }

                    }
                    grid.Set(i, j, node);
                }
            }
            
        }
        
        public static Grid2D GetGrid()
        {
            return grid;
        }
    }
}
