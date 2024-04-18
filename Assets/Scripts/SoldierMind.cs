using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMind : MonoBehaviour
{
    public Attacking myAttacking;
    public float health;
    public float damage;
    float startDamage;
    public float speed = -1;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = null;
        rb = GetComponent<Rigidbody2D>();
        startDamage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Angels")
        {
            rb.position += new Vector2(speed, 0) * Time.deltaTime;
        }
        damage = startDamage * PlayerPrefs.GetInt(NumberController.MORALESTAT)/100;
        if (damage < 1)
        {
            damage = 1;
        }
        if (health <=0)
        {
            myAttacking.Die();
        }
    }
    
}
