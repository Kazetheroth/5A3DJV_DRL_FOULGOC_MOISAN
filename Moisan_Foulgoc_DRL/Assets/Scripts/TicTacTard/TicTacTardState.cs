using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Vector2Int = Utils.Vector2Int;

namespace TicTacTard
{
    public class TicTacTardState : IGameState
    {

        public float value;
        public int reward;
        
        private List<List<TicTacTardGrid>> gridBoard;
        private float winScore;
        private int visits;

        public TicTacTardState()
        {
        }
        
        public int CheckVictory()
        {
            if (gridBoard[0][0] == gridBoard[0][1] && gridBoard[0][1] == gridBoard[0][2])
                return gridBoard[0][0].playerId;
            
            else if (gridBoard[1][0] == gridBoard[1][1] && gridBoard[1][1] == gridBoard[1][2])
                return gridBoard[1][0].playerId;
            
            else if (gridBoard[2][0] == gridBoard[2][1] && gridBoard[2][1] == gridBoard[2][2])
                return gridBoard[2][0].playerId;

            else if (gridBoard[0][0] == gridBoard[1][0] && gridBoard[1][0] == gridBoard[2][0])
                return gridBoard[0][0].playerId;
            
            else if (gridBoard[0][1] == gridBoard[1][1] && gridBoard[1][1] == gridBoard[2][1])
                return gridBoard[0][1].playerId;
            
            else if (gridBoard[0][2] == gridBoard[1][2] && gridBoard[1][2] == gridBoard[2][2])
                return gridBoard[0][2].playerId;

            else if (gridBoard[0][0] == gridBoard[1][1] && gridBoard[1][1] == gridBoard[2][2])
                return gridBoard[0][0].playerId;
            
            else if (gridBoard[0][2] == gridBoard[1][1] && gridBoard[1][1] == gridBoard[2][0])
                return gridBoard[0][2].playerId;
            
            else
                return -1;
        }

        public List<List<TicTacTardGrid>> Grid
        {
            get => gridBoard;
            set => gridBoard = value;
        }

        public float WinScore
        {
            get => winScore;
            set => winScore = value;
        }

        public int Visits
        {
            get => visits;
            set => visits = value;
        }

        
        
        
        
        public List<TicTacTardState> GetAllPossibleStates()
        {
            throw new System.NotImplementedException();
        }
        
        public void RandomStates() {
            throw new System.NotImplementedException();
        }

        public void SetPos(Vector3 pos)
        {
            throw new System.NotImplementedException();
        }

        public void SetPos(Vector2Int pos)
        {
            throw new System.NotImplementedException();
        }

        Vector2Int IGameState.GetPos()
        {
            throw new System.NotImplementedException();
        }

        public Vector3 GetPos()
        {
            throw new System.NotImplementedException();
        }

        public float GetValue()
        {
            throw new System.NotImplementedException();
        }

        public void SetValue(float value)
        {
            throw new System.NotImplementedException();
        }

        public List<List<ICell>> GetCells()
        {
            throw new System.NotImplementedException();
        }

        public void SetCells(List<List<ICell>> cells)
        {
            throw new System.NotImplementedException();
        }

        public void SetCells(List<List<TicTacTardGrid>> cells)
        {
            Grid = cells;
        }
    }
}