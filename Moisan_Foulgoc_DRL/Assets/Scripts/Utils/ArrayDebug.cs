using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Utils
{
    public class ArrayDebug
    {
        public static void PrintArray(List<List<ICell>> cells, int x, int y)
        {
            var test = cells.ToArray();
            Debug.Log(test[1].Count);
            string arrayPrint = "";

            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    Debug.Log(j);
                    arrayPrint += test[i][j].GetReward() + "\t";
                }

                arrayPrint += "\n";
            }

            Debug.Log(arrayPrint);
        }
    }
}