using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace GridWORLDO
{
    public class GridWorldIntent : IPlayerIntent
    {
        public Intent GetPlayerIntent(int currentX, int currentY)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                return Intent.WantToGoTop;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                return Intent.WantToGoBot;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                return Intent.WantToGoLeft;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                return Intent.WantToGoRight;
            }

            return Intent.Nothing;
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

        public void InitPlayerIntent()
        {
            return;
        }
    }
}