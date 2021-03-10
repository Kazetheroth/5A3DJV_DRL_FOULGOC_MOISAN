using System.Collections.Generic;
using Interfaces;
using Utils;

namespace TicTacTard
{
    public class TicTacTardPlayer : IPlayer
    {
        private int id;
        private bool isHuman;
        private string token;

        public TicTacTardPlayer(int id, bool isHuman, string token)
        {
            this.id = id;
            this.isHuman = isHuman;
            this.token = token;
        }

        public int ID
        {
            get => id;
            set => id = value;
        }

        public bool IsHuman
        {
            get => isHuman;
            set => isHuman = value;
        }

        public string Token
        {
            get => token;
            set => token = value;
        }

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