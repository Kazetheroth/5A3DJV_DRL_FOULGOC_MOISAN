using System.Collections.Generic;
using Interfaces;

namespace Algorithms.MCTS
{
    public class MCTS
    {
        private const int WIN_SCORE = 10;
        private const int MAX_PLAYS = 10000;

        public List<List<ICell>> FindNextMove(List<List<ICell>> grid)
        {
            int plays = 0;
            while (plays < MAX_PLAYS)
            {
                plays++;
            }
            
            throw new System.NotImplementedException();
        }
    }
}