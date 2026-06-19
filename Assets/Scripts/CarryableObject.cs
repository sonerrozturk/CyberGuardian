using UnityEngine;

public class CarryableObject : MonoBehaviour
{
    [Header("Object Settings")]
    // Is this box the "correct" answer for the area it will be dropped in?
    public bool isCorrectBox;

    // What text should appear on the screen when near the object?
    public string interactionMessage = "Taþýmak ve býrakmak için: (E)";
}