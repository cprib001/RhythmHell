using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public RhythmicObjects2 theGameManager;
    public Text songName;
    public Text BPM;
    public Text artist;
    public int songIndex = 0;
    public SongData2[] songData;
    public Camera viewCamera;
    public GameObject thePlayer;
    public GameObject theMenu;
    public GameObject theCanvasMenu;
    public GameObject notMenu;
    public GameObject theEnemies;

    public GameObject youWon;
    public GameObject youLost;
    public Text score;

	// Use this for initialization
	void Start () {
        songData = FindObjectsOfType<SongData2>();
        theGameManager.GetComponent<LoadSongs2>().LoadSong(songData[songIndex].theSongName);
        viewCamera.GetComponent<CameraFollow>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        songName.text = "Song name: " + songData[songIndex].theSongName;
        BPM.text = "BPM: " + songData[songIndex].theBPM;
        artist.text = "Artist: " + songData[songIndex].theArtistName;
        score.text = "Score: " + GameManager.Instance.currentScore;

        if(GameManager.Instance.gameState == "Won")
        {
            Won();
        }
        if (GameManager.Instance.gameState == "Lost")
        {
            Lost();
        }
	}

    public void ClickLeft()
    {
        songIndex--;
        if(songIndex < 0)
        {
            songIndex = songData.Length - 1;
        }
        theGameManager.GetComponent<LoadSongs2>().LoadSong(songData[songIndex].theSongName);
    }

    public void ClickRight()
    {
        songIndex++;
        if(songIndex > songData.Length - 1)
        {
            songIndex = 0;
        }
        theGameManager.GetComponent<LoadSongs2>().LoadSong(songData[songIndex].theSongName);
    }

    
    public void Select()
    {
        GameManager.Instance.gameState = "Game";
        theMenu.SetActive(false);
        theCanvasMenu.SetActive(false);
        thePlayer.SetActive(true);
        theEnemies.SetActive(true);
        notMenu.SetActive(true);
        viewCamera.GetComponent<CameraFollow>().enabled = true;
        theGameManager.GetComponent<LoadSongs2>().LoadSong(songData[songIndex].theSongName);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.ResumeGame();
        //Cursor.visible = false;
    }

    public void Lost()
    {
        notMenu.SetActive(false);
        youLost.SetActive(true);
        GameManager.Instance.EndGame();
        Cursor.lockState = CursorLockMode.None;
    }

    public void Won()
    {
        notMenu.SetActive(false);
        youWon.SetActive(true);
        GameManager.Instance.EndGame();
        Cursor.lockState = CursorLockMode.None;
    }
}
