using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GridWORLDO
{
    public class GameStateWithAction
    {
        public IGameState gameState;
        public Intent intent;

        public Dictionary<Intent, int> intentProbability;

        public IGameState GetNextState(List<IGameState> gameStates, Intent intentTrigger)
        {
            Vector3 position = gameState.GetPos();

            switch (intentTrigger)
            {
                case Intent.WantToGoBot:
                    position.z -= 1;
                    break;
                case Intent.WantToGoLeft:
                    position.x -= 1;
                    break;
                case Intent.WantToGoRight:
                    position.x += 1;
                    break;
                case Intent.WantToGoTop:
                    position.z += 1;
                    break;
            }

            return gameStates.Find(state =>
            {
                float epsilon = 0.00001f;
                Vector3 statePos = state.GetPos();

                return Math.Abs(statePos.x - position.x) < epsilon && Math.Abs(statePos.y - position.y) < epsilon;
            });
        }
    }

    public class GridWorldAndroidIntent : IPlayerIntent
    {
        private List<GameStateWithAction> gameStateWithActions;
        private List<IGameState> gameStates;
        private List<List<ICell>> worldCells;
        private IPlayer player;
        
        public GridWorldAndroidIntent(int maxX, int maxY, List<List<ICell>> cells, IPlayer player)
        {
            gameStateWithActions = new List<GameStateWithAction>();
            gameStates = new List<IGameState>();

            worldCells = cells;
            player = this.player;
            
            InitIntent(maxX, maxY, cells, player);
        }

        public void InitIntent(int maxX, int maxY, List<List<ICell>> cells, IPlayer player)
        {
            for (int i = 0; i < maxX; ++i)
            {
                for (int j = 0; j < maxY; ++j)
                {
                    GridWorldState gridWorldState = new GridWorldState();
                    gridWorldState.SetPos(new Vector3(i, 0, j));

                    gridWorldState.SetValue(Random.Range(0, 100));
                    gameStates.Add(gridWorldState);

                    player.SetCell(cells[i][j]);

                    int move;
                    bool canMove = false;
                    do
                    {
                        move = Random.Range(1, 5);
                        switch ((Intent) move)
                        {
                            case Intent.WantToGoTop:
                                canMove = (bool) player?.WantToGoTop(cells);
                                break;
                            case Intent.WantToGoBot:
                                canMove = (bool) player?.WantToGoBot(cells);
                                break;
                            case Intent.WantToGoLeft:
                                canMove = (bool) player?.WantToGoLeft(cells);
                                break;
                            case Intent.WantToGoRight:
                                canMove = (bool) player?.WantToGoRight(cells);
                                break;
                        }
                    } while (!canMove);

                    Dictionary<Intent, int> intentByProba = new Dictionary<Intent, int>();
                    intentByProba.Add((Intent) move, 100);

                    gameStateWithActions.Add(new GameStateWithAction
                    {
                        intent = (Intent) move,
                        gameState = gridWorldState,
                        intentProbability = intentByProba
                    });
                }
            }
        }

        public void PolicyEvaluation()
        {
            float delta = 0;
            float gamma = 0.8f;
            float tetha = 0.1f;

            while (delta < tetha)
            {
                foreach (GameStateWithAction gameStateWithAction in gameStateWithActions)
                {
                    float temp = gameStateWithAction.gameState.GetValue();

                    float newValue = 0;
                    foreach (KeyValuePair<Intent, int> intentProb in gameStateWithAction.intentProbability)
                    {
                        IGameState nextGameState = gameStateWithAction.GetNextState(gameStates, intentProb.Key);
                        
                        float nextReward = worldCells[(int) nextGameState.GetPos().x][(int) nextGameState.GetPos().y].GetReward(); 
                        newValue = intentProb.Value * nextReward * (gamma * nextGameState.GetValue());
                    }

                    gameStateWithAction.gameState.SetValue(newValue);

                    delta = Math.Max(delta, temp - gameStateWithAction.gameState.GetValue());
                }
            }
        }

        public void PolicyImprovement()
        {
            bool policyStable = true;
            IPlayer fakePlayer = new GridWoldPlayer();

            foreach (GameStateWithAction gameStateWithAction in gameStateWithActions)
            {
                Vector3 currentPos = gameStateWithAction.gameState.GetPos();
                fakePlayer.SetCell(worldCells[(int) currentPos.x][(int) currentPos.z]);
                
                Intent intentToPlay = gameStateWithAction.intent;

                int maxValue = 0;
                Intent maxValueForIntent;
                GameStateWithAction testingGameState;
                
                for (int i = 1; i < 5; ++i)
                {
                    if (fakePlayer.WantToGoBot(worldCells))
                    {
                        testingGameState = gameStateWithAction.GetNextState(gameStates, )
                    }
                }
            }
        }
        
        public Intent GetPlayerIntent()
        {
            return Intent.Nothing;
        }

        public IPlayer GetPlayer()
        {
            return player;
        }

        public void SetPlayer(IPlayer player)
        {
            this.player = player;
        }

        public List<List<ICell>> GetWorldCells()
        {
            return worldCells;
        }

        public void SetWorldCells(List<List<ICell>> worldCells)
        {
            this.worldCells = worldCells;
        }
    }
}