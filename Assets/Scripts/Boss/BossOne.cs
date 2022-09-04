using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossOne : MonoBehaviour
{
    [SerializeField] private int requiredDefeatedEnemies = 0;
    [SerializeField] private UnityEvent bossDefeated;
    private int defeatedEnemies = 0;

    public void AddDefeatCounter()
    {
        defeatedEnemies++;

        CheckObjective();
    }

    private void CheckObjective()
    {
        if (defeatedEnemies >= requiredDefeatedEnemies)
            bossDefeated.Invoke();
    }
}
