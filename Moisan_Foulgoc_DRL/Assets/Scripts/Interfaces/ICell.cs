namespace Interfaces
{
    public enum CellType
    {
        Obstacle,
        Goal,
        EndGoal
    }

    public interface ICell
    {
        
        CellType WhenInteract();
    }
}