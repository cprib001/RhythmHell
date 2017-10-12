using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour {

    public float damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            col.gameObject.GetComponent<damageScript>().dealDamage = true;
        }
        else if(col.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
    }
}
