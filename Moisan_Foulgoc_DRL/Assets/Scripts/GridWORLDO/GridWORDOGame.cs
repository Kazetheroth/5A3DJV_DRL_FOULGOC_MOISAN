using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2Int = Utils.Vector2Int;

namespace GridWORLDO
{
    public class GridWORDOGame : IGame
    {
        public enum Algo
        {
            PolicyImprovement,
            ValueIteration
        }
        
        public static Algo chosenAlgo;
        private IPlayer player;
        private IPlayerIntent playerIntent;

        private List<List<ICell>> cells;

        public const int MAX_CELLS_PER_LINE = 10;
        public const int MAX_CELLS_PER_COLUMN = 10;

        public int xGoal;
        public int yGoal;

        private bool gameStart;

        public bool InitGame()
        {
            gameStart = false;
            cells = new List<List<ICell>>();

            for (int i = 0; i < MAX_CELLS_PER_LINE; ++i)
            {
                List<ICell> cellsPerLine = new List<ICell>();
                
                for (int j = 0; j < MAX_CELLS_PER_COLUMN; ++j)
                {
                    CellType type = Random.Range(0, 10) > 8 ? CellType.Obstacle : CellType.Empty;
                    
                    cellsPerLine.Add(new GridWorldCell(
                        new Vector2Int(i, j),
                        type, 
                        type == CellType.Empty ? -1 : -1000));
                }

                cells.Add(cellsPerLine);
            }

            xGoal = Random.Range(0, MAX_CELLS_PER_LINE);
            yGoal = Random.Range(0, MAX_CELLS_PER_COLUMN);

            cells[xGoal][yGoal].SetCellType(CellType.EndGoal);

            int xPlayer = 0;
            int yPlayer = 0;

            do
            {
                xPlayer = Random.Range(0, MAX_CELLS_PER_LINE);
                yPlayer = Random.Range(0, MAX_CELLS_PER_COLUMN);
            } while (xPlayer == xGoal && yPlayer == yGoal);

            cells[xPlayer][yPlayer].SetCellType(CellType.Player);
            cells[xPlayer][yPlayer].SetReward(0);

            player = new GridWoldPlayer();
            player.SetCell(cells[xPlayer][yPlayer]);

            SetInitialReward(xGoal, yGoal);
            PrintArray();

            return true;
        }

        private void PrintArray()
        {
            string medhitoutcourt = "";

            for (int i = MAX_CELLS_PER_LINE - 1; i >= 0; --i)
            {
                for (int j = 0; j < MAX_CELLS_PER_COLUMN; ++j)
                {
                    medhitoutcourt += cells[j][i].GetReward() + "\t";
                }

                medhitoutcourt += "\n";
            }

            Debug.Log(medhitoutcourt);
        }

        private void SetInitialReward(int xGoal, int yGoal)
        {
            cells[xGoal][yGoal].SetReward(20);

            for (int i = 0; i < MAX_CELLS_PER_LINE; ++i)
            {
                for (int j = 0; j < MAX_CELLS_PER_COLUMN; ++j)
                {
                    if (xGoal != i || yGoal != j)
                    {
                        if (cells[i][j].GetCellType() == CellType.Obstacle)
                        {
                            cells[i][j].SetReward(-1000);
                        }
                        else
                        {
                            // cells[i][j].SetReward(20 - (Math.Abs(xGoal - i) + Math.Abs(yGoal - j)));
                            cells[i][j].SetReward(-1);
                        }
                    }
                }
            }
        }

        public List<List<ICell>> GetCells()
        {
            return cells;
        }

        public void InitIntent(bool isHuman)
        {
            if (isHuman)
            {
                playerIntent = new GridWorldIntent();
            }
            else
            {
                playerIntent = new GridWorldAndroidIntent(MAX_CELLS_PER_LINE, MAX_CELLS_PER_COLUMN, xGoal, yGoal, cells);
            }

            playerIntent.InitPlayerIntent();

            gameStart = true;
        }

        public static bool CanMove(IPlayer player, List<List<ICell>> worldCells, Intent intent, bool saveNewPlayerCell = false)
        {
            bool canMove = false;

            switch (intent)
            {
                case Intent.WantToGoBot:
                    canMove = player.WantToGoBot(worldCells, saveNewPlayerCell);
                    break;
                case Intent.WantToGoLeft:
                    canMove = player.WantToGoLeft(worldCells, saveNewPlayerCell);
                    break;
                case Intent.WantToGoTop:
                    canMove = player.WantToGoTop(worldCells, saveNewPlayerCell);
                    break;
                case Intent.WantToGoRight:
                    canMove = player.WantToGoRight(worldCells, saveNewPlayerCell);
                    break;
            }

            return canMove;
        }
        
        public bool UpdateGame()
        {
            if (!gameStart)
            {
                return false;
            }
            
            bool isMoving = CanMove(
                GetPlayer(), 
                GetCells(), 
                playerIntent.GetPlayerIntent(player.GetPosition().x, player.GetPosition().y), 
                true);
            
            if (isMoving && GetPlayer().GetCell().GetCellType() == CellType.EndGoal)
            {
                EndGame();
            }

            Controller.currentPlayerObject.transform.position = new Vector3(player.GetPosition().x, 0, player.GetPosition().y);

            return true;
        }

        public bool EndGame()
        {
            Debug.Log("You won");
            return true;
        }

        public IPlayer GetPlayer()
        {
            return player;
        }
    }
}