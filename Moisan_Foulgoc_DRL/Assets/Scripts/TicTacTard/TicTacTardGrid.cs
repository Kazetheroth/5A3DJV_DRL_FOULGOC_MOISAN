using Interfaces;
using UnityEngine;

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

        public Vector3 GetPosition()
        {
            throw new System.NotImplementedException();
        }
    }
}