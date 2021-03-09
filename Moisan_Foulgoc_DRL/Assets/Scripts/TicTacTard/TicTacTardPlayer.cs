using System.Collections.Generic;
using Interfaces;
using Utils;

namespace TicTacTard
{
    public class TicTacTardPlayer : IPlayer
    {
        public bool WantToGoTop(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            throw new System.NotImplementedException();
        }

        public bool WantToGoBot(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            throw new System.NotImplementedException();
        }

        public bool WantToGoLeft(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            throw new System.NotImplementedException();
        }

        public bool WantToGoRight(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            throw new System.NotImplementedException();
        }

        public Vector2Int GetPosition()
        {
            throw new System.NotImplementedException();
        }

        public void SetCell(ICell cell)
        {
            throw new System.NotImplementedException();
        }

        public ICell GetCell()
        {
            throw new System.NotImplementedException();
        }
    }
}