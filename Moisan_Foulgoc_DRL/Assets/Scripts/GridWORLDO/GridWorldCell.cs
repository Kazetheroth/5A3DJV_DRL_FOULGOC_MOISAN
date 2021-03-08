using Interfaces;
using UnityEngine;

namespace GridWORLDO
{
    public class GridWorldCell : ICell
    {
        public float reward;
        public CellType cellType;

        public Vector3 position;

        public CellType WhenInteract()
        {
            return cellType;
        }

        public float GetReward()
        {
            return reward;
        }
    }
}