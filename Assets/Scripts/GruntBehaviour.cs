using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntBehaviour : MonoBehaviour
{
    public Collider2D headBox;
    public Collider2D footBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Head" && gameObject.tag == "Feet" && gameObject.tag != "Head")
        {
            collision.gameObject.layer--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
