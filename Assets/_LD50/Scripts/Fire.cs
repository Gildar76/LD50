using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD50
{
    public class Fire : MonoBehaviour
    {
        public bool isBurning;
        private float burnTime = 60f;
        private float timer = 0;
        public GridNode node;
        private void Update()
        {
            if (isBurning)
            {
                timer += Time.deltaTime;
                if (timer >= burnTime)
                {
                    timer = 0;
                    isBurning = false;
                    enabled = false;
                }
            }
        }
    }
}
