using System;
using System.Collections.Generic;
using Interfaces;
using UnityEditor;
using UnityEngine;
using static Controller;
using Random = UnityEngine.Random;

namespace GridWORLDO
{
    public class GridWORDOGame : IGame
    {   
        private IPlayer player;
        private IPlayerIntent playerIntent;

        private List<List<ICell>> cells;

        private const int MAX_CELLS_PER_LINE = 4;
        private const int MAX_CELLS_PER_COLUMN = 4;

        public bool InitGame(bool isHuman)
        {
            cells = new List<List<ICell>>();

            for (int i = 0; i < MAX_CELLS_PER_LINE; ++i)
            {
                List<ICell> cellsPerLine = new List<ICell>();
                
                for (int j = 0; j < MAX_CELLS_PER_COLUMN; ++j)
                {
                    CellType type = Random.Range(0, 10) > 8 ? CellType.Obstacle : CellType.Empty;
                    
                    cellsPerLine.Add(new GridWorldCell(
                        new Vector3(i, 0, j),
                        type, 
                        type == CellType.Empty ? 0 : -1000));
                }

                cells.Add(cellsPerLine);
            }

            int xGoal = Random.Range(0, MAX_CELLS_PER_LINE);
            int yGoal = Random.Range(0, MAX_CELLS_PER_COLUMN);

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
            
            if (isHuman)
            {
                playerIntent = new GridWorldIntent();
            }
            else
            {
                playerIntent = new GridWorldAndroidIntent(MAX_CELLS_PER_LINE, MAX_CELLS_PER_COLUMN, cells, player);
            }

            return true;
        }

        private void PrintArray()
        {
            string medhimemmerde = "";

            for (int i = MAX_CELLS_PER_LINE - 1; i >= 0; --i)
            {
                for (int j = 0; j < MAX_CELLS_PER_COLUMN; ++j)
                {
                    medhimemmerde += cells[j][i].GetReward() + "\t";
                }

                medhimemmerde += "\n";
            }

            Debug.Log(medhimemmerde);
        }

        private void SetInitialReward(int xGoal, int yGoal)
        {
            cells[xGoal][yGoal].SetReward(1000);

            int x = xGoal;
            int y = yGoal;
            bool wantToGoParse = false;
            
            float  newReward;
            
            if (x - 1 < 0 && y + 1 < MAX_CELLS_PER_COLUMN)
            {
                x = MAX_CELLS_PER_LINE - 1;
                y = y + 1;
                wantToGoParse = true;
            } else if (x - 1 >= 0)
            {
                x = x - 1;
                wantToGoParse = true;
            }

            newReward = 1000 - Math.Abs(xGoal - x) - Math.Abs(yGoal - y);
            if (wantToGoParse)
            {
                SetRewardRecursif(x, y,xGoal, yGoal, newReward, true);
            }
            
            x = xGoal;
            y = yGoal;
            wantToGoParse = false;

            if (x + 1 > MAX_CELLS_PER_LINE - 1 && y - 1 >= 0)
            {
                x = 0;
                y = y - 1;
                wantToGoParse = true;
            } else if (x + 1 <= MAX_CELLS_PER_LINE - 1)
            {
                x = x + 1;
                wantToGoParse = true;
            }

            newReward = 1000 - Math.Abs(xGoal - x) - Math.Abs(yGoal - y);
            Debug.Log(newReward);
            if (wantToGoParse)
            {
                SetRewardRecursif(x, y,xGoal, yGoal, newReward, false);
            }
        }

        private void SetRewardRecursif(int x, int y, int endX, int endY, float reward, bool goLeft)
        {
            cells[x][y].SetReward(reward + cells[x][y].GetReward());

            if (goLeft)
            {
                if (x - 1 < 0 && y + 1 < MAX_CELLS_PER_COLUMN)
                {
                    x = MAX_CELLS_PER_LINE - 1;
                    y = y + 1;
                } else if (x - 1 >= 0)
                {
                    x = x - 1;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (x + 1 > MAX_CELLS_PER_LINE - 1 && y - 1 >= 0)
                {
                    x = 0;
                    y = y - 1;
                } else if (x + 1 <= MAX_CELLS_PER_LINE - 1)
                {
                    x = x + 1;
                }
                else
                {
                    return;
                }
            }

            float newReward = 1000 - Math.Abs(endX - x) - Math.Abs(endY - y);
            SetRewardRecursif(x, y, endX, endY, newReward, goLeft);
        }

        public List<List<ICell>> GetCells()
        {
            return cells;
        }

        public static bool CanMove(IPlayer player, List<List<ICell>> worldCells, Intent intent)
        {
            bool canMove = true;
            
            switch (intent)
            {
                case Intent.WantToGoBot:
                    canMove = player.WantToGoBot(worldCells);
                    break;
                case Intent.WantToGoLeft:
                    canMove = player.WantToGoLeft(worldCells);
                    break;
                case Intent.WantToGoTop:
                    canMove = player.WantToGoTop(worldCells);
                    break;
                case Intent.WantToGoRight:
                    canMove = player.WantToGoRight(worldCells);
                    break;
            }

            return canMove;
        }
        
        public bool UpdateGame()
        {
            bool isMoving = CanMove(GetPlayer(), GetCells(), playerIntent.GetPlayerIntent());

            if (isMoving && GetPlayer().GetCell().GetCellType() == CellType.EndGoal)
            {
                EndGame();
            }

            currentPlayerObject.transform.position = player.GetPosition();

            return true;
        }

        public bool EndGame()
        {
            throw new System.NotImplementedException();
        }

        public IPlayer GetPlayer()
        {
            return player;
        }
    }
}