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

        private Intent lastIntent = Intent.Nothing;
        private int nbActions = 0;
        private bool playerWon = false;
        
        public bool InitGame()
        {
            gameStart = false;
            gameType = TicTacTardGameType.BotVBot;
            player = new List<IPlayer>();

            switch (gameType)
            {
                case TicTacTardGameType.HumanVHuman:
                    for (int i = 0; i < 2; i++)
                    {
                        player.Add(new TicTacTardPlayer(i, i.ToString()));
                    }
                    break;
                case TicTacTardGameType.HumanVBot:
                    player.Add(new TicTacTardPlayer(0, "0"));
                    player.Add(new TicTacTardAndroid(1, "1"));
                    break;
                case TicTacTardGameType.BotVBot:
                    for (int i = 0; i < 2; i++)
                    {
                        player.Add(new TicTacTardAndroid(i, i.ToString()));
                    }
                    break;
            }

            cellsGame = new List<List<ICell>>();

            for (int i = 0; i < MAX_CELLS_PER_LINE; i++)
            {
                List<ICell> cellsPerLine = new List<ICell>();

                for (int j = 0; j < MAX_CELLS_PER_COLUMN; j++)
                {
                    cellsPerLine.Add(new TicTacTardCell(new Vector2Int(i, j), CellType.Empty));
                }

                cellsGame.Add(cellsPerLine);
            }

            currentPlayer = (TicTacTardPlayer)player[0];
            return true;
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

                bool changePlayer = PlayAction(lastIntent, currentPlayer.Token, true);

                if ((playerWon = currentPlayer.playerWon) || nbActions == 9)
                {
                    break;
                }

                lastIntent = Intent.Nothing;

                if (changePlayer)
                {
                    ChangePlayer();
                }
            } while (true);

            EndGame();
        }

        // return true if intent is correct
        private bool PlayAction(Intent intent, string token, bool updateAffichage)
        {
            Vector2Int vector2Int = new Vector2Int(0, 0);
            bool endGame = false;
            List<Direction> directions = new List<Direction>();

            switch (intent)
            {
                case Intent.BotCenter:
                    vector2Int.x = 0;
                    vector2Int.y = 1;
                    
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
                    vector2Int.x = 0;
                    vector2Int.y = 2;
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
                    vector2Int.x = 1;
                    vector2Int.y = 0;
                    directions.Add(Direction.Line2);
                    directions.Add(Direction.Column1);
                    break;
                case Intent.MidRight:
                    vector2Int.x = 1;
                    vector2Int.y = 2;
                    directions.Add(Direction.Line2);
                    directions.Add(Direction.Column3);
                    break;
                case Intent.TopCenter:
                    vector2Int.x = 2;
                    vector2Int.y = 1;
                    directions.Add(Direction.Line3);
                    directions.Add(Direction.Column2);
                    break;
                case Intent.TopLeft:
                    vector2Int.x = 2;
                    vector2Int.y = 0;
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

            TicTacTardCell currentCell = cellsGame[vector2Int.x][vector2Int.y] as TicTacTardCell;

            if (currentCell?.token == "-1")
            {
                nbActions++;
                currentCell.token = token;
                currentPlayer.IncrementScore(directions);

                if (updateAffichage)
                {
                    currentCell.GetCellGameObject().transform.GetChild(0).GetComponent<TextMeshPro>().text = token;
                }
            }
            else
            {
                Debug.Log("Token déjà placé");
                return false;
            }

            return true;
        }

        private void ChangePlayer()
        {
            currentPlayer = (TicTacTardPlayer) (currentPlayer.ID == 0 ? player[1] : player[0]);
        }

        public bool EndGame()
        {
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
            return cellsGame;
        }

        public void InitIntent(bool isHuman)
        {
            
        }
    }
}