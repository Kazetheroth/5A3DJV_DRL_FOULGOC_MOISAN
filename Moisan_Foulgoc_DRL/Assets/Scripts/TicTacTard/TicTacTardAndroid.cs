using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TicTacTard
{
    public class TicTacTardAndroid : TicTacTardPlayer
    {
        public List<TicTacTardStateWithAction> ticTacTardStateWithActions;

        private int nbEpisode = 5;
        
        public TicTacTardAndroid(int id, string token) : base(id, token)
        {
            ticTacTardStateWithActions = new List<TicTacTardStateWithAction>();
            IsHuman = false;
        }

        public override Intent GetPlayerIntent(int currentX, int currentY)
        {
            TicTacTardState state = TicTacTardGame.currentState;

            Intent intent = TicTacTardGame.GetRandomPossibleMove(state);

            int i = 0;
            foreach (TicTacTardStateWithAction action in ticTacTardStateWithActions)
            {
                i++;
                if (action.IsSameState(state))
                {
                    action.DisplayGrid();
                    state.DisplayGrid();
                    Debug.Log("Found with " + i + " iteration " + action.intent);
                }
                
                if (action.IsSameState(state) && TicTacTardGame.CanPlayIntent(state, action.intent))
                {
                    intent = action.intent;
                    break;
                }
            }

            ComputeInitIntent(new TicTacTardStateWithAction(state, intent), true, false);
            
            foreach (TicTacTardStateWithAction action in ticTacTardStateWithActions)
            {
                if (action.IsSameState(state) && TicTacTardGame.CanPlayIntent(state, action.intent))
                {
                    Debug.Log("Use generated state");
                    intent = action.intent;
                    break;
                }
            }

            return intent;
        }

        public void SimulateEpisode(TicTacTardStateWithAction state, bool isFirstVisit, bool offPolicy)
        {
            bool foundSameState = false;
            
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
                    
                    Debug.Log("reward " + episode.EpisodeStates[t + 1].reward + " g " + g + " winScore " + currentState.WinScore);
                    g = g + episode.EpisodeStates[t + 1].reward;
                    bool foundSameStateInEpisode = episode.FoundSameStateUntilIndex(t - 1, currentState);

                    if (!isFirstVisit || !foundSameStateInEpisode)
                    {
                        currentState.WinScore = currentState.WinScore + g;
                        currentState.Visits += 1;

                        Debug.Log("For t = " + t);
                        Debug.Log(currentState.WinScore);
                        Debug.Log(currentState.Visits);
                    }
                }

                if (!offPolicy)
                {
                    foreach (TicTacTardStateWithAction action in ticTacTardStateWithActions)
                    {
                        action.value = action.WinScore / action.Visits;
                    }

                    // foreach (TicTacTardStateWithAction action in ticTacTardStateWithActions)
                    // {
                    //     Intent intent = GetBestIntent(action);
                    //
                    //     if (intent != Intent.Nothing && intent != action.intent)
                    //     {
                    //         action.intent = intent;
                    //     }
                    // }
                }
                
                // foreach (TicTacTardStateWithAction action in ticTacTardStateWithActions)
                // {
                //     Debug.Log("For this case ");
                //     action.DisplayGrid();
                //     Debug.Log("Play : " + action.intent);
                // }
            }
        }

        public void ComputeInitIntent(TicTacTardStateWithAction state, bool isFirstVisit, bool offPolicy)
        {
            bool needToRerun = false;
            
            Debug.Log("Initial state");
            state.DisplayGrid();
            Debug.Log("Random input : " + state.intent);

            SimulateEpisode(state, isFirstVisit, offPolicy);

            if (offPolicy)
            {
                foreach (TicTacTardStateWithAction action in ticTacTardStateWithActions)
                {
                    action.value = action.WinScore / action.Visits;
                }
                
                // for (int i = 0; i < ticTacTardStateWithActions.Count; ++i)
                // {
                //     Intent intent = GetBestIntent(ticTacTardStateWithActions[i]);
                //
                //     if (intent != Intent.Nothing && intent != ticTacTardStateWithActions[i].intent)
                //     {
                //         ticTacTardStateWithActions[i].intent = intent;
                //     }
                // }
            }
        }

        private TicTacEpisode GenerateEpisodeFromState(TicTacTardStateWithAction state)
        {
            TicTacEpisode episode = new TicTacEpisode();

            TicTacTardPlayer fakeOpponent1 = new TicTacTardPlayer(state.player1);
            TicTacTardPlayer fakeOpponent2 = new TicTacTardPlayer(state.player2);
            TicTacTardPlayer currentPlayer = fakeOpponent1;
            string tokenCurrentPlayer = Token;

            TicTacTardStateWithAction currentState = new TicTacTardStateWithAction(state, state.intent);
            currentState.reward = 0;
            currentState.WinScore = 0;
            currentState.Visits = 0;

            int safeLoopIteration = 0;
            
            // state.DisplayGrid();
            // Debug.Log(state.nbActionPlayed);
            // Debug.Log(state.player1.scores[Direction.Column1]);
            // Debug.Log(state.player1.scores[Direction.Column1]);

            while (currentState.nbActionPlayed < 9 && !fakeOpponent1.playerWon && !fakeOpponent2.playerWon && safeLoopIteration < 200)
            {
                ++safeLoopIteration;

                TicTacTardState newState;
                if (tokenCurrentPlayer == Token)
                {
                    newState =
                        TicTacTardGame.PlayAction(currentState, currentPlayer, currentState.intent, false);

                    if (newState == null)
                    {
                        currentState.intent = TicTacTardGame.GetRandomPossibleMove(currentState);
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
                    ticTacTardStateWithActions.Add(currentState);
                }
                else
                {
                    Debug.Log("Existing State");
                    existingState.DisplayGrid();
                    currentState = existingState;
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
                currentState.reward = 1000;
            } else if (fakeOpponent2.playerWon)
            {
                currentState.reward = -1000;
            }
            else
            {
                currentState.reward = 0;
            }
            
            // Debug.Log("Episode generate : ");
            // int i = 1;
            // foreach (TicTacTardStateWithAction action in episode.EpisodeStates)
            // {
            //     Debug.Log("Step "+ i++ + " : ");
            //     action.DisplayGrid();
            // }

            TicTacTardStateWithAction lastAction = episode.EpisodeStates[episode.EpisodeStates.Count - 1];
            lastAction.DisplayGrid();
            Debug.Log(lastAction.reward + " 1 : " + lastAction.player1.playerWon + " 2 : " + lastAction.player2.playerWon);

            return episode;
        }

        private Intent GetBestIntent(TicTacTardStateWithAction action)
        {
            float maxValue = -1;
            Intent intent = Intent.Nothing;

            TicTacTardAndroid bot = new TicTacTardAndroid(10, "0");

            for (int i = 5; i < 14; ++i)
            {
                Intent tempIntent = (Intent) i;

                if (TicTacTardGame.CanPlayIntent(action, tempIntent))
                {
                    TicTacTardState nextState = TicTacTardGame.PlayAction(action, action.player2, tempIntent, false);

                    for (int j = 5; j < 14; ++j)
                    {
                        Intent intentMadeByBot = (Intent) j;

                        if (TicTacTardGame.CanPlayIntent(nextState, intentMadeByBot))
                        {
                            TicTacTardState statePlayedByBot =
                                TicTacTardGame.PlayAction(nextState, action.player1, intentMadeByBot, false);

                            TicTacTardState calculatedState = ticTacTardStateWithActions.Find(state => state.IsSameState(statePlayedByBot));

                            if (calculatedState == null)
                            {
                                TicTacTardStateWithAction nextStateAction =
                                    new TicTacTardStateWithAction(nextState, intentMadeByBot);

                                SimulateEpisode(nextStateAction, true, true);
                            }

                            calculatedState = ticTacTardStateWithActions.Find(state => state.IsSameState(statePlayedByBot));
                            
                            if (calculatedState == null)
                            {
                                Debug.LogError("Nice error ");
                            }

                            if (calculatedState != null && calculatedState.value > maxValue)
                            {
                                maxValue = calculatedState.value;
                                intent = tempIntent;
                            }
                        }
                    }

                    // TicTacTardState nextState = TicTacTardGame.PlayAction(action, action.player1, tempIntent, false);
                    //
                    // TicTacTardState calculatedState = ticTacTardStateWithActions.Find(state => state.IsSameState(nextState));
                    //
                    //
                    // Intent randIntent = TicTacTardGame.GetRandomPossibleMove(nextState);
                    // TicTacTardStateWithAction emptyStateAction = new TicTacTardStateWithAction(nextState, randIntent);
                    //
                    // SimulateEpisode(emptyStateAction, false, true);
                    //
                    // calculatedState = ticTacTardStateWithActions.Find(state => state.IsSameState(nextState));
                    //
                    // if (calculatedState == null)
                    // {
                    //     Debug.LogError("Nice error ");
                    // }
                    //
                    // if (calculatedState != null && calculatedState.value > maxValue)
                    // {
                    //     maxValue = calculatedState.value;
                    //     intent = tempIntent;
                    // }
                }
            }

            return intent;
        }
    }
}