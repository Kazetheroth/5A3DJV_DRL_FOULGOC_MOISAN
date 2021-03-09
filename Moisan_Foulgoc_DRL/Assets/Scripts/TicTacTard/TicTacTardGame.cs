using System.Collections.Generic;
using GridWORLDO;
using Interfaces;

namespace TicTacTard
{
    public enum TicTacTardGameType
    {
        HumanVHuman,
        HumanVBot,
        BotVBot
    }
    public class TicTacTardGame : IGame
    {
        private List<IPlayer> player;
        private IPlayerIntent playerIntent;
        private TicTacTardGameType gameType;
        
        private List<List<ICell>> cellsGame;
        private const int MAX_CELLS_PER_LINE = 3;
        private const int MAX_CELLS_PER_COLUMN = 3;
        
        public bool InitGame(bool isHuman)
        {
            gameType = TicTacTardGameType.HumanVHuman;
            switch (gameType)
            {
                case TicTacTardGameType.HumanVHuman:
                    break;
                case TicTacTardGameType.HumanVBot:
                    break;
                case TicTacTardGameType.BotVBot:
                    break;
            }
            
            cellsGame = new List<List<ICell>>();
            List<ICell> cellsPerLine = new List<ICell>();
            for (int i = 0; i < MAX_CELLS_PER_LINE; i++)
            {
                for (int j = 0; j < MAX_CELLS_PER_COLUMN; j++)
                {
                    cellsPerLine.Add(new TicTacTardGrid());
                }
            }
            
            return true;
        }
        
        public bool CheckVictory()
        {
            return false;
        }

        public bool InitGame()
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateGame()
        {
            throw new System.NotImplementedException();
        }

        public bool EndGame()
        {
            throw new System.NotImplementedException();
        }

        public IPlayer GetPlayer()
        {
            throw new System.NotImplementedException();
        }

        public List<List<ICell>> GetCells()
        {
            throw new System.NotImplementedException();
        }

        public void InitIntent(bool isHuman)
        {
            throw new System.NotImplementedException();
        }
    }
//    public class TicTacTardGame : IGame
//    {
//        public bool GenerateScene()
//        {
//            throw new System.NotImplementedException();
//        }
//
//        
//
//        public bool UpdateGame()
//        {
//            throw new System.NotImplementedException();
//        }
//
//        public bool EndGame()
//        {
//            throw new System.NotImplementedException();
//        }
//    }
}