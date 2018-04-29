using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Group))]
public class GroupEditor : Editor 
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //Group g = (Group)target;
        //g.group = EditorGUILayout.l
    }
}
