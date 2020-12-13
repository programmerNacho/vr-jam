using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public bool IsGameFinished { get; private set; } = false;
    public bool IsGamePaused { get; private set; } = false;

    public UnityEvent OnGameWin = new UnityEvent();
    public UnityEvent OnGameLost = new UnityEvent();

    private void Start()
    {
        IsGameFinished = false;
        UnPauseGame();
    }

    public void Won()
    {
        IsGameFinished = true;
        OnGameWin.Invoke();
    }

    public void Lost()
    {
        IsGameFinished = true;
        OnGameLost.Invoke();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
