using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JumpBetweenNodesTest))]
public class EditorJumpTest : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        JumpBetweenNodesTest script = (JumpBetweenNodesTest)target;

        if (GUILayout.Button("Jump UP"))
            script.JumpUp();
        
        if (GUILayout.Button("Jump Down"))
            script.JumpDown();

    }
}
