using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TicTacTard
{
    public class TicTacTardAndroid : TicTacTardPlayer
    {
        public List<TicTacTardStateWithAction> ticTacTardStateWithActions;

        private int nbEpisode = 20;
        
        public TicTacTardAndroid(int id, string token) : base(id, token)
        {
            ticTacTardStateWithActions = new List<TicTacTardStateWithAction>();
            IsHuman = false;
        }

        public override Intent GetPlayerIntent(int currentX, int currentY)
        {
            TicTacTardState state = TicTacTardGame.currentState;

            Intent intent = TicTacTardGame.GetRandomPossibleMove(state);

            foreach (TicTacTardStateWithAction action in ticTacTardStateWithActions)
            {
                if (action.IsSameState(state) && TicTacTardGame.CanPlayIntent(state, action.intent))
                {
                    Debug.Log("Found intent !");
                    intent = action.intent;
                    break;
                }
            }

            return intent;
        }

        public bool SimulateEpisode(TicTacTardStateWithAction state, bool isFirstVisit, bool offPolicy)
        {
            bool foundSameState = false;
            bool isStable = true;

            foreach (TicTacTardStateWithAction action in ticTacTardStateWithActions)
            {
                action.WinScore = 0;
                action.Visits = 0;

                if (action.IsSameState(state))
                {
                    foundSameState = true;
                }
            }
            
            if (!foundSameState) {
                ticTacTardStateWithActions.Add(state);
            }
            
            for (int i = 0; i < nbEpisode; ++i)
            {
                TicTacEpisode episode = GenerateEpisodeFromState(state);
                float g = 0;

                for (int t = episode.EpisodeStates.Count - 2; t >= 0; --t)
                {
                    TicTacTardStateWithAction currentState = episode.EpisodeStates[t];
                    
                    // Debug.Log("reward " + episode.EpisodeStates[t + 1].reward + " g " + g + " winScore " + currentState.WinScore);
                    g = g + episode.EpisodeStates[t + 1].reward;
                    bool foundSameStateInEpisode = episode.FoundSameStateUntilIndex(t - 1, currentState);

                    if (!isFirstVisit || !foundSameStateInEpisode)
                    {
                        currentState.WinScore = currentState.WinScore + g;
                        currentState.Visits += 1;
                    }
                }

                if (!offPolicy)
                {
                    foreach (TicTacTardStateWithAction action in ticTacTardStateWithActions)
                    {
                        action.value = action.WinScore / action.Visits;
                    }

                    foreach (TicTacTardStateWithAction action in ticTacTardStateWithActions)
                    {
                        Intent intent = GetBestIntent(action);
                    
                        if (intent != Intent.Nothing && intent != action.intent)
                        {
                            action.intent = intent;
                            isStable = false;
                        }
                    }
                }
            }
            
            return isStable;
        }

        public bool ComputeInitIntent(TicTacTardStateWithAction state, bool isFirstVisit, bool offPolicy)
        {
            bool isStable = SimulateEpisode(state, isFirstVisit, offPolicy);

            if (offPolicy)
            {
                foreach (TicTacTardStateWithAction action in ticTacTardStateWithActions)
                {
                    action.value = action.WinScore / action.Visits;
                }
                
                for (int i = 0; i < ticTacTardStateWithActions.Count; ++i)
                {
                    Intent intent = GetBestIntent(ticTacTardStateWithActions[i]);
                
                    if (intent != Intent.Nothing && intent != ticTacTardStateWithActions[i].intent)
                    {
                        ticTacTardStateWithActions[i].intent = intent;
                        isStable = false;
                    }
                }
            }

            return isStable;
        }

        private Intent EpsilonGreedy(TicTacTardStateWithAction state)
        {
            int rand = Random.Range(0, 10);
            Intent intent;

            if (state.nbActionPlayed == 9)
            {
                return state.intent;
            }

            if (rand < 5)
            {
                intent = state.intent;
            }
            else
            {
                do
                {
                    intent = TicTacTardGame.GetRandomPossibleMove(state);
                } while (intent == Intent.Nothing);
            }

            return intent;
        }

        private TicTacEpisode GenerateEpisodeFromState(TicTacTardStateWithAction state)
        {
            TicTacEpisode episode = new TicTacEpisode();

            Intent initialIntent = EpsilonGreedy(state);

            TicTacTardPlayer fakeOpponent1 = new TicTacTardPlayer(0, "0");
            TicTacTardPlayer fakeOpponent2 = new TicTacTardPlayer(1, "1");
            TicTacTardPlayer currentPlayer = fakeOpponent1;
            string tokenCurrentPlayer = Token;

            TicTacTardStateWithAction currentState = new TicTacTardStateWithAction(state, state.intent);
            currentState.reward = 0;
            currentState.WinScore = 0;
            currentState.Visits = 0;

            int safeLoopIteration = 0;

            while (currentState.nbActionPlayed < 9 && !fakeOpponent1.playerWon && !fakeOpponent2.playerWon && safeLoopIteration < 200)
            {
                ++safeLoopIteration;

                TicTacTardState newState;
                if (tokenCurrentPlayer == Token)
                {
                    newState =
                        TicTacTardGame.PlayAction(currentState, currentPlayer, initialIntent, false);

                    if (newState == null)
                    {
                        initialIntent = TicTacTardGame.GetRandomPossibleMove(currentState);
                        continue;
                    }
                }
                else
                {
                    newState =
                        TicTacTardGame.PlayAction(currentState, currentPlayer, TicTacTardGame.GetRandomPossibleMove(currentState), false);

                    if (newState == null)
                    {
                        continue;
                    }
                }
                
                TicTacTardStateWithAction existingState = ticTacTardStateWithActions.Find(stateSaved => newState.IsSameState(stateSaved));

                if (existingState == null)
                {
                    TicTacTardStateWithAction initNewState =
                        new TicTacTardStateWithAction(newState, TicTacTardGame.GetRandomPossibleMove(newState));

                    initNewState.prevState = currentState;
                    currentState = initNewState;
                    initialIntent = currentState.intent;
                    ticTacTardStateWithActions.Add(currentState);
                }
                else
                {
                    currentState = existingState;
                    initialIntent = currentState.intent;
                }

                episode.EpisodeStates.Add(currentState);

                currentPlayer = tokenCurrentPlayer == fakeOpponent1.Token ? fakeOpponent2 : fakeOpponent1;
                tokenCurrentPlayer = currentPlayer.Token;
            }

            if (safeLoopIteration >= 200)
            {
                Debug.LogError("Safe loopIteration trigger : exit generate episode");
            }

            if (fakeOpponent1.playerWon)
            {
                currentState.reward = 1;
            }
            else
            {
                currentState.reward = 0;
            }

            return episode;
        }

        private Intent GetBestIntent(TicTacTardStateWithAction action)
        {
            float maxValue = -1;
            Intent intent = action.intent;

            TicTacTardPlayer fakePlayer = new TicTacTardPlayer(2, "1");

            for (int i = 5; i < 14; ++i)
            {
                Intent tempIntent = (Intent) i;

                if (TicTacTardGame.CanPlayIntent(action, tempIntent))
                {
                    TicTacTardState nextState = TicTacTardGame.PlayAction(action, fakePlayer, tempIntent, false);

                    TicTacTardState calculatedState = ticTacTardStateWithActions.Find(state => state.IsSameState(nextState));

                    if (calculatedState != null && calculatedState.value > maxValue)
                    {
                        maxValue = calculatedState.value;
                        intent = tempIntent;
                    }
                }
            }

            return intent;
        }
    }
}