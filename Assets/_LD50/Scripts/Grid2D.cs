using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD50
{
    public class Grid2D
    {
        public int width;
        public int height;
        public GridNode[,] grid;

        public Grid2D(int width, int height)
        {
            this.width = width;
            this.height = height;
            grid = new GridNode[width, height];
        }

        public List<GridNode> FindNeighbour(GridNode node)
        {
            List<GridNode> neighbours = new List<GridNode>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.Position.x + x;
                    int checkY = node.Position.y + y;

                    if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;

        }
        public void Set(int x, int y, GridNode value)
        {
            grid[x, y] = value;
        }

        public GridNode Get(int x, int y)
        {
            return grid[x, y];
        }

        public void Clear()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid[x, y] = null;
                }
            }
        }

    }       




}
