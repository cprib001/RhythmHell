// Created by Carlos Arturo Rodriguez Silva (Legend) https://www.youtube.com/watch?v=8mBZPROvR-o
// Contact: ryolxgame@gmail.com or http://facebook.com/legendxh

using UnityEngine;
using System.Collections;

public class LoadSongs : MonoBehaviour {

	public string actualSongName = "";

	[Space(5)]
	public bool loadDefaultSong = true;
	public string defaultSong = "Song name";

	void Start () {
		if (loadDefaultSong) {
			LoadSong (defaultSong);
		}
	}

	public void LoadSong (string songName) {
		actualSongName = songName;
		FindObjectOfType<RhythmicObjects> ().SetSongData (songName);
	}
}
