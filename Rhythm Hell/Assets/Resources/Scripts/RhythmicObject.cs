// Created by Carlos Arturo Rodriguez Silva (Legend) https://www.youtube.com/watch?v=8mBZPROvR-o
// Contact: ryolxgame@gmail.com or http://facebook.com/legendxh

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DisallowMultipleComponent]
[AddComponentMenu("Audio/Rhythmic Object")]
public class RhythmicObject : MonoBehaviour {
	
	[Space(5)]
	public bool active = true;

	[Header("Local Scale Control")]
	public Vector3 initialScale = Vector3.one;
	[Space(10)]
	public Vector3 minExtraScale = new Vector3(0.1f, 0.1f, 0.1f);
	public Vector3 maxExtraScale = new Vector3 (0.5f, 0.5f, 0.5f);

	public void LowSensibility () {
		// Set scale when the minimum sensibility are exceeded
		if (active) {
			transform.localScale = initialScale + minExtraScale;
		}
	}

	public void HighSensibility () {
		// Set scale when the maximum sensibility are exceeded
		if (active) {
			transform.localScale = initialScale + maxExtraScale;
		}
	}
		
	#region RhytmicObjectControl 
	#if UNITY_EDITOR
	public void InsertToBaseScript () {
		if (EditorApplication.isPlaying) {
			var rhythmicObjects = FindObjectOfType<RhythmicObjects> ().rhythmicObjects;
			if (rhythmicObjects.Contains (this.gameObject)) {
				Debug.LogWarning (this.gameObject.name + " already exists as Rhythmic Object");
			} else {
				int count = FindObjectOfType<RhythmicObjects> ().rhythmicObjects.Count;
				FindObjectOfType<RhythmicObjects> ().rhythmicObjects.Insert (count, this.gameObject);
				Debug.Log (this.gameObject.name + " has been added as Rhythmic Object");
			}
		} else {
			Debug.LogWarning ("The Editor is not Playing");
		}
	}

	public void DeleteFromBaseScript () {
		if (EditorApplication.isPlaying) {
			FindObjectOfType<RhythmicObjects> ().rhythmicObjects.Remove (this.gameObject);
			transform.localScale = initialScale;
			Debug.Log (this.gameObject.name + " has been deleted as Rhythmic Object");
		} else {
			Debug.LogWarning ("The Editor is not Playing");
		}
	}

	void OnDestroy () {
		if (EditorApplication.isPlayingOrWillChangePlaymode) {
			DeleteFromBaseScript ();
		}
	}
	#endif
	#endregion
}
