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
    }
}