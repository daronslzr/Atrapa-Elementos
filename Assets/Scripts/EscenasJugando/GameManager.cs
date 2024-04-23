using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState {Ready, Counting, Playing, Ended};

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState gameState = GameState.Ready;
    public RawImage background;
    public float parallaxSpeed = 0.07f;
    public GameObject uiReady;

    public GameObject uiCountDown;
    public float timeLeft = 3.0f;
    public Text countDownText;

    public GameObject uiWinLose;
    public Text winLoseText;

    public GameObject uiLives;
    public Text livesText;

    public GameObject player;
    public int counterLose = 3;
    public int counterWin = 0;

    public string difficulty; 

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player.SetActive(false);
        uiWinLose.SetActive(false);
        uiLives.SetActive(false);
        difficulty = PlayerPrefs.GetString("SceneName");
    }

    void Update()
    {
        bool action = Input.GetKeyDown("space");
        UpdateGameState(action);
        StartCountDown();
        UpdateParallax();
        HandleWinLose();
        HandleExit();
    }

    void UpdateParallax()
    {
        if(gameState == GameState.Playing)
        {
            float finalSpeed = parallaxSpeed * Time.deltaTime;
            background.uvRect = new Rect(0f, background.uvRect.y + finalSpeed, 1f, 1f);
        }

    }

    void UpdateGameState(bool action)
    {
        if(gameState == GameState.Ready && action)
        {
            //Cambiamos el estado a counting para que comience el countdown
            gameState = GameState.Counting;
            uiReady.SetActive(false);
            uiCountDown.SetActive(true);
        }
        else if (gameState == GameState.Ended && action)
        {
            HandleRestart();
        }

    }

    //Con este metodo habilitamos el count down
    void StartCountDown()
    {
        if (gameState == GameState.Counting)
        {
            timeLeft -= Time.deltaTime;
            //Mostramos los segundos restantes en pantalla
            countDownText.text = (timeLeft).ToString("0");
            //Cuando pasan los 3 segundos cambiara el estado del juego
            if (timeLeft < 0)
            {
                gameState = GameState.Playing;
                AudioManager.Instance.musicPlayer.Play();
                uiCountDown.SetActive(false);
                player.SetActive(true);
                uiLives.SetActive(true);
                //Comienza la generacion de falling objects
                SpawnManager.Instance.StartSpawn();
            }
        }
    }

    //Cambia el estado del juego a Ended cuando el counter llega a 0
    void HandleWinLose()
    {
        if((gameState == GameState.Playing) && (counterLose == 0 || counterWin == 3))
        {
            gameState = GameState.Ended;
            //Para la musica de fondo
            AudioManager.Instance.StopMusic();
            //Para la generacion de falling objects
            SpawnManager.Instance.StopSpawn();
            ChangeAnimationAndWinLoseText();
        }
    }

    void ChangeAnimationAndWinLoseText()
    {
        //Checamos si el jugador perdio
        if (counterLose == 0)
        {
            winLoseText.text = "Has perdido!";
            //PlayerManager.Instance.SetAnimation(PlayerManager.Instance.animacionesDisponibles[3].name);
            PlayerManager.Instance.SetAnimation(3);
            //Reproduce la musica de victoria
            AudioManager.Instance.PlaySound("Die");
        }
        else
        {
            winLoseText.text = "Has ganado!";
            //PlayerManager.Instance.SetAnimation(PlayerManager.Instance.animacionesDisponibles[4].name);
            PlayerManager.Instance.SetAnimation(4);
            //Reproduce la musica de muerte
            AudioManager.Instance.PlaySound("Victory");
        }
        uiWinLose.SetActive(true);
    }

    void HandleRestart()
    {
        SceneManager.LoadScene("Menu");
    }

    void HandleExit()
    {
        if (Input.GetKeyDown("escape")) Application.Quit();
    }
}

