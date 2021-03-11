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

            for (int i = y - 1; i >= 0; --i)
            {
                for (int j = 0; j < x; ++j)
                {
                    //Debug.Log(j);
                    arrayPrint += cells[j][i].GetReward() + "\t";
                }

                arrayPrint += "\n";
            }

            Debug.Log(arrayPrint);
        }
    }
}