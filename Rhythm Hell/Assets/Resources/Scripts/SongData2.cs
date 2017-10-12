using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongData2 : MonoBehaviour {

    public AudioClip theAudioClip;
    public string theSongName;
    public string theArtistName;
    public float theBPM = 128f;
    public float theOffsetMS;
    public float theMinSensibility = 1f;
    public float theMaxSensibility = 8f;

    void Awake() {
        gameObject.name = theSongName;
    }
}
