using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] private int unitMaxHP;
    public int unitCurrentHP;

    [Header("Combat Stats")]
    public int ATK;
    [Tooltip("This is how often the unit checks for nearby enemies")]
    public float ReactionTime;
    public float unitAtkSpeed;
    public float unitAtkRange;
    [SerializeField] private GameObject unitAttackSource;

    [Header("Misc")]
    [SerializeField] private Animator unitAnimator;
    public bool AttacksAreAoE = true;
    [SerializeField] private List<GameObject> targettedEnemies = new List<GameObject>();


    /// MANAGING FUNCTIONSs
    private void OnEnable()
    {
        setVariables();
        StartCoroutine(attackRangeCheck());
    }

    private void setVariables()
    {
        unitCurrentHP = unitMaxHP;

        if (unitAnimator == null)
        {
            unitAnimator = FindChildWithTag(gameObject, "UnitAnimator").GetComponent<Animator>();
        }
    }

    /// ATTACKING FUNCTIONS
    IEnumerator attackRangeCheck()
    {
        Collider[] spottedGameObjects = Physics.OverlapSphere(unitAttackSource.transform.position, unitAtkRange);
        if (AttacksAreAoE)
        {
            foreach (var spottedGameObject in spottedGameObjects)
            {
                if (spottedGameObject.CompareTag("Enemy"))
                    targettedEnemies.Add(spottedGameObject.GetComponentInParent<GameObject>());
            }
        }
        else
        {
            //Add checks here for finding nearest enemy. Make it modular to allow other detection types like furthest enemy, lowest HP, highest HP, etc.
        }


        yield return new WaitForSeconds(ReactionTime);
    }

    private void StartAttack()
    {
        unitAnimator.SetTrigger("startAttackAnim");
    }

    public void DealDamage()
    {
        //This is called in the animation event of this unit's Animation Controller
        foreach (var enemy in targettedEnemies)
        {
            enemy.SendMessage("AddDamage", ATK);
        }
    }

    /// HEALTH FUNCTIONS
    public void AddDamage(int receivedDamage)
    {
        unitCurrentHP -= receivedDamage;
    }

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        // This script finds children in the gameobject with a specific tag.
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }
}
