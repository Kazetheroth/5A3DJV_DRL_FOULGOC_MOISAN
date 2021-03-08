using Interfaces;
using UnityEngine;

namespace GridWORLDO
{
    public class GridWorldIntent : IPlayerIntent
    {
        public Intent GetPlayerIntent()
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
    }
}