using System.Collections.Generic;

namespace Interfaces
{
    public class IState
    {
        public ICell Cell;
        public List<IState> NextStates;
    }
}