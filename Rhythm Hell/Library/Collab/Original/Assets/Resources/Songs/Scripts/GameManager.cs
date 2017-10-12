using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public string gameState;

    public RhythmicObjects2 rhythmObj;
    public Renderer playerRend1;
    public Renderer playerRend2; 
    public Renderer playerRend3;
    public Renderer playerRend4;
    public Renderer playerRend5;

    public PlayerInput input;
    public PlayerAttributes attributes;
    public GameObject thePlayer;
    public Image theImage;
    public AudioSource music;
    public float timeYellow = 1f;
    public bool testYellow = false;

    private bool gameEnded = false;
    private float yellowTime = 0f;
    private bool isYellow = false;
    private Color playerOriginalColor;

    public int currentScore;

    public GameObject pauseMenu;

    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    private bool paused;
    public bool Paused
    {
        get { return paused; }
    }

    private void Start()
    {
        gameState = "Menu";
        if (input != null)
            input.onPause += OnPause;
        instance = this.gameObject.GetComponent<GameManager>();
        ResumeGame();
    }

    private void Awake()
    {
        playerOriginalColor = playerRend1.material.color;
    }

    private void Update()
    {
        theImage.fillAmount = thePlayer.GetComponent<damageScript>().health / 10;
        //Handle color flashing on amplitude
        Color finalColor;

        float newEmissionAmount = rhythmObj.theAmplitudeBuffer - 0.5f;

        finalColor = playerRend1.material.color * Mathf.LinearToGammaSpace(newEmissionAmount);
        playerRend1.material.SetColor("_EmissionColor", finalColor);

        finalColor = playerRend2.material.color * Mathf.LinearToGammaSpace(newEmissionAmount);
        playerRend2.material.SetColor("_EmissionColor", finalColor);

        finalColor = playerRend3.material.color * Mathf.LinearToGammaSpace(newEmissionAmount);
        playerRend3.material.SetColor("_EmissionColor", finalColor);

        finalColor = playerRend4.material.color * Mathf.LinearToGammaSpace(newEmissionAmount);
        playerRend4.material.SetColor("_EmissionColor", finalColor);

        finalColor = playerRend5.material.color * Mathf.LinearToGammaSpace(newEmissionAmount);
        playerRend5.material.SetColor("_EmissionColor", finalColor);

        //Handle yellow coloring on missbeat
        yellowTime = Mathf.Clamp(yellowTime - Time.deltaTime, 0f, 1f);
        if (yellowTime == 0f && isYellow)
        {
            playerRend1.material.color = playerOriginalColor;
            playerRend2.material.color = playerOriginalColor;
            playerRend3.material.color = playerOriginalColor;
            playerRend4.material.color = playerOriginalColor;
            playerRend5.material.color = playerOriginalColor;

            attributes.canShoot = true;
            isYellow = false;
        }

        if (gameState == "Game" && (testYellow || (Input.GetMouseButtonDown(0) && attributes.canShoot && !rhythmObj.beat)))
        {
            attributes.canShoot = false;

            playerRend1.material.color = Color.yellow;
            playerRend2.material.color = Color.yellow;
            playerRend3.material.color = Color.yellow;
            playerRend4.material.color = Color.yellow;
            playerRend5.material.color = Color.yellow;

            yellowTime = timeYellow;
            testYellow = false;
            isYellow = true;
        }
    }

    private void OnPause()
    {
        if (gameState == "Menu")
            return;
        if (gameEnded)
            return;

        if (Paused)
            ResumeGame();
        else
            PauseGame();
    }

    public void ResumeGame()
    {
        if (gameState == "Menu")
            return;
        if (gameState == "Game")
            pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        paused = false;
        music.UnPause();
    }

    public void PauseGame()
    {
        if (gameState == "Menu")
            return;
        if(gameState == "Game")
            pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        paused = true;
        music.Pause();
    }

    public void resumePause()
    {
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        paused = true;
        gameEnded = true;
        music.Stop();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
