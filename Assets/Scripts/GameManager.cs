using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
        {
            instance = this;
        }

    enum UiLevel
    {
        start,
        gameover,
        ingame,
        death
    };

    UiLevel uiLevel;

    [SerializeField]private GameObject player1;
    [SerializeField]private GameObject player2;
    [SerializeField]private CinemachineVirtualCamera vcam;

    public GameObject StartMenu;
    public GameObject InGameMenu;
    public GameObject GameoverMenu;
    public GameObject DeathMenu;

    public Sprite keySprite;
    public int numberKeys;

   
    public Transform[] playerSpawns;
    int levelStage;

    public bool playerDied;

    private void Start()
    {
        levelStage = 0;
        uiLevel = UiLevel.start;
        playerDied = false;
        //SpawnPlayer();
    }

    private void Update()
    {
        switch (uiLevel)
        {
            case UiLevel.start:
                StartMenu.SetActive(true);
                GameoverMenu.SetActive(false);
                InGameMenu.SetActive(false);
                DeathMenu.SetActive(false);
                break;
            case UiLevel.gameover:
                StartMenu.SetActive(false);
                GameoverMenu.SetActive(true);
                InGameMenu.SetActive(false);
                DeathMenu.SetActive(false);
                break;
            case UiLevel.ingame:
                StartMenu.SetActive(false);
                GameoverMenu.SetActive(false);
                InGameMenu.SetActive(true);
                DeathMenu.SetActive(false);
                break;
            case UiLevel.death:
                StartMenu.SetActive(false);
                GameoverMenu.SetActive(false);
                InGameMenu.SetActive(false);
                DeathMenu.SetActive(true);
                break;

        }
    }

    public void StartGame()
    {
        playerDied = false;
        uiLevel = UiLevel.ingame;
        levelStage = 0;
        //SpawnPlayer();
    }

    public void Restart()
    {
        playerDied=false;
        uiLevel = UiLevel.ingame;
        levelStage = 0;
       // SpawnPlayer();
    }
    public void GoToMenu()
    {
        uiLevel = UiLevel.start;
    }

    public void Death()
    {
        if (!playerDied)
        {
            playerDied = true;
            uiLevel = UiLevel.death;
        }
    }
    

    public void ChangeLevel()
    {
        levelStage++;
        if (levelStage >= playerSpawns.Length)
        {
            GameComplete();

        }
        else
        {
            //SpawnPlayer();
        }
    }

    public void GameComplete()
    {
        //When we dont have anywhere to teleport...
    }

    private void SpawnPlayer()
    {
        
        GameObject p1 = Instantiate(player1, new Vector2(playerSpawns[levelStage].position.x - 3f, playerSpawns[levelStage].position.y), playerSpawns[levelStage].rotation);
        GameObject p2 = Instantiate(player2, new Vector2(playerSpawns[levelStage].position.x - 3f, playerSpawns[levelStage].position.y), playerSpawns[levelStage].rotation);

        
        
    }


}
