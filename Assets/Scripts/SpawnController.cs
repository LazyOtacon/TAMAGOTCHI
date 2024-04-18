using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public float minX = -2.36f;
    public float maxX = 3.45f;
    public float minY = -3.01f;
    public float maxY = 1.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(Random.Range(minX,maxX), Random.Range(minY,maxY));
    }
}
