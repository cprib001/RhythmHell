using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAIgun : MonoBehaviour {

    public float speed = 10f;
    public float bulletspeed = 40f;
    public Transform player;
    public bullet bullets;
    public bool beats;
    public bool done;
    public float attack1Range = 1f;
    public int attack1Damage = 1;
    public float timeBetweenAttacks;
    bool atPoint = false;
    int odd;
    Vector3 endpos;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        bullets = FindObjectOfType<bullet>();
        endpos = new Vector3(Random.Range(-110, 110), Random.Range(1, 10), Random.Range(-82, 140));
    }
	
	// Update is called once per frame
	void Update () {
        beats = FindObjectOfType<RhythmicObjects2>().beat;
        if (!atPoint)
        {
            transform.LookAt(endpos);

            if (Vector3.Distance(transform.position, endpos) >= 1)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else { atPoint = true; }
            if (beats && !done && (odd % 2 == 0))
            {
                shoot();
                done = true;
                odd++;
            }
            else if (!beats)
            {
                done = false;
                odd++;
            }
            odd %= 2;       

        }
        else
        {
            endpos = new Vector3(Random.Range(-110, 110), Random.Range(1, 10), Random.Range(-82, 140));
            atPoint = false;
        }

    }

    public void shoot()
    {

        GameObject shot = bullets.GetPooledObject();
        shot.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
        shot.transform.LookAt(player.position);
        shot.GetComponent<Rigidbody>().velocity = shot.transform.forward * bulletspeed;
        shot.SetActive(true);
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
