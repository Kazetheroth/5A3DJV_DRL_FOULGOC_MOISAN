using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace TicTacTard
{
    public class TicTacTardState : IGameState
    {
        private List<List<ICell>> grid;
        private float winScore;
        private int visits;

        public TicTacTardState()
        {
        }

        public List<List<ICell>> Grid
        {
            get => grid;
            set => grid = value;
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
            return Grid;
        }

        public void SetCells(List<List<ICell>> cells)
        {
            Grid = cells;
        }
    }
}