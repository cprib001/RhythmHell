using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour {
    //public int healthChange = 0;
    //public int healthCurrent;
    //public int totalHealth;
    //public float timeUntilCharged;
    //public float rechargeTime;
    public bool canJump;
    public bool canShoot = true;
    //public int numCoinsChange = 0;
    //public int numCoins;
    //public int currentScore;
    //public int maxScore;

    //private float scoreTimer = 0f;

    /*
    public delegate void OnHealthUpdate();
    public OnHealthUpdate onHealthUpdate;

    public delegate void OnCoinsUpdate();
    public OnCoinsUpdate onCoinsUpdate;

    public delegate void OnScoreUpdate();
    public OnScoreUpdate onScoreUpdate;

    public delegate void OnReticleUpdate();
    public OnReticleUpdate onReticleUpdate;
    */

    private void Update()
    {
        if (GameManager.Instance.Paused)
            return;

        //GetHealthUpdate();
        //GetCoinsUpdate();
        //GetScoreUpdate();
        //GetReticleUpdate();
    }

    //private void GetHealthUpdate()
    //{
    //    if (healthChange == 0)
    //        return;

    //    healthCurrent = Mathf.Clamp(healthCurrent + healthChange, 0, totalHealth);
    //    healthChange = 0;

    //    if (onHealthUpdate != null)
    //        onHealthUpdate();
    //}

    //private void GetCoinsUpdate()
    //{
    //    if (numCoinsChange == 0)
    //        return;

    //    numCoins += numCoinsChange;
    //    numCoinsChange = 0;

    //    if (onCoinsUpdate != null)
    //        onCoinsUpdate();
    //}

    //private void GetScoreUpdate()
    //{
    //    scoreTimer += Time.deltaTime;
    //    if (scoreTimer < 1f)
    //        return;

    //    scoreTimer = 0f;
    //    if(currentScore > 0)
    //        currentScore--;

    //    if (onScoreUpdate != null)
    //        onScoreUpdate();
    //}

    //private void GetReticleUpdate()
    //{
    //    timeUntilCharged = Mathf.Clamp(timeUntilCharged - Time.deltaTime, 0f, rechargeTime);

    //    if (onReticleUpdate != null)
    //        onReticleUpdate();
    //}
}
