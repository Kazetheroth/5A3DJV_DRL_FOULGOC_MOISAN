﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IGame
    {
        bool InitGame();
        IEnumerator StartGame();
        bool UpdateGame();
        bool EndGame();

        IPlayer GetPlayer();
        List<List<ICell>> GetCells();
        void InitIntent(bool isHuman);

        bool IsInit();
    }
}