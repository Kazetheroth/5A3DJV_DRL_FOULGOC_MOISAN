using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Vector2Int = Utils.Vector2Int;

namespace Soooookolat
{
    public class SooooookolatPlayer : IPlayer
    {
        public ICell CurrentCell { set; get; }
        public List<List<ICell>> currentWorld;
        
        public bool WantToGoTop(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            if (worldCells[(int) CurrentCell.GetPosition().x].Count - 1 < (int) CurrentCell.GetPosition().y + 1)
            {
                return false;
            }

            ICell cellTest = worldCells[(int) CurrentCell.GetPosition().x][(int) CurrentCell.GetPosition().y + 1];

            CellType intractionType = cellTest.WhenInteract();
            if (intractionType != CellType.Obstacle)
            {
                if (intractionType == CellType.Box)
                {
                    Debug.Log("Box collider");
                    if (worldCells[(int) cellTest.GetPosition().x].Count - 1 > (int) cellTest.GetPosition().y + 1)
                    {
                        Debug.Log("Box collider 2");
                        ICell boxTest = worldCells[(int) cellTest.GetPosition().x][(int) cellTest.GetPosition().y + 1];
                        if (boxTest.WhenInteract() != CellType.Box && boxTest.WhenInteract() != CellType.Obstacle)
                        {
                            Debug.Log("Box collider 3 ");
                            ICell emptyCell = new SooooookolatCell(SoooookolatLevels.GetRewardFromType(CellType.Empty), CellType.Empty, cellTest.GetPosition());
                            worldCells[(int) cellTest.GetPosition().x][(int) cellTest.GetPosition().y + 1] = cellTest;
                            worldCells[(int) cellTest.GetPosition().x][(int) cellTest.GetPosition().y] = emptyCell;
                            Utils.ArrayDebug.PrintArray(worldCells, worldCells.Count, worldCells[0].Count);
                            if (setNewCell)
                            {
                                CurrentCell = cellTest;
                                SoooookolatGame.cells = currentWorld = worldCells;
                            }
                        }
                    }
                }
                else
                {
                    if (setNewCell)
                    {
                        CurrentCell = cellTest;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool WantToGoBot(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            if ((int) CurrentCell.GetPosition().y - 1 < 0)
            {
                return false;
            }

            ICell cellTest = worldCells[(int) CurrentCell.GetPosition().x][(int) CurrentCell.GetPosition().y - 1];

            CellType intractionType = cellTest.WhenInteract();
            if (intractionType != CellType.Obstacle)
            {
                if (intractionType == CellType.Box)
                {
                    Debug.Log("Box collider " + (cellTest.GetPosition().y - 1));
                    if (0 <= (int) cellTest.GetPosition().y - 1)
                    {
                        Debug.Log("Box collider 2");
                        ICell boxTest = worldCells[(int) cellTest.GetPosition().x][(int) cellTest.GetPosition().y - 1];
                        if (boxTest.WhenInteract() != CellType.Box && boxTest.WhenInteract() != CellType.Obstacle)
                        {
                            Debug.Log("Box collider 3");
                            ICell emptyCell = new SooooookolatCell(SoooookolatLevels.GetRewardFromType(CellType.Empty), CellType.Empty, cellTest.GetPosition());
                            worldCells[(int) cellTest.GetPosition().x][(int) cellTest.GetPosition().y - 1] = cellTest;
                            worldCells[(int) cellTest.GetPosition().x][(int) cellTest.GetPosition().y] = emptyCell;
                            Utils.ArrayDebug.PrintArray(worldCells, worldCells.Count, worldCells[0].Count);
                            if (setNewCell)
                            {
                                CurrentCell= cellTest;
                                SoooookolatGame.cells = currentWorld = worldCells;
                            }
                        }
                    }
                }
                else
                {
                    if (setNewCell)
                    {
                        CurrentCell = cellTest;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool WantToGoLeft(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            if ((int) CurrentCell.GetPosition().x - 1 < 0)
            {
                return false;
            }

            ICell cellTest = worldCells[(int) CurrentCell.GetPosition().x - 1][(int) CurrentCell.GetPosition().y];

            CellType intractionType = cellTest.WhenInteract();
            if (intractionType != CellType.Obstacle)
            {
                if (intractionType == CellType.Box)
                {
                    Debug.Log("Box collider");
                    if (0 <= (int) cellTest.GetPosition().x - 1)
                    {
                        Debug.Log("Box collider 2");
                        ICell boxTest = worldCells[(int) cellTest.GetPosition().x - 1][(int) cellTest.GetPosition().y];
                        if (boxTest.WhenInteract() != CellType.Box && boxTest.WhenInteract() != CellType.Obstacle)
                        {
                            Debug.Log("Box collider 3");
                            ICell emptyCell = new SooooookolatCell(SoooookolatLevels.GetRewardFromType(CellType.Empty), CellType.Empty, cellTest.GetPosition());
                            worldCells[(int) cellTest.GetPosition().x - 1][(int) cellTest.GetPosition().y] = cellTest;
                            worldCells[(int) cellTest.GetPosition().x][(int) cellTest.GetPosition().y] = emptyCell;
                            Utils.ArrayDebug.PrintArray(worldCells, worldCells.Count, worldCells[0].Count);
                            if (setNewCell)
                            {
                                CurrentCell = cellTest;
                                SoooookolatGame.cells = currentWorld = worldCells;
                            }
                        }
                    }
                }
                else
                {
                    if (setNewCell)
                    {
                        CurrentCell = cellTest;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool WantToGoRight(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            if (worldCells.Count - 1 < (int) CurrentCell.GetPosition().x + 1)
            {
                return false;
            }

            int newX = CurrentCell.GetPosition().x + 1;
            int newY = CurrentCell.GetPosition().y;
            ICell cellTest = worldCells[newX][newY];

            Debug.Log($"Cells : {CurrentCell.GetPosition().x} - {newX} - {cellTest.GetPosition().x}");

            CellType intractionType = cellTest.WhenInteract();
            if (intractionType != CellType.Obstacle)
            {
                if (intractionType == CellType.Box)
                {
                    Debug.Log("Box collider");
                    if (worldCells.Count - 1 > (int) cellTest.GetPosition().x + 1)
                    {
                        Debug.Log("Box collider 2");
                        int boxX = newX + 1;
                        int boxY = newY;
                        ICell boxTest = worldCells[boxX][boxY];
                        Debug.Log(boxTest.WhenInteract());
                        if (boxTest.WhenInteract() != CellType.Box && boxTest.WhenInteract() != CellType.Obstacle)
                        {
                            Debug.Log("Box collider 3");
                            ICell emptyCell = new SooooookolatCell(SoooookolatLevels.GetRewardFromType(CellType.Empty), CellType.Empty, cellTest.GetPosition());
                            worldCells[boxX][boxY] = cellTest;
                            worldCells[newX][newY] = emptyCell;
                            Utils.ArrayDebug.PrintArray(SoooookolatGame.cells, worldCells.Count, worldCells[0].Count);
                        }
                        else
                        {
                            Debug.Log("Current : " + CurrentCell.GetPosition());
                            Debug.Log("CellTest : " + cellTest.GetPosition());
                            Debug.Log("BoxTest : " + boxTest.GetPosition());
                            Utils.ArrayDebug.PrintArray(worldCells, worldCells.Count, worldCells[0].Count);
                        }
                    }
                }
                else
                {
                    Debug.Log(intractionType);
                    Utils.ArrayDebug.PrintArray(SoooookolatGame.cells, worldCells.Count, worldCells[0].Count);
                }
            }
            else
            {
                Debug.Log(intractionType);
                Utils.ArrayDebug.PrintArray(SoooookolatGame.cells, worldCells.Count, worldCells[0].Count);
                return false;
            }
            if (setNewCell)
            {
                Debug.Log("Current : " + CurrentCell.GetPosition());
                Debug.Log("CellTest : " + cellTest.GetPosition());
                CurrentCell = cellTest;
                CurrentCell.SetPostion(cellTest.GetPosition());
                SoooookolatGame.cells = worldCells;
            }

            return true;
        }

        public Vector2Int GetPosition()
        {
            return CurrentCell.GetPosition();
        }

        public List<List<ICell>> GetWoldState()
        {
            return currentWorld;
        }

        public void SetCell(ICell cell)
        {
            CurrentCell = cell;
        }

        public ICell GetCell()
        {
            return CurrentCell;
        }
    }
}