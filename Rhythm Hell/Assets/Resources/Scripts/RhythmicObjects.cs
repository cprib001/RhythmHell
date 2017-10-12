// Created by Carlos Arturo Rodriguez Silva (Legend) https://www.youtube.com/watch?v=8mBZPROvR-o
// Contact: ryolxgame@gmail.com or http://facebook.com/legendxh

using UnityEngine;
using System.Collections.Generic;

public class RhythmicObjects : MonoBehaviour {

	[Tooltip("AudioSource to get the SpectrumData")]
	public AudioSource audioS;
	[Tooltip("AudioSource that will play the metronome")]
	public AudioSource metronome;

	[Space(10)]
	[Tooltip("Set here the object that contains the Song List")]
	public GameObject songList;

	[Space(5)]
	[Tooltip("Rhythmic Objects")]
	public List<GameObject> rhythmicObjects;

	[Space(5)]
	public bool useMetronome;

	[Header("Song Data")]
	[Tooltip("Beats Per Minute (please read the tutorial)")]
	public float BPM;
	float actualBPM;
	[Tooltip("Off-Set in Miliseconds (please read the tutorial)")]
	public float offsetMS;
	float actualOffsetMS;

	[Range (0f, 15f)]
	[Tooltip("Minimum Spectrum Detection (if zero then the Objects will be scale all time")]
	public float minSensibility = 4f;
	[Range(0f, 15f)]
	[Tooltip("Maximum Spectrum Detection")]
	public float sensibility = 6f;

	[Header("Global Scales Control")]
	[Tooltip("Deactivate to use custom scales for each object")]
	public bool useGlobalScales = true;

	[Header("Global X Scale")]
	public bool ChangeXScale = true;
	[Range(0f, 10f)]
	public float minExtraXScale = 0.1f;
	[Range(0f, 10f)]
	public float maxExtraXScale = 0.5f;

	[Header("Global Y Scale")]
	public bool ChangeYScale = true;
	[Range(0f, 10f)]
	public float minExtraYScale = 0.1f;
	[Range(0f, 10f)]
	public float maxExtraYScale = 0.5f;

	[Header("Global Z Scale")]
	public bool ChangeZScale = true;
	[Range(0f, 10f)]
	public float minExtraZScale = 0.1f;
	[Range(0f, 10f)]
	public float maxExtraZScale = 0.5f;

	[Header("Levels Control")] // Channel 0
	[Range(1f, 300f)]
	public float bassSensibility = 80f;

	[Range(1f, 300f)]
	public float trebleSensibility = 40f;

	bool active;

	float nextBPMTime;
	float waitRhythm;
	float waitRhythm2;

	bool ready;
	bool proReady;

	bool MsUpdated;

	float lastOffsetMS;

	string actualSong;

	void Awake () {
		// Increase Pixel Light Count to use more Lights (for EXAMPLE SCENE)
		QualitySettings.pixelLightCount = 10; // Comment this if you don't need it

		// Search for a Rhythmic Gameobjects
		var scripts = Object.FindObjectsOfType<RhythmicObject> ();

		// Insert to gameObjects List
		foreach (RhythmicObject rhyObject in scripts) {
			rhythmicObjects.Insert(rhythmicObjects.Count, rhyObject.gameObject); 
		}

		// Remove excess
		rhythmicObjects.TrimExcess ();
	}


	public void SetSongData (string songName) {
		
		// Deactivate bools
		active = false;
		ready = false;
		proReady = false;

		// Get Song Data
		actualSong = songName;

		try {
			audioS.clip = songList.transform.Find (songName).GetComponent<SongData> ().audioClip;
			minSensibility = songList.transform.Find (songName).GetComponent<SongData> ().minimumSensibility;
			sensibility = songList.transform.Find (songName).GetComponent<SongData> ().sensibility;
			actualBPM = songList.transform.Find (songName).GetComponent<SongData> ().BPM;
			actualOffsetMS = songList.transform.Find (songName).GetComponent<SongData> ().offsetMS;
		
			BPM = actualBPM;
			offsetMS = actualOffsetMS;

			// Calculate the time for beetween BPM's
			waitRhythm2 = (actualBPM / 60f);
			waitRhythm = (1f / waitRhythm2);
			nextBPMTime = waitRhythm;

			// Play the song
			if (audioS.clip != null) {
				active = true;
				Debug.Log (songName + " song started.");
				audioS.Play ();
			} else {
				Debug.LogWarning (songName + " doesn't have an Audio Clip to Play!");
			}
		} catch {
			Debug.LogWarning ("The song: " + actualSong + " doesn't exists in the list.");
			enabled = false;
		}

	}

	void Update () {
		if (active) {

			// Get Channel 0 Spectrum
			#pragma warning disable 618
			float[] spectrumleft = audioS.GetSpectrumData (4096, 0, FFTWindow.Blackman);
			#pragma warning restore 618

			// Get Values from spectrum left
			for (int i = 0; i < spectrumleft.Length; i++) {

				// Apply sensibility
				var spectrumLeftValue = spectrumleft [i] * bassSensibility;

				// Check if the value are greater or equal than the sensibilitys
				if (spectrumLeftValue >= minSensibility) {
					ready = true;
					if (spectrumLeftValue >= sensibility) {
						proReady = true;
					}
				}
			}

			// Then Lerp to Initial Scale
			LerpToInitialScale ();

		}
	}

	void FixedUpdate () {
		if (active) {
			if (audioS.time >= nextBPMTime) { // Wait for the next BPM

				// If Off-set are Updated then calculate the time for the next BPM
				if (MsUpdated) {
					nextBPMTime += ((waitRhythm / audioS.pitch));
				} else { // Else, apply the new Off-set
					nextBPMTime += ((waitRhythm / audioS.pitch)) + (actualOffsetMS / 1000f) - lastOffsetMS;
					lastOffsetMS = (actualOffsetMS / 1000f);
					MsUpdated = true;
				}

				// Metronome
				if (useMetronome) {
					if (metronome != null) {
						metronome.Play ();
					} else {
						Debug.LogWarning ("Please assign metronome variable to Rhythmic Objects script.");
					}
				}

				// Scales
				if (ready) {

					if (useGlobalScales) {
						if (ChangeXScale) {
							SetXScale ();
						}

						if (ChangeYScale) {
							SetYScale ();
						}

						if (ChangeZScale) {
							SetZScale ();
						}
					} else {
						SetPrivateScale ();
					}

					proReady = false;
					ready = false;
				}
			}

			// If the Next BPM Time is greater than audio length, then, the song ended.
			if (nextBPMTime >= audioS.clip.length) {
				active = false;
				nextBPMTime = 0f;
				audioS.Stop ();
				Debug.Log (actualSong + " song ended.");
			}
		}
	}

	public void UpdateBPMOffSet () {
		// Update BPM and Offset
		MsUpdated = false;

		// Calculate the time for beetween BPM's
		waitRhythm2 = (BPM / 60f);
		waitRhythm = (1f / waitRhythm2);

		// Set the new values
		actualOffsetMS = Mathf.Abs (offsetMS);
		actualBPM = BPM;

		Debug.Log ("BPM and Off-set updated.");
	}

	#region Scales Control
	void LerpToInitialScale () {
			foreach (GameObject i in rhythmicObjects) {
				i.transform.localScale = Vector3.Lerp (i.transform.localScale, i.GetComponent<RhythmicObject> ().initialScale, Time.deltaTime * 10f);
			}
	}

	void SetXScale () {
		
		// Set X Scale to all Objects in the list.
		foreach (GameObject i in rhythmicObjects) {
			var newScale = i.transform.localScale;
			if (!proReady) {
				newScale.x = minExtraXScale + i.GetComponent<RhythmicObject> ().initialScale.x;
			} else {
				newScale.x = maxExtraXScale + i.GetComponent<RhythmicObject> ().initialScale.x;
			}

			i.transform.localScale = newScale;
		}
	}

	void SetYScale () {
		
		// Set Y Scale to all Objects in the list.
		foreach (GameObject i in rhythmicObjects) {
			var newScale = i.transform.localScale;
			if (!proReady) {
				newScale.y = minExtraYScale + i.GetComponent<RhythmicObject> ().initialScale.y;
			} else {
				newScale.y = maxExtraYScale + i.GetComponent<RhythmicObject> ().initialScale.y;
			}

			i.transform.localScale = newScale;
		}
	}

	void SetZScale () {
		
		// Set Z Scale to all Objects in the list.
		foreach (GameObject i in rhythmicObjects) {
			var newScale = i.transform.localScale;
			if (!proReady) {
				newScale.z = minExtraYScale + i.GetComponent<RhythmicObject> ().initialScale.z;
			} else {
				newScale.z = maxExtraYScale + i.GetComponent<RhythmicObject> ().initialScale.z;
			}

			i.transform.localScale = newScale;
		}
	}

	void SetPrivateScale () {
		
		// Set private scale to all Objects in the list.
		foreach (GameObject i in rhythmicObjects) {
			if (!proReady) {
				i.GetComponent<RhythmicObject> ().LowSensibility ();
			} else {
				i.GetComponent<RhythmicObject> ().HighSensibility ();
			}
		}
	}
	#endregion
}
