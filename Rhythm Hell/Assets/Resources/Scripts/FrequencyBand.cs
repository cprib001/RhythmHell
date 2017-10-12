using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyBand : MonoBehaviour {
    public int theBandIndex;
    public float theStartScale;
    public float theScaleMultiplier;
    public RhythmicObjects2 theManager;

    public bool useBuffer;
    public Material material;

	// Use this for initialization
	void Start () {
        material = GetComponent<MeshRenderer>().materials[0];
	}
	
	// Update is called once per frame
	void Update () {
        if(!useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (theManager.theAudioBands[theBandIndex] * theScaleMultiplier) + theStartScale, transform.localScale.z);
            Color color = new Color(theManager.theAudioBands[theBandIndex], theManager.theAudioBands[theBandIndex], theManager.theAudioBands[theBandIndex]);
            material.SetColor("_EmissionColor", color);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, (theManager.theAudioBandBuffer[theBandIndex] * theScaleMultiplier) + theStartScale, transform.localScale.z);
            Color color = new Color(theManager.theAudioBandBuffer[theBandIndex], theManager.theAudioBandBuffer[theBandIndex], theManager.theAudioBandBuffer[theBandIndex]);
            material.SetColor("_EmissionColor", color);
        }
	}
}
