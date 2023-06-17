using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ButtonTest))]
public class CardGenerationEditorTester : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("GENERATE NEW OBJECT"))
        {
            CardMechanicManager.Instance.GenerateCardPool();
        }
    }
}
