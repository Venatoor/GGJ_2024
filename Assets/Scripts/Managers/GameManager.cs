using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    GameState gameState;
    bool isPaused;

    public enum GameState
    {
        UI, INGAME
    }

    private void OnLaunch()
    {

    }

    private void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
    private void ResetGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }
    private void OnGameDefeated()
    {

    }

    private void OnGameWon()
    {

    }

    private void StartGame()
    {

    }

    private void PrepareGame()
    {

    }
}
