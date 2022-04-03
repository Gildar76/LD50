using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

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
        public AudioSource waterBombingAudio;
        GameObject[] housees;
        List<Health> houseHealth;
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
            houseHealth = new List<Health>();
            BuildGrid();
            housees = GameObject.FindGameObjectsWithTag("House");
            foreach (var house in housees)
            {
                houseHealth.Add(house.GetComponent<Health>());
            }
        }

        internal void WaterBomb()
        {
            waterBombingAudio.Play();
            List<Fire> fires = FireManager.Instance.ActiveFires;
            for (int i = 0; i < 5; i++)
            {
                int randomNumber = UnityEngine.Random.Range(0, fires.Count);
                Fire fire = fires[randomNumber];
                if (fire != null)
                {
                    fire.GetComponent<Health>().TakeDamage(250);
                }
            }
            Money -= 500;
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
                //Do house check here as well.
                bool gameOver = true;
                foreach (var health in houseHealth)
                {
                    if (!health.IsDead)
                    {
                        gameOver = false;
                        break;
                    }
                }
                if (gameOver)
                {
                    LoadGameOverScene();
                }
            }
        }

        private void LoadGameOverScene()
        {
            SceneManager.LoadScene(2);
        }

        public void OnDestroy()
        {
        }
        public void BuildGrid()
        {
            GameObject[] grassTiles = GameObject.FindGameObjectsWithTag("Grass");
            GameObject[] treeTiles = GameObject.FindGameObjectsWithTag("Tree");
            GameObject[] houseTiles = GameObject.FindGameObjectsWithTag("House");
            foreach (var houseTile in houseTiles)
            {
                //Debug.Log("Grass Tile: " + grassTile.transform.position);
            }
            foreach (var houseti in grassTiles)
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
                    foreach (var houseti in houseTiles)
                    {
                        if ((int)houseti.transform.position.x == (int)node.Position.x && (int)houseti.transform.position.y == (int)node.Position.y)
                        {
                            node.IsWalkable = false;
                            node.Occupied = true;
                            node.occupiedBy = houseti;
                        }
                    }

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
