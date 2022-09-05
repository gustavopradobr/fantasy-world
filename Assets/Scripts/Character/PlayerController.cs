using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NormalInput))]
public class PlayerController : MonoBehaviour {

    private NormalInput input;
    private Vector2 moveDir { get { return input.movement; } }
    private Rigidbody2D m_rigidbody;
    private PlayerWeapon playerWeapon;

    [Header("References")]
    [SerializeField] private Animator m_Animator;
    public ClothChanger clothChanger;

    [Header("Properties")]
    [Range(0, 20f)][SerializeField] private float movementSpeed = 0.1f;
    [Range(0, 5f)][SerializeField] private float walkAnimationSpeed = 10f;
    [SerializeField] private bool enableAutomaticSpriteSorting = true;
    [SerializeField] private int initialSortingOrder = 1;

    [Header("Health")]
    [SerializeField] private float life = 100f;
    private float maxLife;

    public bool movementEnabled = true;
    public bool dead = false;
    private List<SpriteRenderer> spriteRenderer = new List<SpriteRenderer>();
    private float movementScale = 0;
    private Vector3 lastPosition = new Vector3(0,0,0);

    void Start(){
        input = GetComponent<NormalInput>();
        playerWeapon = GetComponent<PlayerWeapon>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        lastPosition = m_rigidbody.position;
        maxLife = life;
        GetComponentsInChildren<SpriteRenderer>(true, spriteRenderer);
        for (int i = 0; i < spriteRenderer.Count; i++)
            spriteRenderer[i].sortingOrder = initialSortingOrder;
    }

    private void Update () {
        if(enableAutomaticSpriteSorting)
        {
            for (int i = 0; i < spriteRenderer.Count; i++)
                spriteRenderer[i].sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        }        

        //float actualVelocity = (transform.position - lastPosition).magnitude * walkAnimationSpeed;
        //lastPosition = transform.position;
        movementScale = Mathf.SmoothStep(movementScale, m_rigidbody.velocity.magnitude, Time.deltaTime*10);

        m_Animator.SetFloat("MoveSpeed", movementScale * walkAnimationSpeed);

        GameManager.Instance.audioManager.Footstep(movementScale > 0.05f && moveDir.magnitude > 0);
    }

    private void FixedUpdate()
    {
        if (!movementEnabled) return;

        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Die") ||
        m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            return;

        //m_rigidbody.MovePosition(m_rigidbody.position + moveDir * movementSpeed);
        m_rigidbody.velocity = moveDir * movementSpeed;

        UpdateCharacterDirection();
    }

    private void UpdateCharacterDirection()
    {
        if(moveDir.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDir.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void UpdateHealth()
    {
        GameManager.Instance.uiManager.UpdateHealthBar(life, maxLife);
    }

    public void Attack1()
    {
        if (PlayerWeapon.playerAttacking)
            return;

        playerWeapon.AttackQuick();
        m_Animator.SetTrigger("Attack");
        GameManager.Instance.audioManager.SwordShort();
    }
    public void Attack2()
    {
        if (PlayerWeapon.playerAttacking)
            return;

        playerWeapon.AttackStrong();
        m_Animator.SetTrigger("Attack2");
        GameManager.Instance.audioManager.SwordLong();
    }
    public void Hit()
    {
        m_Animator.Play("Hit");
    }
    public void Die()
    {
        dead = true;
        m_Animator.Play("Die");
        movementEnabled = false;
        enabled = false;
        GameManager.Instance.EndGame();
    }

    private void AddDamage(float damage)
    {
        if (dead) return;

        life -= damage;

        if(life <= 0)
        {
            Die();
        }
        else
        {
            Hit();
        }

        GameManager.Instance.audioManager.DamageHit();

        UpdateHealth();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyWeapon"))
        {
            if(collision.TryGetComponent(out EnemyArrow enemyArrow)){
                AddDamage(enemyArrow.weaponDamage);
                enemyArrow.DespawnArrow();
            }
        }
    }
}
