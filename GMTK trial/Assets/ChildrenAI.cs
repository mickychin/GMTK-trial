using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenAI : MonoBehaviour
{

    public float speed = 5f; 
    private Vector3 direction; 


    void Start()
    {
        direction = GetRandomDirection();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    Vector3 GetRandomDirection()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        
        return new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
    }
}
