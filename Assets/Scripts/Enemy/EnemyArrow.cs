using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public int weaponDamage = 0;
    public int arrowTimeoutTime = 15;
    private WaitForSeconds arrowTimeout = new WaitForSeconds(15f);

    private void Start()
    {
        arrowTimeout = new WaitForSeconds(arrowTimeoutTime);
    }

    private void OnEnable()
    {
        StartCoroutine(ArrowTimeout());   
    }

    public void DespawnArrow()
    {
        weaponDamage = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Lean.Pool.LeanPool.Despawn(gameObject);
    }

    private IEnumerator ArrowTimeout()
    {
        yield return arrowTimeout;
        DespawnArrow();
    }
}
