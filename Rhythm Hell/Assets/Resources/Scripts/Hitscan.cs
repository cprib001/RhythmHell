using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : MonoBehaviour {

    public GameObject line;
    LineRenderer test;
    public Camera viewCamera;
    public LayerMask mask;

    public float damage;

    public RhythmicObjects2 gameManager;
    public PlayerAttributes attributes;

    void Start()
    {
        test = line.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (GameManager.Instance.Paused)
            return;
        test.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z));
        if (Input.GetMouseButtonDown(0) && attributes.canShoot)
        {
            StartCoroutine(Shoot());
        }
    }
    
    IEnumerator Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(viewCamera.transform.position, viewCamera.transform.forward, out hit, float.MaxValue, mask) && gameManager.beat)
        {
            test.SetPosition(1, hit.point);
            test.enabled = true;

            hit.collider.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        }
        yield return new WaitForSeconds(0.1f);
        test.enabled = false;
    }
}
