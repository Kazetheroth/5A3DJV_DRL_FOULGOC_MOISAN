using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEditorInternal.Profiling.Memory.Experimental;

namespace Soooookolat
{
    public class SoooookolatGame : IGame
    {
        private IPlayer player;
        private IPlayerIntent playerIntent;

        private List<List<ICell>> cells;
        private bool gameStart;
        
        public static int MAX_CELLS_PER_LINE;
        public static int MAX_CELLS_PER_COLUMN;
        
        public bool InitGame()
        {
            gameStart = false;
            cells = new SoooookolatLevels().InitThirdLevel();
            return true;
        }

        public IEnumerator StartGame()
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateGame()
        {
            while (!gameStart)
            {
                return false;
            }

            return true;
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
            return cells;
        }

        public void InitIntent(bool isHuman)
        {
            throw new System.NotImplementedException();
        }
    }
}