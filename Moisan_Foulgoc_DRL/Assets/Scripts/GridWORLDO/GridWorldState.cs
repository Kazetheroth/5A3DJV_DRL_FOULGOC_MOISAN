using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace GridWORLDO
{
    public class GridWorldState : IGameState
    {
        private Vector3 position;
        private float value;

        public void SetPos(Vector3 pos)
        {
            position = pos;
        }

        public Vector3 GetPos()
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
    }
}