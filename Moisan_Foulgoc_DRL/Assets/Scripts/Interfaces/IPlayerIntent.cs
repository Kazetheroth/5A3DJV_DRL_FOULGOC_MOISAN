using System.Collections.Generic;

namespace Interfaces
{
    public enum Intent
    {
        Nothing,
        WantToGoRight,
        WantToGoLeft,
        WantToGoTop,
        WantToGoBot
    }

    public interface IPlayerIntent
    {
        Intent GetPlayerIntent();
        List<List<ICell>> GetWorldCells();
        void SetWorldCells(List<List<ICell>> worldCells);

        void InitPlayerIntent();
    }
}