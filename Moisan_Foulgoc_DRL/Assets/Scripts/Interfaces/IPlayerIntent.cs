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
        IPlayer GetPlayer();
        void SetPlayer(IPlayer player);
        List<List<ICell>> GetWorldCells();
        void SetWorldCells(List<List<ICell>> worldCells);
    }
}