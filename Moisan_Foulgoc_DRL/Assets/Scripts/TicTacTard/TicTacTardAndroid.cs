using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TicTacTard
{
    public class TicTacTardAndroid : TicTacTardPlayer
    {
        public TicTacTardAndroid(int id, string token) : base(id, token)
        {
            IsHuman = false;
        }
        
        public override Intent GetPlayerIntent(int currentX, int currentY)
        {
            Debug.Log("FROM ANDROID");
            return (Intent) Random.Range(5, 14);
        }
    }
}