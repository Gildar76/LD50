using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD50
{
    public class CameraFollow : MonoBehaviour
    {
        private Camera camera;
        public GameObject player;
        void Start()
        {
            camera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            Vector3 newPos = player.transform.position;
            newPos.z = transform.position.z;
            newPos.x = transform.position.x;
            newPos.y = Vector3.Lerp(transform.position, newPos, 0.1f).y;
            transform.position = newPos;


        }

    }
}
