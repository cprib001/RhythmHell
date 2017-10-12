// Created by Carlos Arturo Rodriguez Silva (Legend) https://www.youtube.com/watch?v=8mBZPROvR-o
// Contact: ryolxgame@gmail.com or http://facebook.com/legendxh

using UnityEngine;
using UnityEditor;

namespace UnityEditor.UI {

	static internal class RhythmicObjectsEditor {

		[MenuItem ("GameObject/Audio/Rhythmic Object", false)]
		static public void AddFollowRhythm(MenuCommand menuCommand) {
			var target = menuCommand.context as GameObject;
			Undo.AddComponent<RhythmicObject> (target);
		}

		[MenuItem ("GameObject/UI/Rhythmic Object", false)]
		static public void AddFollowRhythmUI(MenuCommand menuCommand) {
			var target = menuCommand.context as GameObject;
			Undo.AddComponent<RhythmicObject> (target);
		}
	}
}

[CustomEditor(typeof(RhythmicObjects))]
public class RhythmicObjectsEditor : Editor
{
	public override void OnInspectorGUI() {
		var rhythmicObjects = (RhythmicObjects)target;

		if (GUILayout.Button("Update BPM / Off-set")) { rhythmicObjects.UpdateBPMOffSet (); }
		DrawDefaultInspector ();
	}
}