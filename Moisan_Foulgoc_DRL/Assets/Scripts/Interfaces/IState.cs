using System.Collections.Generic;

namespace Interfaces
{
    public interface IState
    {
        ICell GetCell();
        List<IState> GetNextStates();
    }
}