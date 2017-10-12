using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageScript : MonoBehaviour {
    public float health = 2;

    public Renderer[] enemyRend = null;
    public Renderer playerRend1 = null;
    public Renderer playerRend2 = null;
    public Renderer playerRend3 = null;
    public Renderer playerRend4 = null;
    public Renderer playerRend5 = null;

    public string materialPath;
    public bool isPlayer = false;
    public bool dealDamage = false;
    public float timeRed = 0.5f;

    private float redTime = 0f;
    private bool isRed = false;
    private Color playerOriginalColor = Color.black;
    private Material enemyOriginalMaterial = null;

    public GameObject thePlayer;
    public GameManager theGameManager;

    private void Awake()
    {
        if (isPlayer)
        {
            playerOriginalColor = playerRend1.material.color;
        }
        else
        {
            if(enemyRend == null)
            {
                Debug.Log("damageScript: Enemy renderer array is empty");
                return;
            }
            enemyOriginalMaterial = enemyRend[0].material;
        }
    }

    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerAttributes>().gameObject;
        theGameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        if (!isPlayer)
        {
            health = 2;
            foreach(Renderer rend in enemyRend)
            {
                rend.material = enemyOriginalMaterial;
            }
        }
    }

    private void Update()
    {
        if(isPlayer && health <= 0)
        {
            theGameManager.gameState = "Lost";
        }
        if (dealDamage)
        {
            Damage(1f);
            dealDamage = false;
        }
        if (isPlayer)
        {
            redTime = Mathf.Clamp(redTime - Time.deltaTime, 0f, 1f);
            if (redTime == 0f && isRed)
            {
                playerRend1.material.color = playerOriginalColor;
                playerRend2.material.color = playerOriginalColor;
                playerRend3.material.color = playerOriginalColor;
                playerRend4.material.color = playerOriginalColor;
                playerRend5.material.color = playerOriginalColor;
                isRed = false;
            }
        }
    }


    void Damage(float pew)
    {
        health -= pew;

        if(!isPlayer)
        {
            if (health == 1)
            {
                if (materialPath.Length == 0)
                {
                    Debug.Log("damageScript Must set material path");
                    return;
                }
                Material mat = Resources.Load(materialPath, typeof(Material)) as Material;
                if (mat == null)
                {
                    Debug.Log("damageScript Material Load Failed");
                    return;
                }
                foreach (Renderer rend in enemyRend)
                {
                    rend.material = mat;
                }
            }
            else if (health <= 0)
            {
                if (!(thePlayer.GetComponent<damageScript>().health >= 10))
                    thePlayer.GetComponent<damageScript>().health++;
                GameManager.Instance.currentScore++;
                gameObject.SetActive(false);
            }
        }
        else
        {
            playerRend1.material.color = Color.red;
            playerRend2.material.color = Color.red;
            playerRend3.material.color = Color.red;
            playerRend4.material.color = Color.red;
            playerRend5.material.color = Color.red;

            redTime = timeRed;
            isRed = true;
        }
    }
}
