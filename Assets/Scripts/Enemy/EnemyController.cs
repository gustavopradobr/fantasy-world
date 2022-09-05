using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [System.Serializable]
    public class DropItem
    {
        public Collectable.CollectableType dropType;
        public int dropValue = 0;
    }

    public enum EnemyType
    {
        Wooden = 0,
        Archer = 1,
        Knight = 2
    }

    private const string idleAnimationName = "Idle";
    private const string attackAnimationName = "Attack";
    private const string walkAnimationName = "Walk";
    private const string hitAnimationName = "Hit";
    private const string dieAnimationName = "Die";

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Slider healthBar;
    private Rigidbody2D rigibody;
    private EnemyAI enemyAi;
    [HideInInspector] public PlayerController playerController;

    [Header("Properties")]
    public EnemyType enemyType = 0;
    [SerializeField] private float life = 100;
    public bool canReceiveDamage = true;
    [SerializeField] private LayerMask playerWeaponLayer;

    [Header("Movement")]
    [Range(0, 1f)][SerializeField] private float movementSpeed = 0.1f;
    //[Range(0, 25f)][SerializeField] private float walkAnimationSpeed = 10f;
    private bool movementEnabled = true;
    [HideInInspector] public Vector2 moveDir = new Vector2(0,0);

    [Header("Drop")]
    [SerializeField] private List<DropItem> dropItem = new List<DropItem>();

    [Header("Events")]
    [SerializeField] private UnityEvent dieEvent;
   
    private float initialLife;

    private void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();
        enemyAi = GetComponent<EnemyAI>();
        playerController = GameManager.Instance.playerController;
        initialLife = life;
        healthBar.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(enemyType == EnemyType.Wooden)
        {

        }
        else if(enemyType == EnemyType.Archer)
        {
            UpdateCharacterDirection();

        }
        else if(enemyType == EnemyType.Knight)
        {
            UpdateCharacterDirection();

        }
    }

    private void FixedUpdate()
    {
        if (!movementEnabled) return;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimationName) || animator.GetCurrentAnimatorStateInfo(0).IsName(dieAnimationName) || animator.GetCurrentAnimatorStateInfo(0).IsName(hitAnimationName))
            return;

        rigibody.MovePosition(rigibody.position + moveDir * movementSpeed);
    }

    public float AttackAnimation()
    {
        animator.Play(attackAnimationName);
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    private void UpdateCharacterDirection()
    {
        if (transform.position.x > playerController.transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (transform.position.x < playerController.transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canReceiveDamage || life <= 0)
            return;

        if((playerWeaponLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            canReceiveDamage = false;
           
            life -= PlayerWeapon.playerDamage;

            if(!healthBar.transform.parent.gameObject.activeSelf)
                healthBar.transform.parent.gameObject.SetActive(true);

            healthBar.value = life / initialLife;

            if (life <= 0)
                DestroyEnemy(true);
            else
            {
                animator.Play(hitAnimationName);
                StartCoroutine(WaitPlayerAttackFinish());
            }

            GameManager.Instance.audioManager.SwordHit();
        }
    }

    public void DestroyEnemy(bool reward)
    {
        if(enemyAi)
            enemyAi.CancelInvoke();

        healthBar.transform.parent.gameObject.SetActive(false);

        animator.Play(dieAnimationName);
        foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
            collider.enabled = false;

        if (TryGetComponent(out SpriteOrdering _spriteOrdering))
            _spriteOrdering.ResetOrdering();

        if(reward)
            DropItems();

        dieEvent.Invoke();

        enabled = false;
    }

    private void DropItems()
    {
        for(int i=0; i<dropItem.Count; i++)
        {
            GameObject newCollectable = null;

            if (dropItem[i].dropType == Collectable.CollectableType.CoinSmall)
                newCollectable = GameManager.Instance.poolCoinSmall.Spawn(null);
            else if (dropItem[i].dropType == Collectable.CollectableType.CoinMedium)
                newCollectable = GameManager.Instance.poolCoinMedium.Spawn(null);
            else if (dropItem[i].dropType == Collectable.CollectableType.Chest)
                newCollectable = GameManager.Instance.poolChest.Spawn(null);

            newCollectable.transform.position = transform.position;
            newCollectable.GetComponent<Collectable>().InitCollectable(dropItem[i].dropValue);
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            newCollectable.GetComponent<Rigidbody2D>().AddForce(randomDirection * newCollectable.GetComponent<Rigidbody2D>().mass * 10f, ForceMode2D.Impulse);
        }     
    }

    private WaitUntil waitPlayerFinishAttack = new WaitUntil(() => !PlayerWeapon.playerAttacking);
    private IEnumerator WaitPlayerAttackFinish()
    {
        yield return waitPlayerFinishAttack;
        canReceiveDamage = true;
    }

    public void CanReceiveDamage(bool enable)
    {
        canReceiveDamage = enable;
    }

}
