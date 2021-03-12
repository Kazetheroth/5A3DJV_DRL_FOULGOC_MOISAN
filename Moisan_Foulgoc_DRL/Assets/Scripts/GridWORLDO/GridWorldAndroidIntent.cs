using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2Int = Utils.Vector2Int;

namespace GridWORLDO
{
    public class GameStateWithAction
    {
        public IGameState gameState;
        public Intent intent;

        public Dictionary<Intent, float> intentProbability;

        public IGameState GetNextState(List<IGameState> gameStates, Intent intentTrigger)
        {
            int x = gameState.GetPos().x;
            int y = gameState.GetPos().y;

            switch (intentTrigger)
            {
                case Intent.WantToGoBot:
                    y -= 1;
                    break;
                case Intent.WantToGoLeft:
                    x -= 1;
                    break;
                case Intent.WantToGoRight:
                    x += 1;
                    break;
                case Intent.WantToGoTop:
                    y += 1;
                    break;
            }

            return gameStates.Find(state =>
            {
                float epsilon = 0.00001f;
                Vector2Int statePos = state.GetPos();

                return statePos.x == x && statePos.y == y;
            });
        }
    }

    public class IntentWithValueState
    {
        public float value;
        public Intent intent;
    }
    
    public class GridWorldAndroidIntent : IPlayerIntent
    {
        private List<GameStateWithAction> gameStateWithActions;
        private List<IGameState> gameStates;
        private List<List<ICell>> worldCells;

        private int goalX = 0;
        private int goalY = 0;
        
        public GridWorldAndroidIntent(int maxX, int maxY, int goalX, int goalY, List<List<ICell>> cells)
        {
            gameStateWithActions = new List<GameStateWithAction>();
            gameStates = new List<IGameState>();

            worldCells = cells;
            this.goalX = goalX;
            this.goalY = goalY;
            
            InitIntent(maxX, maxY, goalX, goalY, cells);
        }

        private void ComputePolicy()
        {
            int safeLoopIteration = 0;

            while (!PolicyImprovement() && safeLoopIteration < 5000)
            {
                ++safeLoopIteration;
            }

            if (safeLoopIteration >= 5000)
            {
                Debug.LogError("Safe loop iteration trigger, exit policyEvaluation");
            }
        }

        private void PrintCellsPath()
        {
            foreach (GameStateWithAction gameStateWithAction in gameStateWithActions)
            {
                Controller.InstantiateArrowByIntent(
                    gameStateWithAction.intent, 
                    gameStateWithAction.gameState.GetPos().x, 
                    gameStateWithAction.gameState.GetPos().y,
                    gameStateWithAction.gameState.GetValue());
            }
        }

        public void InitIntent(int maxX, int maxY, int goalX, int goalY, List<List<ICell>> cells)
        {
            IPlayer fakePlayer = new GridWoldPlayer();
            
            for (int i = 0; i < maxX; ++i)
            {
                for (int j = 0; j < maxY; ++j)
                {
                    GridWorldState gridWorldState = new GridWorldState();
                    gridWorldState.SetPos(new Vector2Int(i, j));

                    if (i == goalX && j == goalY)
                    {
                        gridWorldState.SetValue(1000);
                    }
                    else
                    {
                        gridWorldState.SetValue(0);
                    }

                    gameStates.Add(gridWorldState);

                    fakePlayer.SetCell(cells[i][j]);

                    int move;
                    bool canMove = false;
                    do
                    {
                        move = Random.Range(1, 5);
                        switch ((Intent) move)
                        {
                            case Intent.WantToGoTop:
                                canMove = (bool) fakePlayer?.WantToGoTop(cells);
                                break;
                            case Intent.WantToGoBot:
                                canMove = (bool) fakePlayer?.WantToGoBot(cells);
                                break;
                            case Intent.WantToGoLeft:
                                canMove = (bool) fakePlayer?.WantToGoLeft(cells);
                                break;
                            case Intent.WantToGoRight:
                                canMove = (bool) fakePlayer?.WantToGoRight(cells);
                                break;
                        }
                    } while (!canMove);

                    Dictionary<Intent, float> intentByProba = new Dictionary<Intent, float> {{(Intent) move, 1}};

                    gameStateWithActions.Add(new GameStateWithAction
                    {
                        intent = (Intent) move,
                        gameState = gridWorldState,
                        intentProbability = intentByProba
                    });
                }
            }
        }

        private float GetReward(int x, int y)
        {
            float result = GridWORDOGame.MAX_CELLS_PER_LINE * GridWORDOGame.MAX_CELLS_PER_COLUMN - (Vector3.Distance(new Vector3(x, 0, y), new Vector3(goalX, 0, goalY)));
            if (worldCells[x][y].GetCellType() == CellType.Obstacle)
            {
                return result - 1000;
            }
            return result;
        }

        public void PolicyEvaluation()
        {
            float delta;
            float gamma = 0.9f;
            float tetha = 0.1f;

            int safeLoopIteration = 0;
            ICell cell;

            do
            {
                delta = 0;
                ++safeLoopIteration;

                foreach (GameStateWithAction gameStateWithAction in gameStateWithActions)
                {
                    Vector2Int pos = gameStateWithAction.gameState.GetPos();
                    cell = worldCells[pos.x][pos.y];
                    if ((goalX == pos.x && goalY == pos.y) || cell.GetCellType() == CellType.Obstacle)
                    {
                        continue;
                    }

                    float temp = gameStateWithAction.gameState.GetValue();
                    float newValue = 0;

                    IGameState nextGameState = gameStateWithAction.GetNextState(gameStates, gameStateWithAction.intent);
                    float nextReward = worldCells[nextGameState.GetPos().x][nextGameState.GetPos().y].GetReward();
                    newValue += 1 * (nextReward + gamma * nextGameState.GetValue());

                    gameStateWithAction.gameState.SetValue(newValue);
                    delta = Math.Max(delta, Math.Abs(temp - gameStateWithAction.gameState.GetValue()));
                }
            } while (delta >= tetha && safeLoopIteration < 5000);

            if (safeLoopIteration >= 5000)
            {
                Debug.LogError("Safe loop iteration trigger, exit policyEvaluation");
            }
        }

        public bool PolicyImprovement()
        {
            bool policyStable = true;
            IPlayer fakePlayer = new GridWoldPlayer();

            foreach (GameStateWithAction gameStateWithAction in gameStateWithActions)
            {
                Vector2Int currentPos = gameStateWithAction.gameState.GetPos();
                ICell cell = worldCells[currentPos.x][currentPos.y];
                if ((goalX == currentPos.x && goalY == currentPos.y) || cell.GetCellType() == CellType.Obstacle)
                {
                    continue;
                }
                
                fakePlayer.SetCell(worldCells[currentPos.x][currentPos.y]);

                Intent intentToPlay = gameStateWithAction.intent;

                int maxValue = 0;
                Intent intentWithBestValue = GetIntentWithBestValue(gameStateWithAction, fakePlayer).intent;
                gameStateWithAction.intent = intentWithBestValue;

                if (intentToPlay != intentWithBestValue)
                {
                    Debug.Log("State " + gameStateWithAction.gameState.GetPos() + " has " + intentToPlay + " redirect to " + intentWithBestValue);
                    policyStable = false;
                }
            }

            if (!policyStable)
            {
                Debug.Log(">>>>>>>>> Call PolicyEvaluation");
                PolicyEvaluation();
            }

            return policyStable;
        }

        public IntentWithValueState GetIntentWithBestValue(GameStateWithAction gameStateWithAction, IPlayer fakePlayer)
        {
            float maxValue = float.MinValue;
            Intent intentWithBestValue = Intent.Nothing;
            
            for (int i = 1; i < 5; ++i)
            {
                bool canMove = GridWORDOGame.CanMove(fakePlayer, worldCells, (Intent) i);

                if (canMove)
                {
                    IGameState gameState = gameStateWithAction.GetNextState(gameStates, (Intent) i);

                    if (maxValue < gameState.GetValue())
                    {
                        maxValue = gameState.GetValue();
                        intentWithBestValue = (Intent) i;
                    }
                }
            }

            return new IntentWithValueState
            {
                intent = intentWithBestValue,
                value = maxValue
            };
        }

        public void ValueIteration()
        {
            IPlayer fakePlayer = new GridWoldPlayer();

            float tetha = 0.1f;
            float delta;
            float gamma = 0.8f;

            int safeLoopIteration = 0;
            
            do
            {
                ++safeLoopIteration;
                delta = 0;

                foreach (GameStateWithAction gameStateWithAction in gameStateWithActions)
                {
                    Vector2Int currentPos = gameStateWithAction.gameState.GetPos();
                    ICell currentCell = worldCells[currentPos.x][currentPos.y]; 
                    fakePlayer.SetCell(currentCell);

                    if ((goalX == currentPos.x && goalY == currentPos.y) || currentCell.GetCellType() == CellType.Obstacle)
                    {
                        continue;
                    }

                    float temp = gameStateWithAction.gameState.GetValue();

                    float newValue = 0;

                    for (int i = 1; i < 5; ++i)
                    {
                        if (GridWORDOGame.CanMove(fakePlayer, worldCells, (Intent) i))
                        {
                            IGameState nextGameState = gameStateWithAction.GetNextState(gameStates, (Intent) i);
                        
                            float nextReward = worldCells[nextGameState.GetPos().x][nextGameState.GetPos().y].GetReward(); 
                            float tempValue = 1 * nextReward + (gamma * nextGameState.GetValue());

                            if (tempValue > newValue)
                            {
                                newValue = tempValue;
                            }
                        }
                    }

                    gameStateWithAction.gameState.SetValue(newValue);

                    delta = Math.Max(delta, Math.Abs(temp - gameStateWithAction.gameState.GetValue()));
                }
            } while (delta >= tetha && safeLoopIteration < 5000);

            if (safeLoopIteration >= 5000)
            {
                Debug.LogError("Safe loop iteration trigger, exit valueIteration");
                return;
            }

            foreach (GameStateWithAction gameStateWithAction in gameStateWithActions)
            {
                Vector2Int currentPos = gameStateWithAction.gameState.GetPos();
                fakePlayer.SetCell(worldCells[currentPos.x][currentPos.y]);

                gameStateWithAction.intent = GetIntentWithBestValue(gameStateWithAction, fakePlayer).intent;
            }
        }

        private GameStateWithAction GetGameStateByCurrentPosition(int currentX, int currentY)
        {
            return gameStateWithActions.Find(action => action.gameState.GetPos().x == currentX && action.gameState.GetPos().y == currentY);
        }
        
        public Intent GetPlayerIntent(int currentX, int currentY)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                return GetGameStateByCurrentPosition(currentX, currentY).intent;
            }
            
            return Intent.Nothing;
        }

        public List<List<ICell>> GetWorldCells()
        {
            return worldCells;
        }

        public void SetWorldCells(List<List<ICell>> worldCells)
        {
            this.worldCells = worldCells;
        }

        public void InitPlayerIntent()
        {
            if (GridWORDOGame.chosenAlgo == GridWORDOGame.Algo.PolicyImprovement)
            {
                Debug.Log("==================== Compute Policy Improvement ====================");
                ComputePolicy();
            }
            else
            {
                Debug.Log("==================== Compute Value Iteration ====================");
                ValueIteration();
            }

            PrintCellsPath();
        }
    }
}