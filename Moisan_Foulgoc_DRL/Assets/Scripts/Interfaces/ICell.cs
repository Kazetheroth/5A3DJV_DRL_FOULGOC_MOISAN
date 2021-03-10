using UnityEngine;
using Vector2Int = Utils.Vector2Int;

namespace Interfaces
{
    public enum CellType
    {
        Obstacle,
        Goal,
        Box,
        Empty,
        EndGoal,
        Player
    }

    public interface ICell
    {
        CellType WhenInteract();

        void SetCellType(CellType cellType);
        CellType GetCellType();

        void SetReward(float reward);
        float GetReward();
        Vector2Int GetPosition();

        GameObject GetCellGameObject();
        void SetCellGameObject(GameObject gameObject);
        void AddMaterialToCell(Material mat);
        int GetPlayerId();
    }
}