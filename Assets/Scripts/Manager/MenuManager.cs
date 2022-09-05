using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("References")]
    public GameData gameData;
    public Animator playerAnimator;

    private WaitForSecondsRealtime waitAnimation = new WaitForSecondsRealtime(2.5f);

    private void Awake()
    {
        Time.timeScale = 1f;
    }
    private IEnumerator Start()
    {
        while (true)
        {
            yield return waitAnimation;

            float time = 0;
            float duration = 5f;

            while (time < duration)
            {
                playerAnimator.SetFloat("MoveSpeed", Mathf.Lerp(0.2f, 2.5f, time / duration));
                time += Time.unscaledDeltaTime;
                yield return null;
            }

            int random = Random.Range(0, 3);
            if (random == 0){
                playerAnimator.SetBool("Attack", true);
                playerAnimator.SetFloat("MoveSpeed", 0);
                yield return waitAnimation;
                continue;
            }
            else if (random == 1){
                playerAnimator.SetBool("Attack2", true);
                playerAnimator.SetFloat("MoveSpeed", 0);
                yield return waitAnimation;
                continue;
            }

            time = 0;
            duration = 5f;

            while (time < duration)
            {
                playerAnimator.SetFloat("MoveSpeed", Mathf.Lerp(2.5f, 0, time / duration));
                time += Time.unscaledDeltaTime;
                yield return null;
            }

            yield return waitAnimation;
        }
    }

    public void StartGame()
    {
        StopAllCoroutines();
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
