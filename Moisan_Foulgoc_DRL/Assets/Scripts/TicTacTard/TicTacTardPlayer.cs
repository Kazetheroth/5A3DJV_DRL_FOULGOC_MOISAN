using System.Collections.Generic;
using Interfaces;

namespace TicTacTard
{
    public class TicTacTardPlayer : IPlayerIntent
    {
        public Intent GetPlayerIntent()
        {
            throw new System.NotImplementedException();
        }

        public IPlayer GetPlayer()
        {
            throw new System.NotImplementedException();
        }

        public void SetPlayer(IPlayer player)
        {
            throw new System.NotImplementedException();
        }

        public List<List<ICell>> GetWorldCells()
        {
            throw new System.NotImplementedException();
        }

        public void SetWorldCells(List<List<ICell>> worldCells)
        {
            throw new System.NotImplementedException();
        }
    }
}