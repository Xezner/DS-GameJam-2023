using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ButtonTest))]
public class ObjectGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("GENERATE NEW OBJECT"))
        {
            ObjectGenerator.Instance.GenerateObjectPool();
        }
    }
}
