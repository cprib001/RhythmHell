using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public Transform target;
    public float timeBetweenAttacks;

    // Use this for initialization
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
    }
}
