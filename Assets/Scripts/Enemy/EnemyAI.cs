using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Lean.Pool.LeanGameObjectPool arrowPool;
    [SerializeField] private Transform arrowSpawnPoint;
    private EnemyController enemyController;

    [Header("Archer")]
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float arrowInterval;
    [SerializeField] private int arrowDamage;
    [HideInInspector] public bool playerInSight;

    [HideInInspector] public bool attacking = false;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();

        if (enemyController.enemyType == EnemyController.EnemyType.Archer)
            InvokeRepeating("ShotArrow", arrowInterval, arrowInterval);
    }

    private void Update()
    {
        if(enemyController.enemyType == EnemyController.EnemyType.Archer && playerInSight)
        {            
            arrowSpawnPoint.right = enemyController.playerController.transform.position - transform.position;
        }
    }

    private void ShotArrow()
    {
        if (!playerInSight)
            return;

        float delay = enemyController.AttackAnimation();
        StartCoroutine(SpawnArrowDelayed(delay));
    }

    private IEnumerator SpawnArrowDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject spawnedArrow = arrowPool.Spawn(arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        spawnedArrow.SetActive(true);
        spawnedArrow.GetComponent<EnemyArrow>().weaponDamage = arrowDamage;
        spawnedArrow.GetComponent<Rigidbody2D>().velocity = arrowSpawnPoint.right * arrowSpeed;
    }

    public void PlayerInEnemySight(bool inSight)
    {
        playerInSight = inSight;
    }
}
