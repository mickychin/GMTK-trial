using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenAI : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 1f;
    public bool IsMoving;
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

    [Header("Animation")]
    public Animator animator;
    AudioSource audioSource;

    void Start()
    {
        direction = GetRandomDirection();
        originalLayer = gameObject.layer;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!IsMoving)
        {
            direction = (Vector3.zero - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, Vector3.zero) < 0.1f)
            {
                Destroy(gameObject);
            }

            if(isDragging)
            {
                animator.SetBool("IsPickedUpByThePlayer", true);
            }
            else
            {
                animator.SetBool("IsPickedUpByThePlayer", false);
            }

            return;
        }

        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition + offset;   

        }
        else
        {
            animator.SetBool("IsPickedUpByThePlayer", false);
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
        //audioSource.clip = PickUpClip;
        audioSource.Play();
        isDragging = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        offset = transform.position - mousePosition;
        animator.SetBool("IsPickedUpByThePlayer", true);
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
        FindObjectOfType<HealthBar>().TakeDamage();

        if (deathObjectPrefab != null)
        {
            GameObject deathObject = Instantiate(deathObjectPrefab, transform.position, Quaternion.identity);
            deathObject.transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + 180);
        }
    }
}