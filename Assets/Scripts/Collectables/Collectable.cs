using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
    public enum CollectableType
    {
        CoinSmall = 0,
        CoinMedium = 1,
        Chest = 2
    }

    private const string dropAnimationName = "Drop";    
    private const string collectedAnimationName = "Collected";    

    [Header("References")]
    [SerializeField] private Animator animator;

    [Header("Properties")]
    public CollectableType type;
    public int value = 0;

    [Header("Animation")]
    public float thresholdAnimationDistance = 0.1f;
    public float followPlayerSpeed = 1f;

    private bool used = false;

    public void InitCollectable(int _value)
    {
        animator.StopPlayback();
        transform.localScale = Vector3.one;
        GetComponent<Collider2D>().enabled = false;
        used = false;
        value = _value;
        animator.Play(dropAnimationName);
        float triggerDelay = animator.GetCurrentAnimatorStateInfo(0).length * 0.5f;
        StartCoroutine(EnableTrigger(triggerDelay));
    }

    public int Collect()
    {
        if (used) return 0;

        used = true;
        DespawnCollectable();
        return value;
    }

    private void DespawnCollectable()
    {
        animator.Play(collectedAnimationName);
        GetComponent<Collider2D>().enabled = false;        
        Lean.Pool.LeanPool.Despawn(gameObject, 1.5f);
    }

    private IEnumerator EnableTrigger(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider2D>().enabled = true;
    }

    /*
    private IEnumerator FollowPlayer(Transform target)
    {
        Vector3 velocity = Vector3.zero;
        while (Vector3Distance(target.transform.position, transform.position) > thresholdAnimationDistance)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, Time.deltaTime * followPlayerSpeed);
            yield return null;
        }

        DespawnCollectable();
    }

    private float Vector3Distance(Vector3 posA, Vector3 posB)
    {
        return Mathf.Sqrt((posA - posB).sqrMagnitude);
    }
    */
}
