using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD50
{
    public class Fire : MonoBehaviour
    {
        Health fireHealth;
        public bool isBurning;
        private float burnTime = 10f;
        private float timer = 0;
        int burnDamage = 5;
        Health targetHealth;
        public GridNode node;

        private void Start()
        {
            fireHealth = GetComponent<Health>();
        }
        private void OnEnable()
        {
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }
        private void OnDisable()
        {
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().Stop();
        }
        private void Update()
        {
            if (fireHealth.IsDead)
            {
                GameManager.Instance.Money += 50;
                isBurning = false;
                node.IsOnFire = false;
                FireManager.Instance.AddFireBackToPool(this.gameObject);
                this.gameObject.SetActive(false);
                
            }
            if (isBurning)
            {
                timer += Time.deltaTime;
                if (timer >= burnTime)
                {
                    timer = 0;
                    if (targetHealth == null)
                    {
                        GameObject go = node.occupiedBy;
                        //Debug.Log(go.name);
                        if (go != null)
                        {
                           targetHealth = go.GetComponent<Health>();
                        }
                    } else
                    {
                        targetHealth.TakeDamage(burnDamage);
                        if (targetHealth.IsDead)
                        {


                            isBurning = false;

                            targetHealth.gameObject.SetActive(false);
                            //if (FireManager.Instance == null) Debug.Log("FireManager is null");
                            GameObject go = FireManager.Instance.GetBurntGround();

                            go.transform.position = transform.position;
                            node.occupiedBy = go;
                            node.Occupied = false;
                            go.SetActive(true);

                            this.gameObject.SetActive(false);
                        }
                    }
                    
                    

                }
            }
        }
    }
}
