using Utils;

namespace Interfaces
{
    public interface IGameState
    {
        void SetPos(Vector2Int pos);
        Vector2Int GetPos();

        float GetValue();
        void SetValue(float value);
    }
}