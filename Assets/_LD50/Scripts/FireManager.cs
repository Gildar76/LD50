using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GildarGaming.LD50
{
    public class FireManager : MonoBehaviour
    {
        private static FireManager instance;
        public static FireManager Instance { get; set; }
        [SerializeField] private GameObject firePrefab;
        Queue<GameObject> firePool = new Queue<GameObject>();
        Queue<GameObject> burntGroundPool = new Queue<GameObject>();
        [SerializeField] private GameObject burntGroundPrefab;
        float fireSpreadDelay = 15f;
        float fireSpreadTimer = 0f;
        List<Fire> activeFires = new List<Fire>();
        float spreadChance = 0.1f;
        [SerializeField] List<Vector2Int> fireStartPositions;
        public List<Fire> ActiveFires { get { return activeFires; } }
        private List<Fire> fires;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        void Start()
        {
            for (int i = 0; i < 1000; i++)
            {
                GameObject go = GameObject.Instantiate(firePrefab);
                
                go.SetActive(false);
                firePool.Enqueue(go);
                GameObject burntGround = GameObject.Instantiate(burntGroundPrefab);
                burntGround.SetActive(false);
                burntGroundPool.Enqueue(burntGround);


            }
            StartCoroutine(StartFires(3));

        }

        IEnumerator StartFires(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            foreach (var item in fireStartPositions)
            {
                //Debug.Log("Fire started at " + item);
                GridNode node = GameManager.grid.Get(item.x, item.y);
                //Debug.Log("Node at " + node.Position);
                SPawnFire(node);
            }
            yield return null;
        }

        internal GameObject GetBurntGround()
        {
            return burntGroundPool.Dequeue();
        }

        void Update()
        {
            if (fireSpreadTimer > fireSpreadDelay)
            {
                fireSpreadTimer = 0f;
                SpreadFire();
            }
            else
            {
                fireSpreadTimer += Time.deltaTime;
            }
        }

        private void SpreadFire()
        {
            Grid2D grid = GameManager.grid;
            int currentFireLength = activeFires.Count;
            for (int i = 0; i < currentFireLength; i++)
            {
                Fire fire = activeFires[i];
                if (!activeFires[i].enabled)
                {
                    continue;
                }
                GridNode node = grid.Get(fire.node.Position.x, fire.node.Position.y);
                List<GridNode> neigbhours = grid.FindNeighbour(node);
                foreach (var n in neigbhours)
                {
                    if (n.IsOnFire) continue;
                    if (!n.Occupied) continue;
                    //Debug.Log("Checking node at " + n.Position);
                    if (UnityEngine.Random.Range(0, 1f) < spreadChance)
                    {
                        SPawnFire(n);
                        break;
                    }

                }
            }
            
        }

        private void SPawnFire(GridNode n)
        {
            GameObject go = firePool.Dequeue();
            go.GetComponent<Fire>().node = n;
            go.transform.position = new Vector3(n.Position.x,    n.Position.y, -5);
            n.IsOnFire = true;
            go.SetActive(true);
            Fire fire = go.GetComponent<Fire>();
            fire.isBurning = true;
            activeFires.Add(fire);
            
        }

        public void AddFireBackToPool(GameObject fire)
        {
            Fire fireComponent = fire.GetComponent<Fire>();
            activeFires.Remove(fireComponent);
            firePool.Enqueue(fire);
        }
    }
}
