using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("References")]
    public GameData gameData;

    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetGameData()
    {
        gameData.ResetGameData();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    private void OnApplicationQuit()
    {
        gameData.SaveGameData();
    }
}
