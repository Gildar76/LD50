using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD50
{
    public class GridNode
    {
        Vector2Int position;
        public Vector2Int Position
        {
            get { return position; }
            set { position = value; }
        }
        bool occupied;
        public bool Occupied
        {
            get { return occupied; }
            set { occupied = value; }
        }
        bool isOnFire;
        public bool IsOnFire
        {
            get { return isOnFire; }
            set { isOnFire = value; }
        }
        bool isBurned;
        public bool IsBurned
        {
            get { return isBurned; }
            set { isBurned = value; }
        }

        public bool IsWalkable { get; internal set; }
        public GameObject OccupiedBy { get; internal set; }

        public GameObject occupiedBy;
    }
}
