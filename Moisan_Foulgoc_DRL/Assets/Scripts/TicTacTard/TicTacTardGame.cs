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
            List<ICell> cellsPerLine = new List<ICell>();
            for (int i = 0; i < MAX_CELLS_PER_LINE; i++)
            {
                for (int j = 0; j < MAX_CELLS_PER_COLUMN; j++)
                {
                    Debug.Log($"Grid Exist : {i} - {j} - " + new Vector2Int(i, j).ToString());
                    cellsPerLine.Add(new TicTacTardGrid(new Vector2Int(i, j), CellType.Empty));
                }
                cellsGame.Add(cellsPerLine);
            }

            Debug.Log(cellsGame.Count + " " + cellsPerLine.Count);
            currentPlayer = (TicTacTardPlayer)player[0];
            gameStart = true;
            return true;
        }
        
        public int CheckVictory()
        {
            if (cellsGame[0][0] == cellsGame[0][1] && cellsGame[0][1] == cellsGame[0][2])
                return cellsGame[0][0].GetPlayerId();
            
            else if (cellsGame[1][0] == cellsGame[1][1] && cellsGame[1][1] == cellsGame[1][2])
                return cellsGame[1][0].GetPlayerId();
            
            else if (cellsGame[2][0] == cellsGame[2][1] && cellsGame[2][1] == cellsGame[2][2])
                return cellsGame[2][0].GetPlayerId();

            else if (cellsGame[0][0] == cellsGame[1][0] && cellsGame[1][0] == cellsGame[2][0])
                return cellsGame[0][0].GetPlayerId();
            
            else if (cellsGame[0][1] == cellsGame[1][1] && cellsGame[1][1] == cellsGame[2][1])
                return cellsGame[0][1].GetPlayerId();
            
            else if (cellsGame[0][2] == cellsGame[1][2] && cellsGame[1][2] == cellsGame[2][2])
                return cellsGame[0][2].GetPlayerId();

            else if (cellsGame[0][0] == cellsGame[1][1] && cellsGame[1][1] == cellsGame[2][2])
                return cellsGame[0][0].GetPlayerId();
            
            else if (cellsGame[0][2] == cellsGame[1][1] && cellsGame[1][1] == cellsGame[2][0])
                return cellsGame[0][2].GetPlayerId();
            
            else
                return -1;
        }

        public bool UpdateGame()
        {
            if (!gameStart)
            {
                return false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Debug.Log("Mouse 0 down");
                Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast (ray, out hit, 100.0f))
                {
                    Debug.Log("RayCast : " + hit.collider.name);
                    if(hit.collider.name.Contains("GRID_")){
                        Debug.Log("Hit");
                        for (int i = 0; i < cellsGame.Count; i++)
                        {
                            for (int j = 0; j < cellsGame[i].Count; j++)
                            {
                                if (cellsGame[i][j].GetCellGameObject().name == hit.collider.name)
                                {
                                    //Debug.Log($"Grid Exist : {i} - {j}");
                                    cellsGame[i][j].GetCellGameObject().transform.GetChild(0).GetComponent<TextMeshPro>().text = currentPlayer.Token;
                                    if (CheckVictory() != -1)
                                    {
                                        EndGame();
                                    }
                                    
                                    ChangePlayer();
                                }
                            }
                        }
                    }
                }
                
            }
            
            
            return true;
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
            throw new System.NotImplementedException();
        }
    }
}