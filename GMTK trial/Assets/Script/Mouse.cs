using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite clickedSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
    }

    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
 // Force it to be in the same plane as the object

        // Set the object's position to the mouse position
        transform.position = mousePos;

        // Change sprite based on mouse button state
        if (Input.GetMouseButton(0))
        {
            spriteRenderer.sprite = clickedSprite;
        }
        else
        {
            spriteRenderer.sprite = defaultSprite;
        }
    }
}