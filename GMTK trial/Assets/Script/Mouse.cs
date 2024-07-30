using UnityEngine;

public class CustomMousePointer : MonoBehaviour
{
    public Sprite defaultSprite; // Sprite for the default state
    public Sprite clickedSprite; // Sprite for the clicked state
    private SpriteRenderer spriteRenderer;
    public Vector3 offset; // Offset for the mouse pointer position

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = false; // Hide the default system cursor
    }

    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Force it to be in the same plane as the object

        // Apply the offset and set the object's position to the adjusted mouse position
        transform.position = mousePos + offset;

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
