using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace GridWORLDO
{
    public class GameStateWithAction
    {
        public IGameState gameState;
        public List<Intent> intents;
    }
    
    public class GridWorldAndroidIntent : IPlayerIntent
    {
        private List<GameStateWithAction> gameStateWithActions;
        private List<List<ICell>> worldCells;
        private IPlayer player;
        
        public GridWorldAndroidIntent(int maxX, int maxY, List<List<ICell>> cells, IPlayer player)
        {
            gameStateWithActions = new List<GameStateWithAction>();
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

                    List<Intent> intents = new List<Intent>();
                    player.SetCell(cells[i][j]);
                    
                    for (int k = 0; k < 5; ++k)
                    {
                        int move;
                        bool canMove = false;
                        do
                        {
                            move = Random.Range(1, 5);
                            switch ((Intent) move)
                            {
                                case Intent.WantToGoTop:
                                    canMove = (bool) player?.WantToGoTop(cells);
                                    if (canMove)
                                    {
                                        player.SetCell(cells[i][j+1]);
                                    }
                                    break;
                                case Intent.WantToGoBot:
                                    canMove = (bool) player?.WantToGoBot(cells);
                                    if (canMove)
                                    {
                                        player.SetCell(cells[i][j-1]);
                                    }
                                    break;
                                case Intent.WantToGoLeft:
                                    canMove = (bool) player?.WantToGoLeft(cells);
                                    if (canMove)
                                    {
                                        player.SetCell(cells[i-1][j]);
                                    }
                                    break;
                                case Intent.WantToGoRight:
                                    canMove = (bool) player?.WantToGoRight(cells);
                                    if (canMove)
                                    {
                                        player.SetCell(cells[i+1][j]);
                                    }
                                    break;
                            }
                        } while (!canMove);
                        intents.Add((Intent) move);
                        
                    }

                    gameStateWithActions.Add(new GameStateWithAction
                    {
                        intents = intents,
                        gameState = gridWorldState
                    });
                }
            }
        }

        public void EvaluationPolicy()
        {
            float delta = 0;

            foreach (GameStateWithAction gameStateWithAction in gameStateWithActions)
            {
                float temp = gameStateWithAction.gameState.GetValue();
                
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