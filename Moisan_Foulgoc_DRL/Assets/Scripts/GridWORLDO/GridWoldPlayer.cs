using System.Collections.Generic;
using Interfaces;

namespace GridWORLDO
{
    public class GridWoldPlayer : IPlayer
    {
        //private IPlayer _playerImplementation;

        public ICell CurrentCell { set; get; }
        //public GridWorldState PlayerState { get; set; }
        

        public bool WantToGoTop(List<List<ICell>> worldCells)
        {
            return worldCells[(int) CurrentCell.GetPosition().x].Count - 1 < (int) CurrentCell.GetPosition().y + 1 && worldCells[(int) CurrentCell.GetPosition().x][(int) CurrentCell.GetPosition().y + 1].WhenInteract() != CellType.Obstacle;
        }

        public bool WantToGoBot(List<List<ICell>> worldCells)
        {
            return worldCells[(int) CurrentCell.GetPosition().x].Count - 1 > (int) CurrentCell.GetPosition().y - 1 && worldCells[(int) CurrentCell.GetPosition().x][(int) CurrentCell.GetPosition().y - 1].WhenInteract() != CellType.Obstacle;
        }

        public bool WantToGoLeft(List<List<ICell>> worldCells)
        {
            return worldCells.Count - 1 > (int) CurrentCell.GetPosition().y - 1 && worldCells[(int) CurrentCell.GetPosition().x - 1][(int) CurrentCell.GetPosition().y].WhenInteract() != CellType.Obstacle;
        }

        public bool WantToGoRight(List<List<ICell>> worldCells)
        {
            return worldCells.Count - 1 < (int) CurrentCell.GetPosition().x + 1 && worldCells[(int) CurrentCell.GetPosition().x + 1][(int) CurrentCell.GetPosition().y].WhenInteract() != CellType.Obstacle;
        }
    }
}
