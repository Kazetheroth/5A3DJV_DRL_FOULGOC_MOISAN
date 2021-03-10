using System.Collections.Generic;

namespace Interfaces
{
    public enum Intent
    {
        // TheWORLDO
        Nothing,
        WantToGoRight,
        WantToGoLeft,
        WantToGoTop,
        WantToGoBot,
        
        // TicTacTard
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
        Intent GetPlayerIntent(int currentX, int currentY);
        List<List<ICell>> GetWorldCells();
        void SetWorldCells(List<List<ICell>> worldCells);

        void InitPlayerIntent();
    }
}