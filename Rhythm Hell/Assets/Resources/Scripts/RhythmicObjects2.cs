using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmicObjects2 : MonoBehaviour {

    public AudioSource theAudioSource;
    public AudioSource theMetronome;
    public GameObject theSongList;
    public List<GameObject> theRhythmicObjects;

    public bool useMetronome;

    public float theBPM;
    public float theActualBPM;
    public float theOffsetMS;
    public float theActualOffsetMS;

    [Range(0f, 15f)]
    public float theMinSensibility = 4f;
    [Range(0f, 15f)]
    public float theMaxSensibility = 6f;

    public bool useGlobalScales = true;

    public bool ChangeXScale = true;
    [Range(0f, 10f)]
    public float theMinExtraXScale = 0.1f;
    [Range(0f, 10f)]
    public float theMaxExtraXScale = 0.5f;

    public bool ChangeYScale = true;
    [Range(0f, 10f)]
    public float theMinExtraYScale = 0.1f;
    [Range(0f, 10f)]
    public float theMaxExtraYScale = 0.5f;

    public bool ChangeZScale = true;
    [Range(0f, 10f)]
    public float theMinExtraZScale = 0.1f;
    [Range(0f, 10f)]
    public float theMaxExtraZScale = 0.5f;

    [Range(1f, 300f)]
    public float theBassSensibility = 80f;
    [Range(1f, 300f)]
    public float theTrebleSensibility = 40f;

    public bool isActive;

    public float theNextBPMTime;
    public float theWaitRhythm;
    public float theWaitRhythm2;

    bool isReady;
    bool isProReady;

    bool isMsUpdated;

    public float theLastOffsetMS;

    public string theActualSong;

    public float[] theSamples = new float[512];
    public float[] theBands = new float[8];
    public float[] theBandBuffer = new float[8];
    public float[] theBufferDecrease = new float[8];
    public float[] theBandMax = new float[8];
    public float[] theAudioBands = new float[8];
    public float[] theAudioBandBuffer = new float[8];

    public float theAmplitude;
    public float theAmplitudeBuffer;
    public float theAmplitudeMax;

    public bool beat = false;

    public GameManager theGameManager;

    void Awake()
    {
        RhythmicObject2[] scripts = FindObjectsOfType<RhythmicObject2>();

        foreach(RhythmicObject2 rhythmicObject in scripts)
        {
            theRhythmicObjects.Insert(theRhythmicObjects.Count, rhythmicObject.gameObject);
        }

        theRhythmicObjects.TrimExcess();
    }
	
	// Update is called once per frame
	void Update () {
		if(isActive)
        {
            #pragma warning disable 618
            theSamples = theAudioSource.GetSpectrumData(512, 0, FFTWindow.Blackman);
            #pragma warning restore 618
            MakeFrequencyBands();
            BandBuffer();
            CreateAudioBands();
            GetAmplitude();

            for (int i = 0; i < theSamples.Length; i++)
            {
                float spectrumLeftValue = theSamples[i] * theBassSensibility;

                if(spectrumLeftValue >= theMinSensibility)
                {
                    isReady = true;
                    if(spectrumLeftValue >= theMaxSensibility)
                    {
                        isProReady = true;
                    }
                }
            }
            LerpToInitialScale();
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (beat)
        //    {
        //        Debug.Log("Success");
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Fail");
        //    }
        //}
    }

    void FixedUpdate()
    {
        if(isActive)
        {
            if (theAudioSource.time >= theNextBPMTime)
            {
                if(isMsUpdated)
                {
                    theNextBPMTime += ((theWaitRhythm / theAudioSource.pitch));
                }
                else
                {
                    theNextBPMTime += ((theWaitRhythm / theAudioSource.pitch)) + (theActualOffsetMS / 1000f) - theLastOffsetMS;
                    theLastOffsetMS = (theActualOffsetMS / 1000f);
                    isMsUpdated = true;
                }

                if(useMetronome)
                {
                    if(theMetronome != null)
                    {
                        theMetronome.Play();
                    }
                    else
                    {
                        Debug.LogWarning("Please assign metronome variable to rythmic Objects script.");
                    }
                }

                StartCoroutine(Beat());

                if(isReady)
                {
                    if (useGlobalScales) {
                        if(ChangeXScale)
                        {
                            SetXScale();
                        }
                        if(ChangeYScale)
                        {
                            SetYScale();
                        }
                        if(ChangeZScale)
                        {
                            SetZScale();
                        }
                    }
                    else
                    {
                        SetPrivateScale();
                    }

                    isProReady = false;
                    isReady = false;
                }
            }

            if (theNextBPMTime >= theAudioSource.clip.length)
            {
                theGameManager.gameState = "Won";
                isActive = false;
                theNextBPMTime = 0f;
                theAudioSource.Stop();
                Debug.Log(theActualSong + " song ended.");
            }

        }
    }

    IEnumerator Beat()
    {
        beat = true;
        yield return new WaitForSeconds(theWaitRhythm / 2);
        beat = false;
    }

    public void SetSongData(string songName)
    {
        isActive = false;
        isReady = false;
        isProReady = false;

        theActualSong = songName;

        try
        {
            theAudioSource.clip = theSongList.transform.Find(songName).GetComponent<SongData2>().theAudioClip;
            theMinSensibility = theSongList.transform.Find(songName).GetComponent<SongData2>().theMinSensibility;
            theMaxSensibility = theSongList.transform.Find(songName).GetComponent<SongData2>().theMaxSensibility;
            theActualBPM = theSongList.transform.Find(songName).GetComponent<SongData2>().theBPM;
            theActualOffsetMS = theSongList.transform.Find(songName).GetComponent<SongData2>().theOffsetMS;

            theBPM = theActualBPM;
            theOffsetMS = theActualOffsetMS;

            theWaitRhythm2 = (theActualBPM / 60f);
            theWaitRhythm = (1f / theWaitRhythm2);
            theNextBPMTime = theWaitRhythm;

            if (theAudioSource.clip != null)
            {
                isActive = true;
                Debug.Log(songName + " song started.");
                theAudioSource.Play();
            }
            else
            {
                Debug.LogWarning(songName + " doesn't have an Audio Clip to Play!");
            }
        }
        catch
        {
            Debug.LogWarning("The song: " + theActualSong + "doesn't exist in the list.");
            enabled = false;
        }
    }

    public void UpdateBPMOffSet()
    {
        isMsUpdated = false;

        theWaitRhythm2 = (theActualBPM / 60f);
        theWaitRhythm = (1f / theWaitRhythm2);

        theActualOffsetMS = Mathf.Abs(theOffsetMS);
        theActualBPM = theBPM;

        Debug.Log("BPM and Off-set updated ");
    }

    void LerpToInitialScale()
    {
        foreach (GameObject i in theRhythmicObjects)
        {
            i.transform.localScale = Vector3.Lerp(i.transform.localScale, i.GetComponent<RhythmicObject2>().theInitialScale, Time.deltaTime * 10f);
        }
    }

    void SetXScale()
    {
        foreach(GameObject i in theRhythmicObjects)
        {
            Vector3 newScale = i.transform.localScale;
            if(!isProReady)
            {
                newScale.x = theMinExtraXScale + i.GetComponent<RhythmicObject2>().theInitialScale.x;
            }
            else
            {
                newScale.x = theMaxExtraXScale + i.GetComponent<RhythmicObject2>().theInitialScale.x;
            }

            i.transform.localScale = newScale;
        }
    }

    void SetYScale()
    {
        foreach (GameObject i in theRhythmicObjects)
        {
            Vector3 newScale = i.transform.localScale;
            if (!isProReady)
            {
                newScale.y = theMinExtraYScale + i.GetComponent<RhythmicObject2>().theInitialScale.y;
            }
            else
            {
                newScale.y = theMaxExtraYScale + i.GetComponent<RhythmicObject2>().theInitialScale.y;
            }

            i.transform.localScale = newScale;
        }
    }

    void SetZScale()
    {
        foreach (GameObject i in theRhythmicObjects)
        {
            Vector3 newScale = i.transform.localScale;
            if (!isProReady)
            {
                newScale.z = theMinExtraZScale + i.GetComponent<RhythmicObject2>().theInitialScale.z;
            }
            else
            {
                newScale.z = theMaxExtraZScale + i.GetComponent<RhythmicObject2>().theInitialScale.z;
            }

            i.transform.localScale = newScale;
        }
    }

    void SetPrivateScale()
    {
        foreach (GameObject i in theRhythmicObjects)
        {
            if(!isProReady)
            {
                i.GetComponent<RhythmicObject2>().LowSensibility();
            }
            else
            {
                i.GetComponent<RhythmicObject2>().HighSensibility();
            }
        }
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for(int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if(i == 7)
            {
                sampleCount += 2;
            }

            for(int j = 0; j < sampleCount; j++)
            {
                average += theSamples[count] * (count + 1);
                count++;
            }
            average /= count;

            theBands[i] = average * 10;
        }
    }

    void BandBuffer()
    {
        for(int i = 0; i < 8; i++)
        {
            if(theBands[i] > theBandBuffer[i])
            {
                theBandBuffer[i] = theBands[i];
                theBufferDecrease[i] = 0.005f;
            }
            if(theBands[i] < theBandBuffer[i])
            {
                theBandBuffer[i] -= theBufferDecrease[i];
                theBufferDecrease[i] *= 1.2f;
            }
        }
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if(theBands[i] > theBandMax[i])
            {
                theBandMax[i] = theBands[i];
            }
            theAudioBands[i] = theBands[i] / theBandMax[i];
            theAudioBandBuffer[i] = theBandBuffer[i] / theBandMax[i];
        }
    }

    void GetAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;
        for(int i = 0; i < 8; i++)
        {
            currentAmplitude += theAudioBands[i];
            currentAmplitudeBuffer += theAudioBandBuffer[i];
        }
        if(currentAmplitude > theAmplitudeMax)
        {
            theAmplitudeMax = currentAmplitude;
        }
        theAmplitude = currentAmplitude / theAmplitudeMax;
        theAmplitudeBuffer = currentAmplitudeBuffer / theAmplitudeMax;
    }
}
