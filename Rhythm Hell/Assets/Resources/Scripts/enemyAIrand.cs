using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAIrand : MonoBehaviour {

    public Transform target;
    public float speed = 0.1f;
    public float attack1Range = 1f;
    public int attack1Damage = 1;
    public float timeBetweenAttacks;
    bool toStart = false;
    Vector3 endpos;

    // Use this for initialization
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        endpos = new Vector3(Random.Range(-110, 110), Random.Range(1, 10), Random.Range(-82, 140));
    }

    // Update is called once per frame
    void Update()
    {
        if(!toStart)
        {
            transform.LookAt(endpos);

            if (Vector3.Distance(transform.position, endpos) >= 1)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else { toStart = true;  }
        }

        if (toStart) { 
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
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            col.gameObject.GetComponent<damageScript>().dealDamage = true;
        }
    }
}
