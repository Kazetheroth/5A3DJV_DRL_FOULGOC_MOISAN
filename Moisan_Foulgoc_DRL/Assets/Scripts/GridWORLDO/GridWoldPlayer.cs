using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Vector2Int = Utils.Vector2Int;

namespace GridWORLDO
{
    public class GridWoldPlayer : IPlayer
    {
        //private IPlayer _playerImplementation;

        public ICell CurrentCell { set; get; }
        //public GridWorldState PlayerState { get; set; }

        public bool WantToGoTop(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            if (worldCells[(int) CurrentCell.GetPosition().x].Count - 1 < (int) CurrentCell.GetPosition().y + 1)
            {
                return false;
            }

            ICell cellTest = worldCells[(int) CurrentCell.GetPosition().x][(int) CurrentCell.GetPosition().y + 1];

            if (cellTest.WhenInteract() != CellType.Obstacle)
            {
                if (setNewCell)
                {
                    CurrentCell = cellTest;
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

            if (cellTest.WhenInteract() != CellType.Obstacle)
            {
                if (setNewCell)
                {
                    CurrentCell = cellTest;
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

            if (cellTest.WhenInteract() != CellType.Obstacle)
            {
                if (setNewCell)
                {
                    CurrentCell = cellTest;
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

            if (cellTest.WhenInteract() != CellType.Obstacle)
            {
                if (setNewCell)
                {
                    CurrentCell = cellTest;
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
