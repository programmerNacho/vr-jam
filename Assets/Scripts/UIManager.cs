using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu = null;
    [SerializeField]
    private GameObject winMenu = null;
    [SerializeField]
    private GameObject loseMenu = null;
    [SerializeField]
    private PlayerInput playerInput = null;
    [SerializeField]
    private PlayerModeManager playerModeManager = null;
    [SerializeField]
    private Camera vrCamera = null;
    [SerializeField]
    private Vector3 uiOffsetFromPlayer = new Vector3(0f, 0.75f, 1);

    private Transform uiParent = null;
    private Transform cameraTransform = null;
    private GameManager gameManager = null;

    private void Awake()
    {
        uiParent = pauseMenu.transform.parent;
        cameraTransform = vrCamera.transform;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        HideAllUI();
    }

    private void HideAllUI()
    {
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
    }

    private void OnEnable()
    {
        playerInput.OnPauseActionPressed.AddListener(PauseActionPressed);

        gameManager.OnGameWin.AddListener(OpenWinMenu);
        gameManager.OnGameLost.AddListener(OpenLoseMenu);
    }

    private void OnDisable()
    {
        playerInput.OnPauseActionPressed.RemoveListener(PauseActionPressed);

        gameManager.OnGameWin.RemoveListener(OpenWinMenu);
        gameManager.OnGameLost.RemoveListener(OpenLoseMenu);
    }

    private void PauseActionPressed()
    {
        if(!gameManager.IsGameFinished)
        {
            if(!pauseMenu.activeInHierarchy)
            {
                OpenPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        PlaceUI();
        playerModeManager.EnterUIMode();
        gameManager.PauseGame();
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        playerModeManager.EnterCombatMode();
        gameManager.UnPauseGame();
    }

    public void OpenWinMenu()
    {
        pauseMenu.SetActive(false);
        winMenu.SetActive(true);
        loseMenu.SetActive(false);
        PlaceUI();
        playerModeManager.EnterUIMode();
        gameManager.PauseGame();
    }

    public void OpenLoseMenu()
    {
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(true);
        PlaceUI();
        playerModeManager.EnterUIMode();
        gameManager.PauseGame();
    }

    private void PlaceUI()
    {
        uiParent.position = cameraTransform.position + (Vector3.Scale(cameraTransform.forward, new Vector3(1f, 0f, 1f)).normalized * uiOffsetFromPlayer.z + cameraTransform.right * uiOffsetFromPlayer.x + cameraTransform.up * uiOffsetFromPlayer.y);
        Vector3 forwardUIParent = cameraTransform.position - uiParent.position;
        forwardUIParent.y = 0f;
        forwardUIParent.Normalize();
        uiParent.forward = -forwardUIParent;
    }

    public void ResumeButtonPressed()
    {
        ClosePauseMenu();
    }

    public void RestartButtonPressed()
    {
        gameManager.RestartGame();
    }

    public void MainMenuButtonPressed()
    {
        gameManager.ExitToMenu();
    }
}
