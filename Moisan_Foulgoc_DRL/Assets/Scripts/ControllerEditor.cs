using GridWORLDO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Controller))]
public class ControllerEditor : Editor
{
    public static Controller.GameType gameSelected;

    public static bool showStateValue = false;
    public static bool showArrow = false;

    public override void OnInspectorGUI()
    {
        Controller controller = target as Controller;

        Color defaultColor = GUI.color;

        if (!Application.isPlaying)
        {
            EditorGUI.BeginChangeCheck();
            controller.parentGeneratedScene = (GameObject) EditorGUILayout.ObjectField("parentGeneratedScene", controller.parentGeneratedScene, typeof(GameObject), true);

            controller.wallPrefab = (GameObject) EditorGUILayout.ObjectField("wallPrefab", controller.wallPrefab, typeof(GameObject), false);
            controller.playerPrefab = (GameObject) EditorGUILayout.ObjectField("playerPrefab", controller.playerPrefab, typeof(GameObject), false);
            controller.goalPrefab = (GameObject) EditorGUILayout.ObjectField("goalPrefab", controller.goalPrefab, typeof(GameObject), false);
            controller.boxPrefab = (GameObject) EditorGUILayout.ObjectField("boxPrefab", controller.boxPrefab, typeof(GameObject), false);
            controller.endGoalPrefab = (GameObject) EditorGUILayout.ObjectField("endGoalPrefab", controller.endGoalPrefab, typeof(GameObject), false);
            controller.groundCellPrefab = (GameObject) EditorGUILayout.ObjectField("groundCellPrefab", controller.groundCellPrefab, typeof(GameObject), false);
            controller.planeRightArrowPrefab = (GameObject) EditorGUILayout.ObjectField("planeRightArrowPrefab", controller.planeRightArrowPrefab, typeof(GameObject), false);
            controller.planeBotArrowPrefab = (GameObject) EditorGUILayout.ObjectField("planeBotArrowPrefab", controller.planeBotArrowPrefab, typeof(GameObject), false);
            controller.planeTopArrowPrefab = (GameObject) EditorGUILayout.ObjectField("planeTopArrowPrefab", controller.planeTopArrowPrefab, typeof(GameObject), false);
            controller.planeLeftArrowPrefab = (GameObject) EditorGUILayout.ObjectField("planeLeftArrowPrefab", controller.planeLeftArrowPrefab, typeof(GameObject), false);
            controller.cellGrid = (GameObject) EditorGUILayout.ObjectField("cellGridPrefab", controller.cellGrid, typeof(GameObject), true);
            controller.mainCamera = (GameObject) EditorGUILayout.ObjectField("camera", controller.mainCamera, typeof(GameObject), true);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
            
            return;
        }

        GUI.color = Color.blue;
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUI.color = defaultColor;

        if (GUILayout.Button("Play GridWORLDO"))
        {
            controller.InitGame(Controller.GameType.GridWORLDO);
            gameSelected = Controller.GameType.GridWORLDO;
        }

        if (gameSelected == Controller.GameType.GridWORLDO)
        {
            DisplayGridWorldOptions(controller);
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        
        GUI.color = Color.blue;
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUI.color = defaultColor;

        if (GUILayout.Button("Play TicTacTard"))
        {
            gameSelected = Controller.GameType.TicTacTard;
            controller.InitGame(Controller.GameType.TicTacTard);
        }

        if (gameSelected == Controller.GameType.TicTacTard)
        {
            DisplayTicTacTard(controller);
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        
        GUI.color = Color.blue;
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUI.color = defaultColor;

        if (GUILayout.Button("Play Soooookolat"))
        {
            gameSelected = Controller.GameType.Soooookolat;
            controller.InitGame(Controller.GameType.Soooookolat);
        }

        if (gameSelected == Controller.GameType.Soooookolat)
        {
            
        }
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Clean scene"))
        {
            controller.DestroyOldScene();
        }
    }

    private void DisplayGridWorldOptions(Controller controller)
    {
        EditorGUI.BeginChangeCheck();
        controller.isHuman = EditorGUILayout.Toggle("Is human", controller.isHuman);

        if (EditorGUI.EndChangeCheck())
        {
            controller.isAndroid = !controller.isHuman;
        }
        
        EditorGUI.BeginChangeCheck();
        controller.isAndroid = EditorGUILayout.Toggle("Is android", controller.isAndroid);

        if (EditorGUI.EndChangeCheck())
        {
            controller.isHuman = !controller.isAndroid;
        }

        showArrow = EditorGUILayout.Toggle("Display path arrow", showArrow);

        EditorGUI.BeginChangeCheck();
        showStateValue = EditorGUILayout.Toggle("Display state value", showStateValue);
        if (EditorGUI.EndChangeCheck() && showStateValue)
        {
            showArrow = true;
        }

        GridWORDOGame.chosenAlgo =
            (GridWORDOGame.Algo) EditorGUILayout.EnumPopup("Algo chosen", GridWORDOGame.chosenAlgo);

        if (GUILayout.Button("Démarrer le jeu"))
        {
            controller.StartGame();
        }

        if (GUILayout.Button("Regénérer la carte"))
        {
            controller.InitGame(Controller.GameType.GridWORLDO);
        }
    }

    private void DisplayTicTacTard(Controller controller)
    {
        if (GUILayout.Button("Démarrer le jeu"))
        {
            controller.StartGame();
        }
    }
}