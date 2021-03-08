using UnityEngine;

namespace Interfaces
{
    public class IPlayer
    {
        public bool WantToGoTop;
        public bool WantToGoBot;
        public bool WantToGoLeft;
        public bool WantToGoRight;

        public IState PlayerState;
    }
}