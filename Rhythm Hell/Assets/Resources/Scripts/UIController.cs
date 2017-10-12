using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//FIXME: UIUpdate clean up

public class UIController : MonoBehaviour
{
    public PlayerAttributes attributes;
    public Text textTarget;
    public Text textHealth;
    public Text textReticle;
    public Text textCoins;
    public Text textEndGame;
    public Text textScore;
    public Color loseColor;
    public Color winColor;
    public GameObject player;
    public AudioSource music;
    public AudioSource loseSound;
    public AudioSource winSound;

    private int totalCoins;

    private void Start()
	{
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
		//DeactivateTargetName();
  //      OnHealthUpdate();
  //      OnCoinsUpdate();
  //      OnReticleUpdate();
  //      OnScoreUpdate();

        if (attributes != null)
        {
            //attributes.onHealthUpdate += OnHealthUpdate;
            //attributes.onCoinsUpdate += OnCoinsUpdate;
            //attributes.onReticleUpdate += OnReticleUpdate;
            //attributes.onScoreUpdate += OnScoreUpdate;
        }
    }

 //   private void OnHealthUpdate()
 //   {
 //       textHealth.text = "Health: " + player.GetComponent<PlayerAttributes>().healthCurrent + "/" + player.GetComponent<PlayerAttributes>().totalHealth;

 //       if (player.GetComponent<PlayerAttributes>().healthCurrent <= 0)
 //       {
 //           textEndGame.text = "You Lose";
 //           textEndGame.color = loseColor;
 //           textEndGame.rectTransform.localPosition = new Vector3(0f, 112.5f, 0f);
 //           music.Stop();
 //           loseSound.Play();
 //           textScore.gameObject.SetActive(false);
 //           GameManager.Instance.EndGame();
 //       }
 //   }

 //   private void OnCoinsUpdate()
 //   {
 //       textCoins.text = "Coins: " + player.GetComponent<PlayerAttributes>().numCoins + "/" + totalCoins;

 //       if (player.GetComponent<PlayerAttributes>().numCoins >= totalCoins)
 //       {
 //           textEndGame.text = "You Win!";
 //           textScore.rectTransform.localPosition = new Vector3(0f, 112.5f, 0f);
 //           textEndGame.color = winColor;
 //           music.Stop();
 //           winSound.Play();
 //           GameManager.Instance.EndGame();
 //       }
 //   }

 //   private void OnReticleUpdate()
 //   {
 //       if (player.GetComponent<PlayerAttributes>().timeUntilCharged <= 0f)
 //           textReticle.text = "+";
 //       else
 //           textReticle.text = "";
 //   }

 //   private void OnScoreUpdate()
 //   {
 //       textScore.text = "Score: " + player.GetComponent<PlayerAttributes>().currentScore;
 //   }

 //   public void ActivateTargetName(string itemName)
	//{
	//	textTarget.text = itemName + "\n[E]";
	//}

 //   public void ActivateTargetUnableName(string unableName)
 //   {
 //       textTarget.text = unableName;
 //   }

 //   public void DeactivateTargetName()
	//{
	//	textTarget.text = "";
	//}
}
