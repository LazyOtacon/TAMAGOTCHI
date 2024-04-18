using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct UnitStats
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float atk;
    [SerializeField] public float speed;
    [SerializeField] public float range;
    [SerializeField, Range(0.01f, 999f)] public float firerate;
}

public static class EnumerableExtensions
{
    public static IEnumerable<T> ToEnumerable<T>(this Array target)
    {
        foreach (var item in target)
            yield return (T)item;
    }
}
public class Character_Base : MonoBehaviour
{
    [SerializeField] public UnitStats stats;
    [HideInInspector] public UnitStats extraStats;
    protected float health = 1;

    public LayerMask allyLayer, enemyLayer;

    [HideInInspector] public string nameID;
    [HideInInspector] protected AI_Machine AI_StateMachine;
    [HideInInspector] public bool canBeTarget = false;

    [HideInInspector] public delegate void OnDamageEvent(float amount, Character_Base damager = null);
    [HideInInspector] public OnDamageEvent onDamageEvent;
    [HideInInspector] public delegate void OnHealEvent(float amount);
    [HideInInspector] public OnHealEvent onHealEvent;
    [HideInInspector] public delegate void OnDeathEvent();
    [HideInInspector] public OnDeathEvent onDeathEvent;

    [Header("Misc")]
    [SerializeField] public bool UseAnimations = false;

    public virtual void Init()
    {
        onDeathEvent = null;
        onHealEvent = null;
        onDamageEvent = null;

        health = GetCurrentStats().maxHealth;

        AI_StateMachine = InitStateMachine();
        Invoke("SetTarget", 0.5f);
    }

    public void SetTarget()
    {
        canBeTarget = true;
    }

    protected virtual AI_Machine InitStateMachine()
    {
        if (AI_StateMachine != null) AI_StateMachine = null;
        return new AI_Machine(this, this);
    }

    public virtual bool Tick()
    {
        extraStats = new UnitStats();
        if (health > GetCurrentStats().maxHealth) health = GetCurrentStats().maxHealth;
        return true;
    }

    public UnitStats GetCurrentStats()
    {
        UnitStats finalStats = stats;

        finalStats.maxHealth += extraStats.maxHealth;
        finalStats.speed += extraStats.speed;
        finalStats.range += extraStats.range;
        finalStats.firerate += extraStats.firerate;
        finalStats.firerate = Mathf.Clamp(finalStats.firerate, 0.01f, Mathf.Infinity);

        return finalStats;
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

    public virtual void DoAttack(Character_Base enemy)
    {
        if (enemy == null) return;

        enemy.TakeDamage(GetDamageToDeal());
    }

    public virtual void OnRemoved()
    {
        onDeathEvent?.Invoke(); // invoke if not null
        canBeTarget = false;
        AI_StateMachine.OnRemoved();
        AI_StateMachine = null;
        UpdateSprite();
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


    //State Machine
    public class AI_Machine : System.Object
    {
        protected MonoBehaviour mono;
        protected Character_Base parent;
        protected string currentState = "Idle";
        protected string nextState;
        protected IEnumerator[] States;

        public AI_Machine(Character_Base parent, MonoBehaviour mono)
        {
            this.parent = parent;
            this.mono = mono;
            nextState = currentState;

            mono.StartCoroutine(GetNextCoroutine());
        }

        public virtual void GetStates()
        {
            States = new IEnumerator[1];

            States[0] = Idle();
        }

        public virtual IEnumerator GetNextCoroutine()
        {
            IEnumerator result = null;

            GetStates();

            for (int i = 0; i < States.Length; i++)
            {
                string stateName = States[i].GetType().Name.Split('<', '>')[1];
                if (stateName == currentState) mono.StopCoroutine(States[i]);
                if (stateName == nextState)
                {
                    result = States[i];
                    break;
                }
            }

            return result;
        }

        public virtual void ChangeState(string state)
        {
            nextState = state;
        }

        protected virtual IEnumerator Idle() //this is the template for all AI states
        {
            currentState = nextState;

            //OnEnter state

            while (currentState == nextState) //Tick state
            {
                yield return new WaitUntil(() => parent.Tick() == true);
            }

            //OnLeave state

            mono.StartCoroutine(GetNextCoroutine());

            yield break;
        }

        public virtual void OnRemoved()
        {
            GetStates();

            for (int i = 0; i < States.Length; i++)
            {
                mono.StopCoroutine(States[i]);
            }
        }
    }
}
