using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    GameState gameState;
    bool isPaused;
    InputManager inputManager;

    public GameObject victory;
    public GameObject defeat;
    public GameObject Start;


    private int ballonNumbers;
    private LevelManager levelManager;
    private Stat laughingStat;

    public int currentBallons;

    private bool gameIsRuning ;

    //SFX
    public AudioSource randomLaughsSFX;
    public AudioSource backgroundMusic;
    private AudioManager audioManager;
    [SerializeField]

    public enum GameState
    {
        UI, INGAME
    }

    private void Awake()
    {

        DontDestroyOnLoad(this);

        InitializeGame();

        audioManager = FindObjectOfType<AudioManager>();
        Character player = FindObjectOfType<Character>();
        inputManager = player.GetComponent<InputManager>();
        laughingStat = player.GetComponent<LaughStat>();
        FetchBallonNumbers();
    }

    private void FetchBallonNumbers()
    {
        MainPickUps[] ballons = FindObjectsOfType<MainPickUps>();
        ballonNumbers = ballons.Length;
        print(ballonNumbers);
    }

    private void InitializeGame()
    {
        gameIsRuning = true;
        audioManager.playAudio(backgroundMusic);
        gameState = GameState.UI;
        PauseGame();
        ShowUi(Start);
    }

    private void Update()
    {
        if ( gameState == GameState.INGAME )
        {
            inputManager.HandlePauseInput(this);
        }
        HandleGameDefeatCondition();
        HandleGameWonCondition();

        Invoke("RandomLaughs", 90f);
    }

    public void SwitchGameManageState()
    {
        if (gameIsRuning)
        {
            switch (gameState)
            {
                case (GameState.INGAME):
                    ResetGame();
                    gameState = GameState.UI;
                    break;
                case (GameState.UI):
                    PauseGame();
                    gameState = GameState.INGAME;
                    break;
            }
        }
    }

    private void OnLaunch()
    {
        //Show UI 
        ShowUi(Start);
    }

    public void ShowUi(GameObject uiToShow) {
        DisableAllUi();
        uiToShow.SetActive(true);

    }
    public void DisableAllUi() {
        victory.SetActive(false);
        defeat.SetActive(false);
        Start.SetActive(false);
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    public bool GetPaused() {
        return isPaused;
    }

    private void ResetGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }
    private void HandleGameDefeatCondition()
    {
        if ( laughingStat.GetCurrentAmount() <= 0 && gameIsRuning )
        {
            ShowUi(defeat);
            EndGame();
        }
    }

    private void HandleGameWonCondition()
    {
        if (currentBallons == ballonNumbers && gameIsRuning )
        {
            ShowUi(victory);

            EndGame();
        }
    }


    private void EndGame()
    {
        gameIsRuning = false;
        PauseGame();
    }

    private void RandomLaughs()
    {
        AudioManager.instance.playAudio(randomLaughsSFX);
    }

    private void SetNumberOfBallons(int numberOfBallons)
    {
        ballonNumbers = numberOfBallons;
    }

    public void IncrementNumberOfBallons()
    {
        currentBallons++;
    }
}
