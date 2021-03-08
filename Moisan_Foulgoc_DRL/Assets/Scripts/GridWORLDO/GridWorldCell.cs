using Interfaces;
using UnityEngine;

namespace GridWORLDO
{
    public class GridWorldCell : ICell
    {
        public float reward;
        public CellType cellType;

        public Vector3 position;

        public GridWorldCell(Vector3 pos, CellType type, float reward)
        {
            position = pos;
            cellType = type;
        }

        public CellType WhenInteract()
        {
            return cellType;
        }

        public void SetCellType(CellType cellType)
        {
            this.cellType = cellType;
        }

        public CellType GetCellType()
        {
            return cellType;
        }

        public void SetReward(float reward)
        {
            this.reward = reward;
        }

        public Vector3 GetPosition()
        {
            return position;
        }

        public float GetReward()
        {
            return reward;
        }
    }
}