using System;
using System.Collections.Generic;
using GridWORLDO;
using Interfaces;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public enum GameType
    {
        GridWORLDO,
        TicTacTard,
        Soooookolat,
    }

    [SerializeField] private GameObject parentGeneratedScene;

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject endGoalPrefab;

    public static GameObject currentPlayerObject;
    private IGame game;

    public void StartGame(GameType gameType)
    {
        switch (gameType)
        {
            case GameType.Soooookolat:
//                game = new SoooookolatGame();
                break;
            case GameType.TicTacTard:
//                game = new TicTacTardGame();
                break;
            case GameType.GridWORLDO:
                game = new GridWORDOGame();
                break;
        }

        game.InitGame();
        GenerateScene();
    }

    private void Update()
    {
        game?.UpdateGame();
    }

    private void GenerateScene()
    {
        List<List<ICell>> cells = game?.GetCells();

        if (cells != null)
        {
            foreach (List<ICell> cellsPerLine in cells)
            {
                foreach (ICell cell in cellsPerLine)
                {
                    GameObject instantiateGo;
    
                    switch (cell.GetCellType())
                    {
                        case CellType.Obstacle:
                            instantiateGo = Instantiate(wallPrefab, parentGeneratedScene.transform);
                            instantiateGo.transform.position = cell.GetPosition();
                            break;
                        case CellType.Player:
                            currentPlayerObject = instantiateGo = Instantiate(playerPrefab, parentGeneratedScene.transform);
                            instantiateGo.transform.position = cell.GetPosition();
                            break;
                        case CellType.EndGoal:
                            Debug.Log("Aloa");
                            instantiateGo = Instantiate(endGoalPrefab, parentGeneratedScene.transform);
                            instantiateGo.transform.position = cell.GetPosition();
                            break;
                    }
                }
            }
        }
    }
}