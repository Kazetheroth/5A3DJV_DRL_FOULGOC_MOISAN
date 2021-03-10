using System.Collections.Generic;
using Interfaces;
using UnityEditor;
using UnityEngine;
using Vector2Int = Utils.Vector2Int;

namespace TicTacTard
{
    public enum Direction
    {
        Line1,
        Line2,
        Line3,
        Column1,
        Column2,
        Column3,
        Diagonal1,
        Diagonal2
    }
    
    public class TicTacTardPlayer : IPlayer, IPlayerIntent
    {
        private int id;
        private bool isHuman;
        private string token;

        public Dictionary<Direction, int> scores;

        public bool playerWon;

        public TicTacTardPlayer(int id, string token)
        {
            this.id = id;
            this.token = token;

            isHuman = true;
            playerWon = false;
            scores = new Dictionary<Direction, int>();

            for (int i = 0; i < 8; ++i)
            {
                scores.Add((Direction) i, 0);
            }
        }

        public void IncrementScore(List<Direction> directions)
        {
            foreach (Direction dir in directions)
            {
                scores[dir] += 1;

                if (scores[dir] == 3)
                {
                    playerWon = true;
                }
            }
        }

        public int ID
        {
            get => id;
            set => id = value;
        }

        public bool IsHuman
        {
            get => isHuman;
            set => isHuman = value;
        }

        public string Token
        {
            get => token;
            set => token = value;
        }

        public bool WantToGoTop(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            throw new System.NotImplementedException();
        }

        public bool WantToGoBot(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            throw new System.NotImplementedException();
        }

        public bool WantToGoLeft(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            throw new System.NotImplementedException();
        }

        public bool WantToGoRight(List<List<ICell>> worldCells, bool setNewCell = false)
        {
            throw new System.NotImplementedException();
        }

        public Vector2Int GetPosition()
        {
            throw new System.NotImplementedException();
        }

        public void SetCell(ICell cell)
        {
            throw new System.NotImplementedException();
        }

        public ICell GetCell()
        {
            throw new System.NotImplementedException();
        }

        public virtual Intent GetPlayerIntent(int currentX, int currentY)
        {
            Intent intentToPlay = Intent.Nothing;
            
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Click");
                RaycastHit hit;

                Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.collider.name.Contains("GRID_"))
                    {
                        Vector2Int vector2Int = Vector2Int.ConvertStringToVector2Int(hit.collider.name);

                        intentToPlay = ConvertVector2IntToIntent(vector2Int);
                    }
                }
            }

            return intentToPlay;
        }

        private Intent ConvertVector2IntToIntent(Vector2Int vector2Int)
        {
            Intent intent = Intent.Nothing;
            
            if (vector2Int.x == 0 && vector2Int.y == 0)
            {
                intent = Intent.BotLeft;
            } else if (vector2Int.x == 0 && vector2Int.y == 1)
            {
                intent = Intent.BotCenter;
            } else if (vector2Int.x == 0 && vector2Int.y == 2)
            {
                intent = Intent.BotRight;
            } else if (vector2Int.x == 1 && vector2Int.y == 0)
            {
                intent = Intent.MidLeft;
            } else if (vector2Int.x == 1 && vector2Int.y == 1)
            {
                intent = Intent.MidCenter;
            } else if (vector2Int.x == 1 && vector2Int.y == 2)
            {
                intent = Intent.MidRight;
            } else if (vector2Int.x == 2 && vector2Int.y == 0)
            {
                intent = Intent.TopLeft;
            } else if (vector2Int.x == 2 && vector2Int.y == 1)
            {
                intent = Intent.TopCenter;
            } else if (vector2Int.x == 2 && vector2Int.y == 2) {
                intent = Intent.TopRight;
            }

            return intent;
        }

        public List<List<ICell>> GetWorldCells()
        {
            throw new System.NotImplementedException();
        }

        public void SetWorldCells(List<List<ICell>> worldCells)
        {
            throw new System.NotImplementedException();
        }

        public void InitPlayerIntent()
        {
            throw new System.NotImplementedException();
        }
    }
}