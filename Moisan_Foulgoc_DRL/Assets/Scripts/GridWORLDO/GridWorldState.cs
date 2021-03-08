using System.Collections.Generic;
using Interfaces;

namespace GridWORLDO
{
    public class GridWorldState : IState
    {
        public GridWorldCell StateCell { set; get; }
        public List<IState> NextStates { set; get; }
        public ICell GetCell()
        {
            return StateCell;
        }

        public List<IState> GetNextStates()
        {
            return NextStates;
        }
    }
}