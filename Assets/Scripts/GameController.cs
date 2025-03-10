using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private bool isGamePaused = false;
    private bool isGameStarted = false;

    [SerializeField]
    private PlayerController playerController;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);

        inventoryPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    private void Update()
    {
        if (isGameStarted && Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryPanel();
        }
    }
    public bool IsGameActive()
    {
        return isGameStarted && !isGamePaused;
    }

    private void OnPlayButtonClicked()
    {
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            mainMenuPanel.SetActive(false);
            isGameStarted = true;
        }
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

    private void ToggleInventoryPanel()
    {
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        inventoryPanel.SetActive(true);
    }

    private void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        inventoryPanel.SetActive(false);

        playerController.UpdatePlayerStats();
     //   playerController.UpdateMovementSpeed();

    }
}
