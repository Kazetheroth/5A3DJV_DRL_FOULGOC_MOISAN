using System.Collections.Generic;
using GridWORLDO;
using UnityEngine;

namespace Interfaces
{
    public interface IPlayer
    {
        bool WantToGoTop(List<List<ICell>> worldCells);
        bool WantToGoBot(List<List<ICell>> worldCells);
        bool WantToGoLeft(List<List<ICell>> worldCells);
        bool WantToGoRight(List<List<ICell>> worldCells);
    }
}