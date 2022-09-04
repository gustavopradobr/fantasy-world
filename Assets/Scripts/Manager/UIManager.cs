using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Text goldText;

    [Header("Health")]
    [SerializeField] private Slider healthBar;

    [Header("Pause")]
    [SerializeField] private UnityEvent pauseGame;
    [SerializeField] private UnityEvent unpauseGame;

    [Header("Shop")]
    [SerializeField] private UnityEvent openShop;
    [SerializeField] private UnityEvent closeShop;

    [Header("EndGame")]
    [SerializeField] private UnityEvent endGame;


    private void Start()
    {
        UpdateCurrency();
    }
    public void UpdateCurrency()
    {
        goldText.text = GameManager.goldCoins.ToString();
    }

    public void UpdateHealthBar(float actualLife, float maxLife)
    {
        healthBar.value = actualLife / maxLife;
    }

    public void PauseGame(bool pause)
    {
        if (pause)
            pauseGame.Invoke();
        else
            unpauseGame.Invoke();
    }

    public void OpenShop(bool open)
    {
        if (open)
            openShop.Invoke();
        else
            closeShop.Invoke();
    }

    public void EndGameScreen()
    {
        endGame.Invoke();
    }
}
