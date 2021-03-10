using Interfaces;
using UnityEngine;
using Vector2Int = Utils.Vector2Int;

namespace GridWORLDO
{
    public class GridWorldCell : ICell
    {
        public float reward;
        public CellType cellType;

        public Vector2Int position;

        public GameObject gameObject;

        public GridWorldCell(Vector2Int pos, CellType type, float reward)
        {
            position = pos;
            cellType = type;
            this.reward = reward;
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
        
        public Vector2Int GetPosition()
        {
            return position;
        }

        public GameObject GetCellGameObject()
        {
            return gameObject;
        }

        public void SetCellGameObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public void AddMaterialToCell(Material mat)
        {
            
        }

        public int GetPlayerId()
        {
            throw new System.NotImplementedException();
        }

        public float GetReward()
        {
            return reward;
        }
    }
}