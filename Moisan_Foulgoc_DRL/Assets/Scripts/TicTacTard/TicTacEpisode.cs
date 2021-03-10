using System.Collections.Generic;
using Interfaces;

namespace TicTacTard
{
    public class TicTacEpisode
    {
        public List<TicTacTardStateWithAction> EpisodeStates = new List<TicTacTardStateWithAction>();

        public bool FoundSameStateUntilIndex(int idx, TicTacTardState state)
        {
            bool foundSameState = false;

            if (idx == -1)
            {
                return false;
            }

            for (int i = 0; i < idx; ++i)
            {
                foundSameState = state.IsSameState(EpisodeStates[i]);

                if (foundSameState)
                {
                    break;
                }
            }

            return foundSameState;
        }
    }
}