// Created by Carlos Arturo Rodriguez Silva (Legend) https://www.youtube.com/watch?v=8mBZPROvR-o
// Contact: ryolxgame@gmail.com or http://facebook.com/legendxh

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RhythmicObject))]
public class RhythmicObjectEditor : Editor
{
	public override void OnInspectorGUI()
	{
		RhythmicObject rhythmicObj = (RhythmicObject)target;
		DrawDefaultInspector ();
		if (GUILayout.Button("Insert to Base")) { rhythmicObj.InsertToBaseScript (); }
		if (GUILayout.Button("Delete from Base")) { rhythmicObj.DeleteFromBaseScript (); }
	}
}