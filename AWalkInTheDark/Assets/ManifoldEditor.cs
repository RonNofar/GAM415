using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Manifold.Manifold))]
public class ManifoldEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Manifold.Manifold myScript = (Manifold.Manifold)target;
        if (GUILayout.Button("Load Manifold"))
        {
            myScript.CreateManifold();
        }
    }
}
#endif