using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace GridWORLDO
{
    public class GridWoldPlayer : IPlayer
    {
        //private IPlayer _playerImplementation;

        public ICell CurrentCell { set; get; }
        //public GridWorldState PlayerState { get; set; }

        public bool WantToGoTop(List<List<ICell>> worldCells)
        {
            if (worldCells[(int) CurrentCell.GetPosition().x].Count - 1 < (int) CurrentCell.GetPosition().z + 1)
            {
                return false;
            }

            ICell cellTest = worldCells[(int) CurrentCell.GetPosition().x][(int) CurrentCell.GetPosition().z + 1];

            if (cellTest.WhenInteract() != CellType.Obstacle)
            {
                CurrentCell = cellTest;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool WantToGoBot(List<List<ICell>> worldCells)
        {
            if ((int) CurrentCell.GetPosition().z - 1 < 0)
            {
                return false;
            }

            ICell cellTest = worldCells[(int) CurrentCell.GetPosition().x][(int) CurrentCell.GetPosition().z - 1];

            if (cellTest.WhenInteract() != CellType.Obstacle)
            {
                CurrentCell = cellTest;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool WantToGoLeft(List<List<ICell>> worldCells)
        {
            if ((int) CurrentCell.GetPosition().x - 1 < 0)
            {
                return false;
            }

            ICell cellTest = worldCells[(int) CurrentCell.GetPosition().x - 1][(int) CurrentCell.GetPosition().z];

            if (cellTest.WhenInteract() != CellType.Obstacle)
            {
                CurrentCell = cellTest;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool WantToGoRight(List<List<ICell>> worldCells)
        {
            if (worldCells.Count - 1 < (int) CurrentCell.GetPosition().x + 1)
            {
                return false;
            }

            ICell cellTest = worldCells[(int) CurrentCell.GetPosition().x + 1][(int) CurrentCell.GetPosition().z];

            if (cellTest.WhenInteract() != CellType.Obstacle)
            {
                CurrentCell = cellTest;
            }
            else
            {
                return false;
            }

            return true;
        }

        public Vector3 GetPosition()
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
