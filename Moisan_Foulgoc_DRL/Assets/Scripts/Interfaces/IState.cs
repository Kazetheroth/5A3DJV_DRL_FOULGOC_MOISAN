using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IGameState
    {
        void SetPos(Vector3 pos);
        Vector3 GetPos();

        float GetValue();
        void SetValue(float value);
    }
}