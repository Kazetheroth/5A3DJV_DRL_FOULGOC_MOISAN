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

    public enum TicTacToeIntent
    {
        TopLeft,
        TopCenter,
        TopRight,
        MidLeft,
        MidCenter,
        MidRight,
        BotLeft,
        BotCenter,
        BotRight
    }
    
    public interface IPlayerIntent
    {
        Intent GetPlayerIntent();
        List<List<ICell>> GetWorldCells();
        void SetWorldCells(List<List<ICell>> worldCells);

        void InitPlayerIntent();
    }
}