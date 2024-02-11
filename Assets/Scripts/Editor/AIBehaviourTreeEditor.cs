using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(AIBehaviourTree))]
public class AIBehaviourTreeEditor : Editor
{
    private bool needsRepaint = false;

    public override void OnInspectorGUI()
    {
        //EditorGUI.BeginChangeCheck();

        DrawDefaultInspector();
        //AIBehaviourTree tree = (AIBehaviourTree)target;

        //if (EditorGUI.EndChangeCheck())
        //{
        //    EditorUtility.SetDirty(target);
        //    needsRepaint = true;
        //}

        //if (needsRepaint)
        //{
        //    needsRepaint = false;
        //    EditorApplication.delayCall += () => Repaint();
        //}
    }
}