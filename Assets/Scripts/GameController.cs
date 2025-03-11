using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [Header("Panels references")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject hudPanel;

    [Header("Buttons references")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private bool isGamePaused = false;
    private bool isGameStarted = false;

    [Space]
    [SerializeField]
    private PlayerController playerController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);

        inventoryPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        hudPanel.SetActive(false);
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
            hudPanel.SetActive(true);
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
        hudPanel.SetActive(false);
    }

    private void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        inventoryPanel.SetActive(false);
        hudPanel.SetActive(true);
        playerController.UpdatePlayerStats(); 
        playerController.StopPlayerMovementAnim();
    }
}
