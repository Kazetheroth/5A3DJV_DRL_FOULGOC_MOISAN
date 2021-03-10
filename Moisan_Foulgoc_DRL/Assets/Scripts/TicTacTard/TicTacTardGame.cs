using System.Collections;
using System.Collections.Generic;
using GridWORLDO;
using Interfaces;
using TMPro;
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
        private TicTacTardGameType gameType;
        private TicTacTardPlayer currentPlayer;

        private List<List<ICell>> cellsGame;
        private const int MAX_CELLS_PER_LINE = 3;
        private const int MAX_CELLS_PER_COLUMN = 3;

        public bool InitGame()
        {
            gameStart = false;
            gameType = TicTacTardGameType.HumanVHuman;
            player = new List<IPlayer>();

            switch (gameType)
            {
                case TicTacTardGameType.HumanVHuman:
                    for (int i = 0; i < 2; i++)
                    {
                        player.Add(new TicTacTardPlayer(i, true, i.ToString()));
                    }
                    break;
                case TicTacTardGameType.HumanVBot:
                    player.Add(new TicTacTardPlayer(0, true, "0"));
                    player.Add(new TicTacTardPlayer(1, false, "1"));
                    break;
                case TicTacTardGameType.BotVBot:
                    for (int i = 0; i < 2; i++)
                    {
                        player.Add(new TicTacTardPlayer(i, false, i.ToString()));
                    }
                    break;
            }

            cellsGame = new List<List<ICell>>();

            for (int i = 0; i < MAX_CELLS_PER_LINE; i++)
            {
                List<ICell> cellsPerLine = new List<ICell>();

                for (int j = 0; j < MAX_CELLS_PER_COLUMN; j++)
                {
                    cellsPerLine.Add(new TicTacTardGrid(new Vector2Int(i, j), CellType.Empty));
                }

                cellsGame.Add(cellsPerLine);
            }

            currentPlayer = (TicTacTardPlayer)player[0];
            return true;
        }

        private Intent lastIntent = Intent.Nothing;

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
            int endToken = -1;

            do
            {
                while (lastIntent == Intent.Nothing)
                {
                    yield return new WaitForSeconds(0.1f);
                }

                if (PlayAction(lastIntent, currentPlayer.Token, true))
                {
                    break;
                }

                lastIntent = Intent.Nothing;
                ChangePlayer();
            } while (endToken == -1);

            EndGame();
        }

        private bool PlayAction(Intent intent, string token, bool updateAffichage)
        {
            Vector2Int vector2Int = new Vector2Int(0, 0);
            bool endGame = false;
            
            switch (intent)
            {
                case Intent.BotCenter:
                    vector2Int.x = 0;
                    vector2Int.y = 1;
                    currentPlayer.IncrementScore(Direction.Line1);
                    currentPlayer.IncrementScore(Direction.Column2);

                    endGame = currentPlayer.scores[Direction.Column2] == 3 ||
                              currentPlayer.scores[Direction.Line1] == 3;
                    break;
                case Intent.BotLeft:
                    vector2Int.x = 0;
                    vector2Int.y = 0;
                    currentPlayer.IncrementScore(Direction.Line1);
                    currentPlayer.IncrementScore(Direction.Column1);
                    currentPlayer.IncrementScore(Direction.Diagonal1);
                    
                    endGame = currentPlayer.scores[Direction.Column1] == 3 ||
                              currentPlayer.scores[Direction.Diagonal1] == 3 ||
                              currentPlayer.scores[Direction.Line1] == 3;
                    break;
                case Intent.BotRight:
                    vector2Int.x = 0;
                    vector2Int.y = 2;
                    currentPlayer.IncrementScore(Direction.Line1);
                    currentPlayer.IncrementScore(Direction.Column3);
                    currentPlayer.IncrementScore(Direction.Diagonal2);
                    
                    endGame = currentPlayer.scores[Direction.Column3] == 3 ||
                              currentPlayer.scores[Direction.Diagonal2] == 3 ||
                              currentPlayer.scores[Direction.Line1] == 3;
                    break;
                case Intent.MidCenter:
                    vector2Int.x = 1;
                    vector2Int.y = 1;
                    currentPlayer.IncrementScore(Direction.Line2);
                    currentPlayer.IncrementScore(Direction.Column2);
                    currentPlayer.IncrementScore(Direction.Diagonal1);
                    currentPlayer.IncrementScore(Direction.Diagonal2);
                    
                    endGame = currentPlayer.scores[Direction.Column2] == 3 ||
                              currentPlayer.scores[Direction.Diagonal1] == 3 ||
                              currentPlayer.scores[Direction.Diagonal2] == 3 ||
                              currentPlayer.scores[Direction.Line2] == 3;
                    break;
                case Intent.MidLeft:
                    vector2Int.x = 1;
                    vector2Int.y = 0;
                    currentPlayer.IncrementScore(Direction.Line2);
                    currentPlayer.IncrementScore(Direction.Column1);
                    
                    endGame = currentPlayer.scores[Direction.Column1] == 3 ||
                              currentPlayer.scores[Direction.Line2] == 3;
                    break;
                case Intent.MidRight:
                    vector2Int.x = 1;
                    vector2Int.y = 2;
                    currentPlayer.IncrementScore(Direction.Line2);
                    currentPlayer.IncrementScore(Direction.Column3);
                    
                    endGame = currentPlayer.scores[Direction.Column3] == 3 ||
                              currentPlayer.scores[Direction.Line2] == 3;
                    break;
                case Intent.TopCenter:
                    vector2Int.x = 2;
                    vector2Int.y = 1;
                    currentPlayer.IncrementScore(Direction.Line3);
                    currentPlayer.IncrementScore(Direction.Column2);
                    
                    endGame = currentPlayer.scores[Direction.Column2] == 3 ||
                              currentPlayer.scores[Direction.Line3] == 3;
                    break;
                case Intent.TopLeft:
                    vector2Int.x = 2;
                    vector2Int.y = 0;
                    currentPlayer.IncrementScore(Direction.Line3);
                    currentPlayer.IncrementScore(Direction.Column1);
                    currentPlayer.IncrementScore(Direction.Diagonal2);
                    
                    endGame = currentPlayer.scores[Direction.Column1] == 3 ||
                              currentPlayer.scores[Direction.Diagonal2] == 3 ||
                              currentPlayer.scores[Direction.Line3] == 3;
                    break;
                case Intent.TopRight:
                    vector2Int.x = 2;
                    vector2Int.y = 2;
                    currentPlayer.IncrementScore(Direction.Line3);
                    currentPlayer.IncrementScore(Direction.Column3);
                    currentPlayer.IncrementScore(Direction.Diagonal1);
                    
                    endGame = currentPlayer.scores[Direction.Column3] == 3 ||
                              currentPlayer.scores[Direction.Diagonal1] == 3 ||
                              currentPlayer.scores[Direction.Line3] == 3;
                    break;
            }

            if (updateAffichage)
            {
                cellsGame[vector2Int.x][vector2Int.y].GetCellGameObject().transform.GetChild(0).GetComponent<TextMeshPro>().text = token;
            }

            return endGame;
        }

        private void ChangePlayer()
        {
            currentPlayer = (TicTacTardPlayer) (currentPlayer.ID == 0 ? player[1] : player[0]);
        }

        public bool EndGame()
        {
            Debug.Log("Someone won");
            return true;
        }

        public IPlayer GetPlayer()
        {
            throw new System.NotImplementedException();
        }

        public List<List<ICell>> GetCells()
        {
            return cellsGame;
        }

        public void InitIntent(bool isHuman)
        {
            
        }
    }
}