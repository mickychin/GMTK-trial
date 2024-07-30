using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenAI : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 1f; 
    private Vector3 direction; 
    private Vector3 castStartPosition; 

    [Header("Collision")]
    public LayerMask collisionLayer;
    public float checkRadius = 0.5f;

    void Start()
    {
        direction = GetRandomDirection();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, checkRadius, direction, 0.1f, collisionLayer);
        castStartPosition = transform.position;

        if (hit.collider != null)
        {
            direction = GetRandomDirection();
        }
    }

    Vector3 GetRandomDirection()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(castStartPosition, checkRadius);
        Gizmos.DrawLine(castStartPosition, castStartPosition + direction * 0.1f);
        Gizmos.DrawWireSphere(castStartPosition + direction * 0.1f, checkRadius);
    }
}
