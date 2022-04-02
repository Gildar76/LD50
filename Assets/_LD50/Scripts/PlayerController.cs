using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD50
{
    public class PlayerController : MonoBehaviour
    {
        Grid2D grid;
        ParticleSystem waterEffect;
        public float speed = 0.25f;
        public float rotationSPeed = 10f;
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
                if (grid.IsWalkable((int)newPosition.x, (int)newPosition.y))
                {
                    transform.position = newPosition;
                }

                
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {

                    waterEffect.Play();
                
            }
        }


    }
}
