using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
using UnityEditor;
using VRpen.Scripts.Examples;

namespace VRpen.Scripts.Examples.Editor
{
    [CustomEditor(typeof(LineExample))]
    public class EditorScriptLineExample : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Defaults has to be set to a DefaultReferences.\n" +
                                    "DefaultReferences are Scriptable Objects, containing references to frequently" +
                                    " used prefabs, materials etc.", MessageType.Info);

            DrawDefaultInspector();

            EditorGUILayout.HelpBox("Use the buttons below to interact with the scene at runtime", MessageType.Info);

            LineExample lineScript = (LineExample) target;
            if (GUILayout.Button("Undo"))
            {
                lineScript.UndoActions(1);
            }

            if (GUILayout.Button("Redo"))
            { 
                lineScript.RedoActions(1);
            }
        }

    }
}
