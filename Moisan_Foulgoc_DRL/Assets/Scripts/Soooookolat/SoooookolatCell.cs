using Interfaces;
using UnityEngine;
using Vector2Int = Utils.Vector2Int;

namespace Soooookolat
{
    public class SooooookolatCell : ICell
    {
        private float reward;
        private CellType cellType;
        private Vector2Int position;
        private GameObject gameObject;

        public SooooookolatCell(float reward, CellType cellType, Vector2Int position)
        {
            this.reward = reward;
            this.cellType = cellType;
            this.position = position;
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