using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public float leftLimit = -5f;  // Maximum left X point the fish can go
    public float rightLimit = 5f;  // Maximum right X point the fish can go

    public bool startsMovingRight = true;

    [Header("Visual Settings (IMPORTANT!)")]
    [Tooltip("Check if the original image of the fish faces right. Leave empty if it faces left.")]
    public bool originalSpriteFacesRight = true;

    private bool isMovingRight;
    private float originalScaleX;

    void Start()
    {
        isMovingRight = startsMovingRight;

        // Store the initial scale from the Inspector 
        originalScaleX = Mathf.Abs(transform.localScale.x);

        // Check the visual direction once at the start
        UpdateVisualDirection();
    }

    void Update()
    {
        if (isMovingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightLimit)
            {
                isMovingRight = false;
                UpdateVisualDirection();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftLimit)
            {
                isMovingRight = true;
                UpdateVisualDirection();
            }
        }
    }

    // A function that mathematically calculates the direction
    void UpdateVisualDirection()
    {
        // Direction multiplier: 1 if moving right, -1 if moving left
        float directionMultiplier = isMovingRight ? 1f : -1f;

        // Sprite correction multiplier
        float spriteCorrection = originalSpriteFacesRight ? 1f : -1f;

        float newScaleX = originalScaleX * directionMultiplier * spriteCorrection;

        transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
    }
}