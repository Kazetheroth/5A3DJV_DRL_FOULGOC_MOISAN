using System.Collections.Generic;
using System.Numerics;
using GridWORLDO;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Interfaces
{
    public interface IPlayer
    {
        bool WantToGoTop(List<List<ICell>> worldCells);
        bool WantToGoBot(List<List<ICell>> worldCells);
        bool WantToGoLeft(List<List<ICell>> worldCells);
        bool WantToGoRight(List<List<ICell>> worldCells);

        Vector3 GetPosition();

        void SetCell(ICell cell);
        ICell GetCell();
    }
}