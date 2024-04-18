using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{


    [SerializeField] public UnitStats stats;
    [HideInInspector] public UnitStats extraStats;
    protected float health = 1;

    [SerializeField] Animator anim;
    const string ATTACK = "CanAttack";
    const string IDLE = "isIdle";
    const string ENEMY_ALIVE = "GruntAlive";

    [SerializeField] Transform[] nodes;
    [HideInInspector] int currentNode = 0;

    public LayerMask allyLayer, enemyLayer;

    [SerializeField] Rigidbody2D rb;

    [HideInInspector] public delegate void OnDamageEvent(float amount, Character_Base damager = null);
    [HideInInspector] public OnDamageEvent onDamageEvent;
    [HideInInspector] public delegate void OnHealEvent(float amount);
    [HideInInspector] public OnHealEvent onHealEvent;
    [HideInInspector] public delegate void OnDeathEvent();
    [HideInInspector] public OnDeathEvent onDeathEvent;

    Test enemy;
    
    void Update()
    {

        
        if (anim.GetBool(ATTACK))
        {
            DoAttack(enemy);
        }

        else if (anim.GetBool(ENEMY_ALIVE) && !anim.GetBool(IDLE))
        {
            if (gameObject.CompareTag("Angels"))
            {
                Move(MoveLeft(), true);
                //StartCoroutine(Check());
            }
            anim.SetBool(IDLE, true);
        }

    }

    IEnumerator Check() 
    {
        anim.SetBool(IDLE, true);
        yield return new WaitForSeconds(3);
        anim.SetBool(ENEMY_ALIVE, true);
    }

    public virtual void Init() 
    {
        onDeathEvent = null;
        onHealEvent = null;
        onDamageEvent = null;

        health = GetCurrentStats().maxHealth;
    }

    public void Move(Transform target, bool isLeft)
    {
        if (transform.position != target.position)
        {   
            float speed = GetCurrentStats().speed;

            if (isLeft)
            {
                speed *= -1;
            }

            rb.velocity = new Vector2(speed, 0f);
        }
        rb.velocity = new Vector2(0f,0f);
        //transform.position = target.position;
    }

    public Transform MoveLeft()
    {
        if(currentNode + 1 < nodes.Length) currentNode += 1;
        return nodes[currentNode];
    }

    public Transform MoveRight()
    {
        currentNode = nodes.Length - 1;
        return nodes[currentNode];
    }

    public UnitStats GetCurrentStats()
    {
        UnitStats finalStats = stats;

        finalStats.maxHealth += extraStats.maxHealth;
        finalStats.speed += extraStats.speed;

        return finalStats;
    }
    private void Awake()
    {
        transform.parent = null;
    }
    public virtual void Heal(float heal)
    {
        health += heal;
        if (health > GetCurrentStats().maxHealth) health = GetCurrentStats().maxHealth;
        UpdateSprite();
        
    }

    public virtual void TakeDamage(float damage)
    {
        float totalDamage = damage;

        if (totalDamage < 0) totalDamage = 0;

        health -= totalDamage;
        if (health < 0) health = 0;
        UpdateSprite();
    }

    public virtual void UpdateSprite()
    {
        // change sprite based on current health
    }

    public virtual void DoAttack(Test enemy)
    {
        if (enemy == null)
        {
            anim.SetBool(IDLE, true);
            return;
        }

        enemy.TakeDamage(GetDamageToDeal());
    }

    public virtual float GetHealthPercent()
    {
        return health / GetCurrentStats().maxHealth;
    }

    public virtual float GetDamageToDeal()
    {
        float damage = GetCurrentStats().atk;
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool(IDLE, false);
        anim.SetBool(ENEMY_ALIVE, false);
        anim.SetBool(ATTACK, true);

        enemy = collision.GetComponent<Test>();
    }

}
