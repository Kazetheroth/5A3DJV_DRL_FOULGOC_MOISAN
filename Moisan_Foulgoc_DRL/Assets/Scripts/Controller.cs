using System;
using System.Collections.Generic;
using GridWORLDO;
using Interfaces;
using Soooookolat;
using TicTacTard;
using TMPro;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public enum GameType
    {
        Nothing,
        GridWORLDO,
        TicTacTard,
        Soooookolat,
    }

    [SerializeField] public GameObject parentGeneratedScene;
    [SerializeField] public GameObject wallPrefab;
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] public GameObject boxPrefab;
    [SerializeField] public GameObject goalPrefab;
    [SerializeField] public GameObject endGoalPrefab;
    [SerializeField] public GameObject groundCellPrefab;
    [SerializeField] public GameObject planeRightArrowPrefab;
    [SerializeField] public GameObject planeBotArrowPrefab;
    [SerializeField] public GameObject planeTopArrowPrefab;
    [SerializeField] public GameObject planeLeftArrowPrefab;
    [SerializeField] public GameObject cellGrid;
    [SerializeField] public GameObject mainCamera;

    public bool isHuman = true;
    public bool isAndroid;
    
    public static GameObject currentPlayerObject;
    public static List<GameObject> boxesObjects = new List<GameObject>();
    private IGame game;

    public static Controller instance;

    public static List<GameObject> debugObjects;

    private void Start()
    {
        instance = this;
    }

    public void InitGame(GameType gameType)
    {
        DestroyOldScene();

        Vector3 pos = mainCamera.transform.position;
        switch (gameType)
        {
            case GameType.Soooookolat:
                game = new SoooookolatGame();
                pos = mainCamera.transform.position;
                pos.z = 3.5f;
                mainCamera.transform.position = pos;
                break;
            case GameType.TicTacTard: 
                game = new TicTacTardGame();
                pos.x = 0;
                pos.z = 0;
                break;
            case GameType.GridWORLDO:
                game = new GridWORDOGame();
                pos.x = GridWORDOGame.MAX_CELLS_PER_LINE / 2;
                pos.z = GridWORDOGame.MAX_CELLS_PER_COLUMN / 2;
                mainCamera.transform.position = pos;
                break;
        }

        debugObjects = new List<GameObject>();
        game?.InitGame();
        GenerateScene();
    }

    public void StartGame()
    {
        ClearDebugObjects();
        game.InitIntent(isHuman);

        StartCoroutine(game.StartGame());
    }

    private void Update()
    {
        game?.UpdateGame();
    }


    public void TicTacTardSimulation()
    {
        (game as TicTacTardGame).SimulateTestCase();
    }
    
    public void DestroyOldScene()
    {
        int safeLoopIteration = 0;

        foreach (Transform child in parentGeneratedScene.transform)
        {
            Destroy(child.gameObject);
        }

        game = null;
    }
    
    public static void InstantiateArrowByIntent(Intent intent, int x, int y, float stateValue)
    {
        GameObject go = null;

        if (!ControllerEditor.showArrow)
        {
            return;
        }
        
        switch (intent)
        {
            case Intent.WantToGoBot:
                go = Instantiate(instance.planeBotArrowPrefab, instance.parentGeneratedScene.transform);
                break;
            case Intent.WantToGoLeft:
                go = Instantiate(instance.planeLeftArrowPrefab, instance.parentGeneratedScene.transform);
                break;
            case Intent.WantToGoRight:
                go = Instantiate(instance.planeRightArrowPrefab, instance.parentGeneratedScene.transform);
                break;
            case Intent.WantToGoTop:
                go = Instantiate(instance.planeTopArrowPrefab, instance.parentGeneratedScene.transform);
                break;
            default:
                Debug.Log(intent);
                break;
        }

        if (go)
        {
            go.transform.position = new Vector3(x, 1f, y);

            if (ControllerEditor.showStateValue)
            {
                go.transform.GetChild(0).GetComponent<TextMeshPro>().text = stateValue.ToString();
            }

            debugObjects.Add(go);
        }
    }

    private void ClearDebugObjects()
    {
        foreach (GameObject go in debugObjects.ToArray())
        {
            Destroy(go);
        }
    }

    private void GenerateScene()
    {
        List<List<ICell>> cells = game?.GetCells();

        if (cells != null)
        {
            GameObject instantiateGo;
            int x = 0;
            int y = 0;

            foreach (List<ICell> cellsPerLine in cells)
            {
                y = 0;
                foreach (ICell cell in cellsPerLine)
                {
                    Vector3 pos = new Vector3(cell.GetPosition().x, 0, cell.GetPosition().y);
                    switch (cell.GetCellType())
                    {
                        case CellType.Obstacle:
                            instantiateGo = Instantiate(wallPrefab, parentGeneratedScene.transform);
                            instantiateGo.transform.position = pos;
                            cell.SetCellGameObject(instantiateGo);
                            break;
                        case CellType.Player:
                            currentPlayerObject = instantiateGo = Instantiate(playerPrefab, parentGeneratedScene.transform);
                            instantiateGo.transform.position = pos;

                            pos.y -= 0.5f;
                            instantiateGo = Instantiate(groundCellPrefab, parentGeneratedScene.transform);
                            instantiateGo.transform.position = pos;
                            cell.SetCellGameObject(instantiateGo);
                            break;
                        case CellType.Goal:
                            instantiateGo = Instantiate(goalPrefab, parentGeneratedScene.transform);
                            instantiateGo.transform.position = pos;
                            cell.SetCellGameObject(instantiateGo);
                            break;
                        case CellType.Box:
                            instantiateGo = Instantiate(boxPrefab, parentGeneratedScene.transform);
                            boxesObjects.Add(instantiateGo);
                            instantiateGo.transform.position = pos;
                            
                            pos.y -= 0.5f;
                            instantiateGo = Instantiate(groundCellPrefab, parentGeneratedScene.transform);
                            instantiateGo.transform.position = pos;
                            cell.SetCellGameObject(instantiateGo);
                            break;
                        case CellType.EndGoal:
                            instantiateGo = Instantiate(endGoalPrefab, parentGeneratedScene.transform);
                            instantiateGo.transform.position = pos;
                            cell.SetCellGameObject(instantiateGo);
                            break;
                        case CellType.Empty:
                            if (ControllerEditor.gameSelected == GameType.TicTacTard)
                            {
                                instantiateGo = Instantiate(cellGrid, parentGeneratedScene.transform);
                                pos.y -= 0.5f;
                                instantiateGo.transform.position = pos;
                                instantiateGo.name = $"GRID_{x}_{y}";
                                cell.SetCellGameObject(instantiateGo);
                            } 
                            else
                            {
                                instantiateGo = Instantiate(groundCellPrefab, parentGeneratedScene.transform);
                                pos.y -= 0.5f;
                                instantiateGo.transform.position = pos;
                                cell.SetCellGameObject(instantiateGo);
                            }
                            break;
                    }

                    ++y;
                }

                ++x;
            }
        }
    }
}