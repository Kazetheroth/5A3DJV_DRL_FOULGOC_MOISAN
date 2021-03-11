using System.Collections.Generic;
using Interfaces;
using TMPro;
using UnityEngine;
using Vector2Int = Utils.Vector2Int;

namespace TicTacTard
{
    public class TicTacTardStateWithAction : TicTacTardState
    {
        public Intent intent;
        public float reward;
        public TicTacTardStateWithAction prevState;

        public TicTacTardStateWithAction()
        {
            
        }
        
        public TicTacTardStateWithAction(TicTacTardState state, Intent intent) : base(state)
        {
            this.intent = intent;
        }
    }
    
    public class TicTacTardState : IGameState
    {
        public float value;

        private List<List<ICell>> gridBoard;
        private float winScore;
        private int visits;

        public int nbActionPlayed;

        public TicTacTardState()
        {
            visits = 0;
            winScore = 0;
            nbActionPlayed = 0;
        }

        public TicTacTardState(TicTacTardState state)
        {
            visits = state.visits;
            winScore = state.winScore;
            nbActionPlayed = state.nbActionPlayed;

            List<List<ICell>> oldGridBoard = state.Grid;
            gridBoard = new List<List<ICell>>();
            
            for (int i = 0; i < oldGridBoard.Count; ++i)
            {
                gridBoard.Add(new List<ICell>());

                for (int j = 0; j < oldGridBoard[i].Count; ++j)
                {
                    gridBoard[i].Add(new TicTacTardCell(oldGridBoard[i][j] as TicTacTardCell, false));
                }
            }
        }

        public TicTacTardState(TicTacTardState state, Vector2Int newToken, string tokenToPlace, bool updateDisplay)
        {
            visits = state.visits;
            winScore = state.winScore;

            List<List<ICell>> oldGridBoard = state.Grid;
            gridBoard = new List<List<ICell>>();
            nbActionPlayed = state.nbActionPlayed + 1;
            
            for (int i = 0; i < oldGridBoard.Count; ++i)
            {
                gridBoard.Add(new List<ICell>());

                for (int j = 0; j < oldGridBoard[i].Count; ++j)
                {
                    gridBoard[i].Add(new TicTacTardCell(oldGridBoard[i][j] as TicTacTardCell, updateDisplay));

                    if (i == newToken.x && j == newToken.y)
                    {
                        ((TicTacTardCell) gridBoard[i][j]).token = tokenToPlace;
                    }
                }
            }

            if (updateDisplay)
            {
                gridBoard[newToken.x][newToken.y].GetCellGameObject().transform.GetChild(0).GetComponent<TextMeshPro>().text = tokenToPlace;
            }
        }
        
        public void DisplayGrid()
        {
            string grid = "";

            for (int i = 2; i >= 0; --i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    grid += (Grid[j][i] as TicTacTardCell).token + "\t";
                }

                grid += "\n";
            }

            Debug.Log(grid);
        }
        
        public List<List<ICell>> Grid
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

        public bool IsSameState(TicTacTardState state)
        {
            bool isSameState = true;

            for (int i = 0; i < gridBoard.Count; ++i)
            {
                for (int j = 0; j < gridBoard[i].Count; ++j)
                {
                    if (((TicTacTardCell) state.gridBoard[i][j]).token != ((TicTacTardCell) gridBoard[i][j]).token)
                    {
                        isSameState = false;
                        break;
                    }
                }

                if (!isSameState)
                {
                    break;
                }
            }

            return isSameState;
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
            return gridBoard;
        }

        public void SetCells(List<List<ICell>> cells)
        {
            gridBoard = cells;
        }
    }
}