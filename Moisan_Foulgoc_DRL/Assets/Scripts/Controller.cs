using System;
using System.Collections.Generic;
using GridWORLDO;
using Interfaces;
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

    public GameObject parentGeneratedScene;

    public GameObject wallPrefab;
    public GameObject playerPrefab;
    public GameObject endGoalPrefab;
    public GameObject groundCellPrefab;
    public GameObject planeRightArrowPrefab;
    public GameObject planeBotArrowPrefab;
    public GameObject planeTopArrowPrefab;
    public GameObject planeLeftArrowPrefab;
    [SerializeField] public GameObject mainCamera;

    public bool isHuman = true;
    public bool isAndroid;
    
    public static GameObject currentPlayerObject;
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
                Vector3 pos = mainCamera.transform.position;
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
                        case CellType.EndGoal:
                            instantiateGo = Instantiate(endGoalPrefab, parentGeneratedScene.transform);
                            instantiateGo.transform.position = pos;
                            cell.SetCellGameObject(instantiateGo);
                            break;
                        case CellType.Empty:
                            instantiateGo = Instantiate(groundCellPrefab, parentGeneratedScene.transform);
                            pos.y -= 0.5f;
                            instantiateGo.transform.position = pos;
                            cell.SetCellGameObject(instantiateGo);
                            break;
                    }
                }
            }
        }
    }
}