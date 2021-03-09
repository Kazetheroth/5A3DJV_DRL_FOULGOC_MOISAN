using System.Collections.Generic;
using Utils;

namespace Interfaces
{
    public interface IPlayer
    {
        bool WantToGoTop(List<List<ICell>> worldCells, bool setNewCell = false);
        bool WantToGoBot(List<List<ICell>> worldCells, bool setNewCell = false);
        bool WantToGoLeft(List<List<ICell>> worldCells, bool setNewCell = false);
        bool WantToGoRight(List<List<ICell>> worldCells, bool setNewCell = false);

        Vector2Int GetPosition();

        void SetCell(ICell cell);
        ICell GetCell();
    }
}