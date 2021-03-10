using Interfaces;
using UnityEngine;
using Vector2Int = Utils.Vector2Int;

namespace TicTacTard
{
    public class TicTacTardGrid : ICell
    {
        public float reward;
        public CellType CellType;
        public int playerId;
        public Vector2Int position;
        public GameObject gameObject;

        public TicTacTardGrid(Vector2Int position, CellType CellType)
        {
            this.position = position;
            this.CellType = CellType;
        }

        public Vector2Int GetPosition()
        {
            return position;
        }

        public int PlayerId
        {
            get => playerId;
            set => playerId = value;
        }

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
            return CellType;
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
            throw new System.NotImplementedException();
        }

        public int GetPlayerId()
        {
            return PlayerId;
        }
    }
}