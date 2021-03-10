using System.Collections.Generic;
using Interfaces;
using Utils;

namespace TicTacTard
{
    public class TicTacTardAndroid : TicTacTardPlayer
    {
        public TicTacTardAndroid(int id, bool isHuman, string token) : base(id, isHuman, token)
        {
        }
    }
}