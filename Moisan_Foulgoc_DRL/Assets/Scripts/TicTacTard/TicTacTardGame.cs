using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Vector2Int = Utils.Vector2Int;

namespace TicTacTard
{
    public enum TicTacTardGameType
    {
        HumanVHuman,
        HumanVBot,
        BotVBot
    }
    public class TicTacTardGame : IGame
    {
        private static bool gameStart = false;
        private List<IPlayer> player;
        private IPlayerIntent playerIntent;
        public static TicTacTardGameType gameType = TicTacTardGameType.HumanVHuman;
        private TicTacTardPlayer currentPlayer;

        // private List<List<ICell>> cellsGame;
        private const int MAX_CELLS_PER_LINE = 3;
        private const int MAX_CELLS_PER_COLUMN = 3;

        private Intent lastIntent = Intent.Nothing;
        private bool playerWon = false;

        public bool offPolicy;
        public bool firstVisit;

        public static TicTacTardState currentState;

        private bool isInit = false; 

        public bool InitGame()
        {
            if (player == null)
            {
                player = new List<IPlayer>();
            }

            gameStart = false;

            List<List<ICell>> cellsGame = new List<List<ICell>>();

            for (int i = 0; i < MAX_CELLS_PER_LINE; i++)
            {
                List<ICell> cellsPerLine = new List<ICell>();

                for (int j = 0; j < MAX_CELLS_PER_COLUMN; j++)
                {
                    cellsPerLine.Add(new TicTacTardCell(new Vector2Int(i, j), CellType.Empty));
                }

                cellsGame.Add(cellsPerLine);
            }

            currentState = new TicTacTardState();
            currentState.SetCells(cellsGame);
            isInit = true;
            return true;
        }

        public bool IsInit()
        {
            return isInit;
        }

        public bool UpdateGame()
        {
            if (!gameStart)
            {
                return false;
            }

            if (lastIntent == Intent.Nothing)
            {
                lastIntent = currentPlayer.GetPlayerIntent(0, 0);
            }

            return true;
        }

        public IEnumerator StartGame()
        {
            gameStart = true;

            do
            {
                while (lastIntent == Intent.Nothing)
                {
                    yield return new WaitForSeconds(0.1f);
                }

                TicTacTardState newState =
                    PlayAction(currentState, currentPlayer, lastIntent, true);

                lastIntent = Intent.Nothing;

                if (newState != null)
                {
                    currentState = newState;
                    Debug.Log(newState.nbActionPlayed + " " + (player[0] as TicTacTardPlayer).playerWon + " " + (player[1] as TicTacTardPlayer).playerWon);
 
                    if ((playerWon = currentPlayer.playerWon) || currentState.nbActionPlayed == 9)
                    {
                        break;
                    }

                    ChangePlayer();
                }
            } while (true);

            EndGame();
        }

        public static bool CanPlayIntent(TicTacTardState state, Intent intent)
        {
            int x = 0;
            int y = 0;
            
            switch (intent)
            {
                case Intent.BotCenter:
                    x = 1;
                    y = 0;
                    break;
                case Intent.BotLeft:
                    x = 0;
                    y = 0;
                    break;
                case Intent.BotRight:
                    x = 2;
                    y = 0;
                    break;
                case Intent.MidCenter:
                    x = 1;
                    y = 1;
                    break;
                case Intent.MidLeft:
                    x = 0;
                    y = 1;
                    break;
                case Intent.MidRight:
                    x = 2;
                    y = 1;
                    break;
                case Intent.TopCenter:
                    x = 1;
                    y = 2;
                    break;
                case Intent.TopLeft:
                    x = 0;
                    y = 2;
                    break;
                case Intent.TopRight:
                    x = 2;
                    y = 2;
                    break;
            }

            TicTacTardCell currentCell = state.Grid[x][y] as TicTacTardCell;
            if (currentCell?.token == "-1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public static Intent GetRandomPossibleMove(TicTacTardState state)
        {
            bool foundPossibleMove = false;
            Intent intent = Intent.Nothing;
            int x = 0;
            int y = 0;
            int safeLoopIteration = 0;

            List<Intent> testedIntent = new List<Intent>();

            while (!foundPossibleMove && safeLoopIteration < 50 && state.nbActionPlayed != 9)
            {
                ++safeLoopIteration;
                intent = (Intent) Random.Range(5, 14);

                if (testedIntent.Contains(intent))
                {
                    continue;
                }
                else
                {
                    testedIntent.Add(intent);
                }

                if (CanPlayIntent(state, intent))
                {
                    break;
                }

                if (safeLoopIteration >= 50)
                {
                    state.DisplayGrid();
                    Debug.LogError("SafeLoopIteration triggered : Exit Get random intent ");
                }

                if (testedIntent.Count == 9)
                {
                    break;
                }
            }

            return intent;
        }

        // return true if intent is correct
        public static TicTacTardState PlayAction(TicTacTardState state, TicTacTardPlayer player, Intent intent, bool updateDisplay)
        {
            Vector2Int vector2Int = new Vector2Int(0, 0);
            bool endGame = false;
            List<Direction> directions = new List<Direction>();

            switch (intent)
            {
                case Intent.BotCenter:
                    vector2Int.x = 1;
                    vector2Int.y = 0;
                    
                    directions.Add(Direction.Line1);
                    directions.Add(Direction.Column2);
                    break;
                case Intent.BotLeft:
                    vector2Int.x = 0;
                    vector2Int.y = 0;
                    directions.Add(Direction.Line1);
                    directions.Add(Direction.Column1);
                    directions.Add(Direction.Diagonal1);
                    break;
                case Intent.BotRight:
                    vector2Int.x = 2;
                    vector2Int.y = 0;
                    directions.Add(Direction.Line1);
                    directions.Add(Direction.Column3);
                    directions.Add(Direction.Diagonal2);
                    break;
                case Intent.MidCenter:
                    vector2Int.x = 1;
                    vector2Int.y = 1;
                    directions.Add(Direction.Line2);
                    directions.Add(Direction.Column2);
                    directions.Add(Direction.Diagonal1);
                    directions.Add(Direction.Diagonal2);
                    break;
                case Intent.MidLeft:
                    vector2Int.x = 0;
                    vector2Int.y = 1;
                    directions.Add(Direction.Line2);
                    directions.Add(Direction.Column1);
                    break;
                case Intent.MidRight:
                    vector2Int.x = 2;
                    vector2Int.y = 1;
                    directions.Add(Direction.Line2);
                    directions.Add(Direction.Column3);
                    break;
                case Intent.TopCenter:
                    vector2Int.x = 1;
                    vector2Int.y = 2;
                    directions.Add(Direction.Line3);
                    directions.Add(Direction.Column2);
                    break;
                case Intent.TopLeft:
                    vector2Int.x = 0;
                    vector2Int.y = 2;
                    directions.Add(Direction.Line3);
                    directions.Add(Direction.Column1);
                    directions.Add(Direction.Diagonal2);
                    break;
                case Intent.TopRight:
                    vector2Int.x = 2;
                    vector2Int.y = 2;
                    directions.Add(Direction.Line3);
                    directions.Add(Direction.Column3);
                    directions.Add(Direction.Diagonal1);
                    break;
            }

            TicTacTardCell currentCell = state.Grid[vector2Int.x][vector2Int.y] as TicTacTardCell;

            if (currentCell?.token == "-1")
            {
                TicTacTardState newState = new TicTacTardState(state, vector2Int, player.Token, updateDisplay);
                player.IncrementScore(directions);

                return newState;
            }
            else
            {
                if (updateDisplay)
                {
                    Debug.Log("Token déjà placé");
                }
            }

            return null;
        }

        private void ChangePlayer()
        {
            currentPlayer = (TicTacTardPlayer) (currentPlayer.ID == 0 ? player[1] : player[0]);
        }

        public bool EndGame()
        {
            gameStart = false;

            if (playerWon)
            {
                Debug.Log("Player with " + currentPlayer.Token + " won ");
            }
            else
            {
                Debug.Log("Draw");
            }

            return true;
        }

        public IPlayer GetPlayer()
        {
            throw new System.NotImplementedException();
        }

        public List<List<ICell>> GetCells()
        {
            return currentState.GetCells();
        }

        public void InitIntent(bool isHuman)
        {
            isInit = false;
            
            if (player.Count == 0)
            {
                switch (gameType)
                {
                    case TicTacTardGameType.HumanVHuman:
                        for (int i = 0; i < 2; i++)
                        {
                            player.Add(new TicTacTardPlayer(i, i.ToString()));
                        }
                        break;
                    case TicTacTardGameType.HumanVBot:
                        player.Add(new TicTacTardAndroid(0, "0"));
                        player.Add(new TicTacTardPlayer(1, "1"));

                        TicTacTardStateWithAction currentStateWithAction =
                            new TicTacTardStateWithAction(currentState, GetRandomPossibleMove(currentState));

                        int safeLoopIteration = 0;
                        bool policyIsStable = false;

                        while (!policyIsStable && safeLoopIteration < 100)
                        {
                            ++safeLoopIteration;
                            policyIsStable = (player[0] as TicTacTardAndroid).ComputeInitIntent(currentStateWithAction, true, true);
                        }

                        if (safeLoopIteration >= 100)
                        {
                            Debug.LogError("safeLoopIteration trigger : ExitComputeIntent");
                        }
                    
                        Debug.Log("Bot generate " + (player[0] as TicTacTardAndroid).ticTacTardStateWithActions.Count + " states");
                        break;
                    case TicTacTardGameType.BotVBot:
                        for (int i = 0; i < 2; i++)
                        {
                            player.Add(new TicTacTardAndroid(i, i.ToString()));
                        }
                        break;
                }
            }

            ((TicTacTardPlayer) player[0]).ResetScore();
            ((TicTacTardPlayer) player[1]).ResetScore();
            currentPlayer = (TicTacTardPlayer)player[0];
        }
    }
}