using UnityEngine;

namespace Interfaces
{
    public enum CellType
    {
        Obstacle,
        Goal,
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

        Vector3 GetPosition();
    }
}