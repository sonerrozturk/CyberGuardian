using UnityEngine;

public class TextStabilizer : MonoBehaviour
{
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        // If the character is facing left (X value is less than zero), make the direction -1, otherwise make it 1.
        float direction = transform.parent.localScale.x < 0 ? -1f : 1f;

        
        // always stay straight
        transform.localScale = new Vector3(initialScale.x * direction, initialScale.y, initialScale.z);
    }
}