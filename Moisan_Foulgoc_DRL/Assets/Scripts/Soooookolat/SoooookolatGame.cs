using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace Soooookolat
{
    public class SoooookolatGame : IGame
    {
        private IPlayer player;
        private IPlayerIntent playerIntent;

        private List<List<ICell>> cells;
        private bool gameStart;
        
        public static int MAX_CELLS_PER_LINE;
        public static int MAX_CELLS_PER_COLUMN;
        
        public bool InitGame()
        {
            gameStart = false;
            player = new SooooookolatPlayer();
            cells = new SoooookolatLevels().InitFirstLevel();
            foreach (var lineCells in cells)
            {
                foreach (var cell in lineCells)
                {
                    if (cell.GetCellType() == CellType.Player)
                    {
                        player.SetCell(cell);
                    }
                }
            }
            return true;
        }

        public IEnumerator StartGame()
        {
            yield return false;
        }

        public bool UpdateGame()
        {
            while (!gameStart)
            {
                return false;
            }
            bool isMoving = CanMove(
                GetPlayer(), 
                GetCells(), 
                playerIntent.GetPlayerIntent(player.GetPosition().x, player.GetPosition().y), 
                true);
            
            if (isMoving && GetPlayer().GetCell().GetCellType() == CellType.EndGoal)
            {
                EndGame();
            }

            Controller.currentPlayerObject.transform.position = new Vector3(player.GetPosition().x, 0, player.GetPosition().y);
            return true;
        }
        
        public static bool CanMove(IPlayer player, List<List<ICell>> worldCells, Intent intent, bool saveNewPlayerCell = false)
        {
            bool canMove = false;

            switch (intent)
            {
                case Intent.WantToGoBot:
                    canMove = player.WantToGoBot(worldCells, saveNewPlayerCell);
                    break;
                case Intent.WantToGoLeft:
                    canMove = player.WantToGoLeft(worldCells, saveNewPlayerCell);
                    break;
                case Intent.WantToGoTop:
                    canMove = player.WantToGoTop(worldCells, saveNewPlayerCell);
                    break;
                case Intent.WantToGoRight:
                    canMove = player.WantToGoRight(worldCells, saveNewPlayerCell);
                    break;
            }

            return canMove;
        }

        public bool EndGame()
        {
            Debug.Log("You won");
            return true;
        }

        public IPlayer GetPlayer()
        {
            return player;
        }

        public List<List<ICell>> GetCells()
        {
            return cells;
        }

        public void InitIntent(bool isHuman)
        {
            if (isHuman)
            {
                playerIntent = new SooooookolatIntent();
            }
            else
            {
                playerIntent = new SooooookolatAndroidInent();
            }

            playerIntent.InitPlayerIntent();

            gameStart = true;
        }
    }
}