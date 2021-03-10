using System;
using UnityEngine;

namespace Utils
{
    public class Vector2Int
    {
        public int x { set; get; }
        public int y { set; get; }

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return x + " " + y;
        }

        public static Vector2Int ConvertStringToVector2Int(string coord)
        {
            int x = -1;
            int y = -1;

            string temp;

            for (int i = 0; i < coord.Length; ++i)
            {
                temp = String.Empty;

                if (Char.IsDigit(coord[i]))
                {
                    if (x == -1)
                    {
                        temp += coord[i];
                        x = Int32.Parse(temp);
                    } 
                    else if (y == -1)
                    {
                        temp += coord[i];
                        y = Int32.Parse(temp);
                    }
                }
            }

            if (x == -1 || y == -1)
            {
                Debug.LogError("ConvertStringToVector : Pas de coordonées trouvées");
                return null;
            }

            return new Vector2Int(x, y);
        }
    }
}