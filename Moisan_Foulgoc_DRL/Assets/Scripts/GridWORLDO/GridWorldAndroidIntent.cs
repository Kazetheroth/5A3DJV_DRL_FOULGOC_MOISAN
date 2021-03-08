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
        
        public GridWorldAndroidIntent(int maxX, int maxY)
        {
            gameStateWithActions = new List<GameStateWithAction>();

            for (int i = 0; i < maxX; ++i)
            {
                for (int j = 0; j < maxY; ++j)
                {
                    GridWorldState gridWorldState = new GridWorldState();
                    gridWorldState.SetPos(new Vector3(i, 0, j));
                    
                    gridWorldState.SetValue(Random.Range(0, 100));

                    List<Intent> intents = new List<Intent>();
                    for (int k = 0; k < 5; ++k)
                    {
                        // Check
                        intents.Add((Intent) Random.Range(1, 5));
                        
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
    }
}