using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IGame
    {
        bool InitGame();
        bool UpdateGame();
        bool EndGame();

        List<List<ICell>> GetCells();
    }
}