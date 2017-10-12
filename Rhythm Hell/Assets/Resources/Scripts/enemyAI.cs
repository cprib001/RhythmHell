using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour {

    public Transform target;
    public float speed = 0.1f;
    public float attack1Range = 1f;
    public int attack1Damage = 1;
    public float timeBetweenAttacks;

    // Use this for initialization
    void Start () {
        target = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(target.position);


        if (Vector3.Distance(transform.position, target.position) >= 1)
        {

            transform.position += transform.forward * speed * Time.deltaTime;



            if (Vector3.Distance(transform.position, target.position) <= 2)
            {
                
            }

        }
    }
}
