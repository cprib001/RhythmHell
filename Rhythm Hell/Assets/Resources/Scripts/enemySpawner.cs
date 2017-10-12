using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemySpawner : MonoBehaviour
{

    //public GameObject gameManager;
    public GameObject player;
    public enemyPooler ePool;
    public bool ranOnce = false;
    public List<GameObject> objPool = new List<GameObject>();
    public bool currentlyspawning = false;
    private float spawnTime = 1f;
    private bool stopSpawning = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlyspawning)
        {
            StartCoroutine(spawnObj(Random.Range(-110, 110), Random.Range(1, 10), Random.Range(-82, 140)));
        }
    }

    IEnumerator spawnObj(float X, float Y, float Z)
    {
        if (!stopSpawning)
        {
            //ePool = (enemyPooler)gameManager.GetComponent(typeof(enemyPooler));
            //int enemies = 0;
            //while (enemies <= num)
            //{
            currentlyspawning = true;


            GameObject newEnemy = ePool.GetPooledObject();
            newEnemy.gameObject.transform.position = new Vector3(X, Y, Z);
            //GameObject obj = (GameObject)Instantiate(newEnemy);
            //objPool.Add(obj);
            newEnemy.SetActive(true);
            // enemies++;
            //}
            /*
            for (int i = 0; i < objPool.Count; i++)
            {
                objPool[i].SetActive(true);
            }
            */
            yield return new WaitForSeconds(spawnTime);
            currentlyspawning = false;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 40)
        {
            stopSpawning = true;
        }
        else if (enemies.Length > 30)
        {
            stopSpawning = false;
            spawnTime = 6f;
        }
        else if (enemies.Length > 20)
        {
            stopSpawning = false;
            spawnTime = 4f;
        }
        else
        {
            stopSpawning = false;
            spawnTime = 2f;
        }
    }
}