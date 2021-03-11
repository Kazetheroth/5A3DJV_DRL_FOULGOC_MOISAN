using Interfaces;
using Utils;

namespace Soooookolat
{
    public class SoooookolatMovable : IMovable
    {
        public ICell CurrentCell { set; get; }

        public Vector2Int GetPosition()
        {
            return CurrentCell.GetPosition();
        }

        public void SetCell(ICell cell)
        {
            CurrentCell = cell;
        }

        public ICell GetCell()
        {
            return CurrentCell;
        }
    }
}