using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static int goldCoins { get { return Instance.gameData.goldCoins; } set { Instance.gameData.goldCoins = value; Instance.OnCurrencyChange(); } }
    public static bool paused = false;
    public static bool pauseEnabled = true;
    public static bool restartingGame = false;

    [Header("References")]
    public PlayerController playerController;
    public CameraController cameraController;
    public UIManager uiManager;
    public CheckpointManager checkpointManager;
    [SerializeField] private ShopManager shopManager;
    public GameData gameData;

    [Header("Canvas")]
    [SerializeField] private Canvas hudCanvas;

    [Header("Coin Pool")]
    public Lean.Pool.LeanGameObjectPool poolCoinSmall;
    public Lean.Pool.LeanGameObjectPool poolCoinMedium;
    public Lean.Pool.LeanGameObjectPool poolChest;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        if (restartingGame)
        {
            playerController.transform.position = checkpointManager.RecoverCheckpointPosition(gameData.checkpoint);
            restartingGame = false;
        }
    }
    public void PauseGame(bool pause)
    {
        if (!pauseEnabled) return;

        if (pause && !paused)
        {
            paused = true;
            Time.timeScale = 0;
            uiManager.PauseGame(true);
        }
        else if(!pause && paused)
        {
            paused = false;
            Time.timeScale = 1;
            uiManager.PauseGame(false);
        }
    }

    public void OnCurrencyChange()
    {
        gameData.SaveGameData();
        uiManager.UpdateCurrency();
    }

    public static void AddCoin(int value)
    {
        goldCoins += value;
    }

    public void OpenShop(bool open)
    {
        shopManager.OpenShop(open);
        playerController.movementEnabled = !open;
        uiManager.OpenShop(open);
        pauseEnabled = !open;
        cameraController.EnableShopCamera(open);
    }

    public void EndGame()
    {
        uiManager.EndGameScreen();
    }

    public void GoToMenu()
    {
        gameData.SaveGameData();
        SceneManager.LoadScene(0);
    }
    public void RestartGame()
    {
        restartingGame = true;
        gameData.SaveGameData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        gameData.SaveGameData();
    }
}
