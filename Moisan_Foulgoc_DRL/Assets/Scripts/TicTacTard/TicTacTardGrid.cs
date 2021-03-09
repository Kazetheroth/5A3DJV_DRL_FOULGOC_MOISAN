using Interfaces;
using UnityEngine;
using Vector2Int = Utils.Vector2Int;

namespace TicTacTard
{
    public class TicTacTardGrid : ICell
    {
        public float reward;
        public CellType CellType;
        

        public CellType WhenInteract()
        {
            throw new System.NotImplementedException();
        }

        public void SetCellType(CellType cellType)
        {
            throw new System.NotImplementedException();
        }

        public CellType GetCellType()
        {
            throw new System.NotImplementedException();
        }

        public void SetReward(float reward)
        {
            throw new System.NotImplementedException();
        }

        public float GetReward()
        {
            throw new System.NotImplementedException();
        }

        Vector2Int ICell.GetPosition()
        {
            throw new System.NotImplementedException();
        }

        public GameObject GetCellGameObject()
        {
            throw new System.NotImplementedException();
        }

        public void SetCellGameObject(GameObject gameObject)
        {
            throw new System.NotImplementedException();
        }

        public void AddMaterialToCell(Material mat)
        {
            throw new System.NotImplementedException();
        }

        public Vector3 GetPosition()
        {
            throw new System.NotImplementedException();
        }
    }
}