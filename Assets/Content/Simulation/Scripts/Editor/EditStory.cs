using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EditorBuild))]
public class EditStory : Editor {

    EditorBuild.MessageHelp message;
    int indexMoment = 0;

    void OnEnable()
    {
        message = new EditorBuild.MessageHelp("Welcome Editor mode", MessageType.Info);
    }

    public override void OnInspectorGUI()
    {
       
        DrawDefaultInspector();
        EditorBuild editorScript = (EditorBuild)target;

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);
        GUILayout.Label("EDITOR MODE");
        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);

        GUILayout.Label("Story");  

        if (GUILayout.Button("StoryLenght"))
        {
            message = editorScript.SizeHistory();
        }

        if (GUILayout.Button("Create History"))
        {
            message = editorScript.CreateHistory();
        }

        if (GUILayout.Button("Add Audio"))
        {
            message = editorScript.AddAudio();
        }


        indexMoment = EditorGUILayout.IntField("Index Moment", indexMoment);

        if (GUILayout.Button("Join"))
        {
            message = editorScript.PutHistory(indexMoment);
        }

        EditorGUILayout.HelpBox(message.helpMessage,message.messageType);

        if (GUILayout.Button("Reset"))
        {
            editorScript.Reset();
        }
    }


}
