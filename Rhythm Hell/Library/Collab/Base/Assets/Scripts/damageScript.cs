using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageScript : MonoBehaviour {
    public float health = 2;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (health == 1) {
            gameObject.GetComponent.<pCube2>().materials[0] = newMat;
        }

        if (health == 0)
            gameObject.SetActive(false);
	}

    void OnEnable()
    {
        health = 2;
    }
    
    void Damage(float pew)
    {
        health -= pew;
    }
}
