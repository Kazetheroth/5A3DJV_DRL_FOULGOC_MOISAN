using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Utils;
using Vector2Int = Utils.Vector2Int;

namespace Soooookolat
{
    public class SoooookolatLevels
    {
        private List<List<List<ICell>>> levelLists;
        private int currentLevel;

        public List<List<ICell>> InitFirstLevel()
        {
            int x = 7;
            int y = 5;

            SoooookolatGame.MAX_CELLS_PER_LINE = x;
            SoooookolatGame.MAX_CELLS_PER_COLUMN = y;
            
            List<List<ICell>> firstLevel = new List<List<ICell>>();
            for (int i = 0; i < x; i++)
            {
                List<ICell> cellsPerLine = new List<ICell>();
                for (int j = 0; j < y; j++)
                {
                    switch (j)
                    {
                        case 1:
                            if (i == 0 || i == 6)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else if (i == 2)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Box),
                                    CellType.Box,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else if(i == 4 || i == 5)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Goal),
                                    CellType.Goal,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 2:
                            if (i == 0 || i == 2 || i > 3)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else if (i == 3)
                            {
                                Debug.Log($"Box : {i} {j}");
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Box),
                                    CellType.Box,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 3:
                            if (i == 0 || i > 3)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else if (i == 1)
                            {
                                Debug.Log("SetPlayer");
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Player),
                                    CellType.Player,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            
                            break;
                        default:
                            cellsPerLine.Add(new SooooookolatCell(
                                GetRewardFromType(CellType.Obstacle),
                                CellType.Obstacle,
                                new Vector2Int(j, i)
                            ));
                            break;
                    }
                }
                firstLevel.Add(cellsPerLine);
            }
            
            ArrayDebug.PrintArray(firstLevel, x, y);
            
            return firstLevel;
        }
        
        public List<List<ICell>> InitSecondLevel()
        {
            int x = 8;
            int y = 12;

            SoooookolatGame.MAX_CELLS_PER_LINE = x;
            SoooookolatGame.MAX_CELLS_PER_COLUMN = y;
            
            List<List<ICell>> secondLevel = new List<List<ICell>>();
            for (int i = 0; i < x; i++)
            {
                List<ICell> cellsPerLine = new List<ICell>();
                for (int j = 0; j < y; j++)
                {
                    switch (j)
                    {
                        case 1:
                            if (i < 3 || i > 6)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else if (i == 6)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Player),
                                    CellType.Player,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else if(i == 4 || i == 5)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Goal),
                                    CellType.Goal,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 2:
                            if (i < 3 || i > 6)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else if(i == 4 || i == 5)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Box),
                                    CellType.Box,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 3:
                            if (i == 4)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 4:
                            if (i == 4)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 5:
                            if (i == 4)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 6:
                            if (i == 4)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 7:
                            if (i > 0 && i <= 4)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 8:
                            if (i == 1 || (i > 2 && i <= 5))
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 9:
                            if (i == 5 || (i > 0 && i < 4))
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 10:
                            if (i >= 3 && i <= 5)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        default:
                            cellsPerLine.Add(new SooooookolatCell(
                                GetRewardFromType(CellType.Obstacle),
                                CellType.Obstacle,
                                new Vector2Int(j, i)
                            ));
                            break;
                    }
                }
                secondLevel.Add(cellsPerLine);
            }
            
            ArrayDebug.PrintArray(secondLevel, x, y);
            
            return secondLevel;
        }
        
        public List<List<ICell>> InitThirdLevel()
        {
            int x = 8;
            int y = 7;

            SoooookolatGame.MAX_CELLS_PER_LINE = x;
            SoooookolatGame.MAX_CELLS_PER_COLUMN = y;
            
            List<List<ICell>> secondLevel = new List<List<ICell>>();
            for (int i = 0; i < x; i++)
            {
                List<ICell> cellsPerLine = new List<ICell>();
                for (int j = 0; j < y; j++)
                {
                    switch (j)
                    {
                        case 1:
                            if (i == 0 || i == 7 || i > 2 && i < 5)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else if (i == 5)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Player),
                                    CellType.Player,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else if(i == 1 || i == 2)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Goal),
                                    CellType.Goal,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 2:
                            if (i < 1 || i > 6)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else if(i == 4 || i == 5)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Box),
                                    CellType.Box,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        
                        case 3:
                            if (i < 1 || i > 6)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else if (i == 3)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Box),
                                    CellType.Box,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else if(i == 1 || i == 2)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Goal),
                                    CellType.Goal,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        case 4:
                            if (i == 4)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Box),
                                    CellType.Box,
                                    new Vector2Int(j, i)
                                ));
                            }
                            if (i < 4 || i > 6)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        
                        case 5:
                            if (i > 3 && i < 6)
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Empty),
                                    CellType.Empty,
                                    new Vector2Int(j, i)
                                ));
                            }
                            else
                            {
                                cellsPerLine.Add(new SooooookolatCell(
                                    GetRewardFromType(CellType.Obstacle),
                                    CellType.Obstacle,
                                    new Vector2Int(j, i)
                                ));
                            }
                            break;
                        default:
                            cellsPerLine.Add(new SooooookolatCell(
                                GetRewardFromType(CellType.Obstacle),
                                CellType.Obstacle,
                                new Vector2Int(j, i)
                            ));
                            break;
                    }
                }
                secondLevel.Add(cellsPerLine);
            }
            
            ArrayDebug.PrintArray(secondLevel, x, y);
            
            return secondLevel;
        }

        private float GetRewardFromType(CellType cellType)
        {
            switch (cellType)
            {
                case CellType.Obstacle:
                    return -1000;
                case CellType.Goal:
                    return 10;
                case CellType.Box:
                    return 5;
                case CellType.Empty:
                    return -1;
                case CellType.Player:
                    return -1000;

            }

            return 0;
        }
    }
}