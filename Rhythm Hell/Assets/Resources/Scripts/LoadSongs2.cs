using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSongs2 : MonoBehaviour {

    public string theActualSongName = "";

    public bool loadDefaultSong = true;
    public string theDefaultSong = "Song name";

	// Use this for initialization
	void Start () {
		if (loadDefaultSong)
        {
            LoadSong(theDefaultSong);
        }
	}
	
	public void LoadSong(string songName)
    {
        theActualSongName = songName;
        FindObjectOfType<RhythmicObjects2>().SetSongData(songName);
    }
}
