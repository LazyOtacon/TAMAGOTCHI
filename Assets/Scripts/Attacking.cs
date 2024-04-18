using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public GameObject weapon;
    public GameObject thisThing;
    public bool inCombat;
    public SoldierMind thisMind;
    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Combat());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != gameObject.tag)
        {
            inCombat = true;
            StartCoroutine(Combat());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != gameObject.tag)
        {
            inCombat = false;
            StopCoroutine(Combat());
        }
    }
    public IEnumerator Combat()
    {
        yield return new WaitForSeconds(1);
        Conflict();
    }
    void Conflict()
    {
        //if(!inCombat)StopCoroutine(Combat());
        if (gameObject.tag == "Angels")
        {
            thisMind.health -= thisMind.damage;
        }
        if (gameObject.tag == "Demons")
        {
            thisMind.health -= 5;
        }
        if (thisMind.health < 1)
        {
            inCombat = false;
            Die();
        }else if (inCombat)
        {
            StartCoroutine(Combat());
        }
    }
    public void Die()
    {
        StopCoroutine (Combat());
        Destroy(thisThing);
    }
}
