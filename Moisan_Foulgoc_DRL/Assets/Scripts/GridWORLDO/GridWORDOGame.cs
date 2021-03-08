using System.Collections.Generic;
using Interfaces;

namespace GridWORLDO
{
    public class GridWORDOGame : IGame
    {
        private IPlayer player;

        private List<List<ICell>> cells;
        
        public bool InitGame()
        {
            cells = new List<List<ICell>>();

            for (int i = 0; i < 10; ++i)
            {
                List<ICell> cellsPerLine = new List<ICell>();
                
                for (int j = 0; j < 10; ++j)
                {
                    cellsPerLine.Add(new GridWorldCell());
                }
            }

            return true;
        }

        public bool UpdateGame()
        {
            throw new System.NotImplementedException();
        }

        public bool EndGame()
        {
            throw new System.NotImplementedException();
        }
    }
}