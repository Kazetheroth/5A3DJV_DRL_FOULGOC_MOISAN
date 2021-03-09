using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Controller))]
public class ControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Controller controller = (Controller) target;

        if (!Application.isPlaying) return;

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
        
        if (GUILayout.Button("Play GridWORLDO"))
        {
            controller.StartGame(Controller.GameType.GridWORLDO);
        }

        if (GUILayout.Button("Play TicTacTard"))
        {
            controller.StartGame(Controller.GameType.TicTacTard);
        }

        if (GUILayout.Button("Play Soooookolat"))
        {
            controller.StartGame(Controller.GameType.Soooookolat);
        }

        if (GUILayout.Button("Clean scene"))
        {
            controller.DestroyOldScene();
        }
    }
}