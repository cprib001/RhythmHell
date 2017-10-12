using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DisallowMultipleComponent]
[AddComponentMenu("Audio/Rhthmic Object 2")]
public class RhythmicObject2 : MonoBehaviour {

    public bool isActive = true;
    public Vector3 theInitialScale = Vector3.one;
    public Vector3 theMinExtraScale = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 theMaxExtraScale = new Vector3(0.5f, 0.5f, 0.5f);

    public void LowSensibility()
    {
        if(isActive)
        {
            transform.localScale = theInitialScale + theMinExtraScale;
        }
    }

	public void HighSensibility()
    {
        if(isActive)
        {
            transform.localScale = theInitialScale + theMaxExtraScale;
        }
    }

    #if UNITY_EDITOR
    public void InsertToBaseScript()
    {
        if(EditorApplication.isPlaying)
        {
            List<GameObject> theRhythmicObjects = FindObjectOfType<RhythmicObjects2>().theRhythmicObjects;
            if (theRhythmicObjects.Contains(gameObject))
            {
                Debug.LogWarning(gameObject.name + " already exists as Rhythmic Object");
            }
            else
            {
                int count = FindObjectOfType<RhythmicObjects2>().theRhythmicObjects.Count;
                FindObjectOfType<RhythmicObjects2>().theRhythmicObjects.Insert(count, gameObject);
                Debug.Log(gameObject.name + " has been added as Rhythmic Object");
            }
        }
        else
        {
            Debug.LogWarning("The Editor is not Playing");
        }
    }

    public void DeleteFromBaseScript()
    {
        if(EditorApplication.isPlaying)
        {
            FindObjectOfType<RhythmicObjects2>().theRhythmicObjects.Remove(gameObject);
            transform.localScale = theInitialScale;
            Debug.Log(gameObject.name + " has been deleted as Rhythmic Object");
        }
        else
        {
            Debug.LogWarning("The Editor is not Playing");
        }
    }

    void OnDestroy()
    {
        if(EditorApplication.isPlayingOrWillChangePlaymode)
        {
            DeleteFromBaseScript();
        }
    }

    #endif
}
