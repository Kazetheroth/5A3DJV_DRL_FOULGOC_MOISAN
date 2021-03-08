using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IGame
    {
        bool InitGame(bool isHuman);
        bool UpdateGame();
        bool EndGame();

        IPlayer GetPlayer();
        List<List<ICell>> GetCells();
    }
}