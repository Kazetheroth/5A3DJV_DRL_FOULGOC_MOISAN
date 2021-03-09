using GridWORLDO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Controller))]
public class ControllerEditor : Editor
{
    private Controller.GameType gameSelected;

    public static bool showStateValue = false;
    public static bool showArrow = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Controller controller = (Controller) target;

        Color defaultColor = GUI.color;

        if (!Application.isPlaying) return;

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
            controller.InitGame(Controller.GameType.TicTacTard);
            gameSelected = Controller.GameType.TicTacTard;
        }

        if (gameSelected == Controller.GameType.TicTacTard)
        {
            
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        
        GUI.color = Color.blue;
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUI.color = defaultColor;

        if (GUILayout.Button("Play Soooookolat"))
        {
            controller.InitGame(Controller.GameType.Soooookolat);
            gameSelected = Controller.GameType.Soooookolat;
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
}