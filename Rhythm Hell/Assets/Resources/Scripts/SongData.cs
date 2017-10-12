// Created by Carlos Arturo Rodriguez Silva (Legend) https://www.youtube.com/watch?v=8mBZPROvR-o
// Contact: ryolxgame@gmail.com or http://facebook.com/legendxh

using UnityEngine;
using System.Collections;

public class SongData : MonoBehaviour {

	[Tooltip("Song Audio Clip")]
	public AudioClip audioClip;
	public string songName;
	[Tooltip("Beats Per Minute (please read the tutorial)")]
	public float BPM = 128f;
	[Tooltip("Off-Set in Miliseconds (please read the tutorial)")]
	public float offsetMS;
	[Tooltip("Minimum spectrum sensibility")]
	public float minimumSensibility = 1f;
	[Tooltip("Maximum spectrum sensibility")]
	public float sensibility = 8f;

	void Awake () {
		gameObject.name = songName;
	}

}
