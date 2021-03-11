using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Vector2Int = Utils.Vector2Int;

namespace Soooookolat
{
    public class SooooookolatPlayer : IPlayer
    {
        public ICell CurrentCell { set; get; }
        
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
                        if (boxTest.WhenInteract() != CellType.Box || boxTest.WhenInteract() != CellType.Obstacle)
                        {
                            Debug.Log("Box collider 3 ");
                            worldCells[(int) cellTest.GetPosition().x][(int) cellTest.GetPosition().y + 1] = cellTest;
                            if (setNewCell)
                            {
                                CurrentCell = cellTest;
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
                        if (boxTest.WhenInteract() != CellType.Box || boxTest.WhenInteract() != CellType.Obstacle)
                        {
                            Debug.Log("Box collider 3");
                            worldCells[(int) cellTest.GetPosition().x][(int) cellTest.GetPosition().y - 1] = cellTest;
                            if (setNewCell)
                            {
                                CurrentCell = cellTest;
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
                        if (boxTest.WhenInteract() != CellType.Box || boxTest.WhenInteract() != CellType.Obstacle)
                        {
                            Debug.Log("Box collider 3");
                            worldCells[(int) cellTest.GetPosition().x - 1][(int) cellTest.GetPosition().y] = cellTest;
                            if (setNewCell)
                            {
                                CurrentCell = cellTest;
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

            ICell cellTest = worldCells[(int) CurrentCell.GetPosition().x + 1][(int) CurrentCell.GetPosition().y];

            CellType intractionType = cellTest.WhenInteract();
            if (intractionType != CellType.Obstacle)
            {
                if (intractionType == CellType.Box)
                {
                    Debug.Log("Box collider");
                    if (worldCells.Count - 1 > (int) cellTest.GetPosition().x + 1)
                    {
                        Debug.Log("Box collider 2");
                        ICell boxTest = worldCells[(int) cellTest.GetPosition().x + 1][(int) cellTest.GetPosition().y];
                        if (boxTest.WhenInteract() != CellType.Box || boxTest.WhenInteract() != CellType.Obstacle)
                        {
                            Debug.Log("Box collider 3");
                            worldCells[(int) cellTest.GetPosition().x + 1][(int) cellTest.GetPosition().y] = cellTest;
                            if (setNewCell)
                            {
                                CurrentCell = cellTest;
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

        public Vector2Int GetPosition()
        {
            return CurrentCell.GetPosition();
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