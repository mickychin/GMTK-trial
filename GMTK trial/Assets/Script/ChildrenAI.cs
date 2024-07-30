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
    private int originalLayer;

    [Header("Mouse")]
    public float dragFactor = 0.5f;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 previousMousePosition;

    [Header("Death")]
    public GameObject deathObjectPrefab;

    void Start()
    {
        direction = GetRandomDirection();
        originalLayer = gameObject.layer;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition + offset;   

        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
            castStartPosition = transform.position;

            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

            RaycastHit2D hit = Physics2D.CircleCast(castStartPosition, checkRadius, direction, 0.1f, collisionLayer);

            gameObject.layer = originalLayer;

            if (hit.collider != null)
            {
                direction = GetRandomDirection();
            }

            if (IsOffScreen())
            {
                InstantiateDeathObject();
                Destroy(gameObject);
            }
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        offset = transform.position - mousePosition;   

        previousMousePosition = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        if (!isDragging)
            return;

        Vector3 mouseDelta = Input.mousePosition - previousMousePosition;
        previousMousePosition = Input.mousePosition;

        Vector3 objectDirection = direction.normalized;
        Vector3 dragDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float dotProduct = Vector2.Dot(objectDirection, dragDirection.normalized);

        if (dotProduct < 0)
        {
            mouseDelta *= dragFactor;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + mouseDelta);
        mousePosition.z = 0;
        transform.position = mousePosition + offset;
    }

    void OnMouseUp()
    {
        isDragging = false;
        direction = GetRandomDirection();
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

    bool IsOffScreen()
    {
        Camera mainCamera = Camera.main;
        float cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float cameraHalfHeight = mainCamera.orthographicSize;

        float buffer = 0.1f;
        return transform.position.x < -cameraHalfWidth - buffer ||
               transform.position.x > cameraHalfWidth + buffer ||
               transform.position.y < -cameraHalfHeight - buffer ||
               transform.position.y > cameraHalfHeight + buffer;
    }

    void InstantiateDeathObject()
    {
        if (deathObjectPrefab != null)
        {
            Instantiate(deathObjectPrefab, transform.position, Quaternion.identity);
        }
    }
}