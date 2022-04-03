using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GildarGaming.LD50
{
    public class PlayerController : MonoBehaviour
    {
        
        public LayerMask collissionMask;
        Grid2D grid;
        ParticleSystem waterEffect;
        public float speed = 0.25f;
        public float rotationSPeed = 10f;
        private bool waterCannonON;
        [SerializeField] LayerMask weaponHitLayer;
        float weaponTimer = 0;
        float weaponDelay = 1f;

        private void Start()
        {
            grid = GameManager.grid;
            waterEffect = GetComponentInChildren<ParticleSystem>();


        }

        private void Update()
        {
            Vector3 movement = new Vector3(0, Input.GetAxis("Vertical"), 0); 
            transform.Rotate(new Vector3(0, 0, Input.GetAxis("Horizontal")) * -1, rotationSPeed * Time.deltaTime);
            if (movement.y > 0)
            {
                Vector3 newPosition = transform.position + transform.up * speed * Time.deltaTime; ;
                if (!Physics2D.Raycast(transform.position, transform.up, speed * Time.deltaTime, collissionMask))
                {
                    transform.position = newPosition;
                }
                Debug.DrawRay(transform.position, transform.up * newPosition.y, Color.red);

                //if (grid.IsWalkable((int)newPosition.x, (int)newPosition.y))
                //{
                //    
                //}


            }
            
            if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.WaterStorage > 0)
            {
                waterCannonON = true;
                waterEffect.Play();
            }
            if (waterCannonON)
            {
                GameManager.Instance.WaterStorage -= 2 * Time.deltaTime;
                 

                weaponTimer += Time.deltaTime;
                if (weaponTimer > weaponDelay)
                {
                    DealDamage();
                    weaponTimer = 0;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space) || GameManager.Instance.WaterStorage <= 0)
            {
                waterCannonON = false;
                waterEffect.Stop();

            }
        }

        private void DealDamage()
        {
            var hit = Physics2D.Raycast(transform.position, transform.forward, 1.5f, weaponHitLayer);
            if (hit.collider != null)
            {
                Health health = hit.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(10);
                    Debug.Log("Dealing damage");
                }
            }
        }
    }
}
