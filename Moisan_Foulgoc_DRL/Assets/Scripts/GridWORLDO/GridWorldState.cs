using System.Collections.Generic;
using Interfaces;
using Utils;

namespace GridWORLDO
{
    public class GridWorldState : IGameState
    {
        private Vector2Int position;
        private float value;

        public void SetPos(Vector2Int pos)
        {
            position = pos;
        }

        public Vector2Int GetPos()
        {
            return position;
        }

        public float GetValue()
        {
            return value;
        }

        public void SetValue(float value)
        {
            this.value = value;
        }

        public List<List<ICell>> GetCells()
        {
            throw new System.NotImplementedException();
        }

        public void SetCells(List<List<ICell>> cells)
        {
            throw new System.NotImplementedException();
        }
    }
}