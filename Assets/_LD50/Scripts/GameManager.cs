using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GildarGaming.LD50
{
    public class GameManager : MonoBehaviour
    {
        private int score = 0;
        public Action ScoreChanged;
        public int Score { get { return score; }
            set { 
                score = value;
                ScoreChanged?.Invoke();
            }
        }
        public static GameManager Instance { get; private set; }
        public static Grid2D grid;
        [SerializeField] private GameObject playerPrefab;
        private float waterStorage = 200;
        public Action WaterStorageChanged;
        public float moneyTimer = 0;
        public float moneyUpdateDelay = 5f;
        public float moneyToAdd = 5f;
        public float WaterStorage { 
            get { return waterStorage; } set { 
                waterStorage = value; 
                WaterStorageChanged?.Invoke();

            } 
        }

        private float money = 150;
        public Action MoneyChanged;
        public float Money
        {
            get { return money; }
            set { 
                money = value;
                MoneyChanged?.Invoke();
            }
        }

        public void Reset()
        {
        }
        
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            grid = new Grid2D(40, 40);
        }
        
        public void Start()
        {
            BuildGrid();
        }

        internal void WaterBomb()
        {
            throw new NotImplementedException();
        }

        public void OnEnable()
        {
        }
        
        public void OnDisable()
        {
        }
        
        public void Update()
        {
            moneyTimer += Time.deltaTime;
            if (moneyTimer > moneyUpdateDelay)
            {
                Score += (int)((money + 1) / 10 + (waterStorage + 1) / 10);
                
                moneyTimer = 0;
                Money += moneyToAdd;
            }
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
                            node.occupiedBy = treeTile;
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
