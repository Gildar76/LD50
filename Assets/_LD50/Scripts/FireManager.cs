using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GildarGaming.LD50
{
    public class FireManager : MonoBehaviour
    {
        private static FireManager instance;
        
        [SerializeField] private GameObject firePrefab;
        Queue<GameObject> firePool = new Queue<GameObject>();
        float fireSpreadDelay = 5f;
        float fireSpreadTimer = 0f;
        List<Fire> activeFires = new List<Fire>();
        float spreadChance = 0.2f;
        [SerializeField] List<Vector2Int> fireStartPositions;

        private List<Fire> fires;
        void Start()
        {
            for (int i = 0; i < 10000; i++)
            {
                GameObject go = GameObject.Instantiate(firePrefab);
                go.SetActive(false);
                firePool.Enqueue(go);
                

            }
            StartCoroutine(StartFires(3));

        }

        IEnumerator StartFires(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            foreach (var item in fireStartPositions)
            {
                Debug.Log("Fire started at " + item);
                GridNode node = GameManager.grid.Get(item.x, item.y);
                Debug.Log("Node at " + node.Position);
                SPawnFire(node);
            }
            yield return null;
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
                    Debug.Log("Checking node at " + n.Position);
                    if (UnityEngine.Random.Range(0, 1) < spreadChance)
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
            activeFires.Add(go.GetComponent<Fire>());
        }
    }
}
