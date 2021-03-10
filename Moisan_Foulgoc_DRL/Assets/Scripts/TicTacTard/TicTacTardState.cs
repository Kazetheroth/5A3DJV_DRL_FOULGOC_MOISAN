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
        
        private List<List<TicTacTardCell>> gridBoard;
        private float winScore;
        private int visits;

        public TicTacTardState()
        {
        }

        public List<List<TicTacTardCell>> Grid
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

        public void SetCells(List<List<TicTacTardCell>> cells)
        {
            Grid = cells;
        }
    }
}