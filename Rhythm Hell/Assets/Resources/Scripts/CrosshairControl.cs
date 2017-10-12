using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairControl : MonoBehaviour {

    public RhythmicObjects2 theRhythmManager;

	// Use this for initialization
	void Start () {
        theRhythmManager = FindObjectOfType<RhythmicObjects2>();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Image>().fillAmount = (theRhythmManager.theAudioSource.time / theRhythmManager.theWaitRhythm) - (int)(theRhythmManager.theAudioSource.time / theRhythmManager.theWaitRhythm);

    }
}
