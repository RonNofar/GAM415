using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Manifold.FatherLord))]
public class FatherLordEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Manifold.FatherLord myScript = (Manifold.FatherLord)target;
        if (GUILayout.Button("Kill Children"))
        {
            myScript.KillChildren();
        }
    }
}
#endif
