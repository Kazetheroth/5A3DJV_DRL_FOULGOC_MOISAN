using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Utils
{
    public class ArrayDebug
    {
        public static void PrintArray(List<List<ICell>> cells, int x, int y)
        {
            string arrayPrint = "";

            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    Debug.Log(j);
                    arrayPrint += cells[i][j].GetReward() + "\t";
                }

                arrayPrint += "\n";
            }

            Debug.Log(arrayPrint);
        }
    }
}