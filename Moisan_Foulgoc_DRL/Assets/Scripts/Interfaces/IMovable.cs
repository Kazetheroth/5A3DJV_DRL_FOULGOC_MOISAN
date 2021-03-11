using Utils;

namespace Interfaces
{
    public interface IMovable
    {
        
        Vector2Int GetPosition();
        void SetCell(ICell cell);
        ICell GetCell();
    }
}