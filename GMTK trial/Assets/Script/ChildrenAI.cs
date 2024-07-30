using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenAI : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 1f; 
    private Vector3 direction; 

    [Header("Collision")]
    public LayerMask collisionLayer;
    public float checkRadius = 0.5f;
    private Vector3 castStartPosition;
    public int originalLayer;

    void Start()
    {
        direction = GetRandomDirection();
        originalLayer = gameObject.layer;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        castStartPosition = transform.position; // Save the start position for Gizmos

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        RaycastHit2D hit = Physics2D.CircleCast(castStartPosition, checkRadius, direction, 0.1f, collisionLayer);

        gameObject.layer = originalLayer;

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
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(castStartPosition, checkRadius);
            Gizmos.DrawLine(castStartPosition, castStartPosition + direction * 0.1f);
            Gizmos.DrawWireSphere(castStartPosition + direction * 0.1f, checkRadius);
        }
    }
}
