using Interfaces;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public enum GameType
    {
        GridWORLDO,
        TicTacTard,
        Soooookolat,
    }
    
    private IGame game;

    void StartGame(GameType gameType)
    {
        
    }
}