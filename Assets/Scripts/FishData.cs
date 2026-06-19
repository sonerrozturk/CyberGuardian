using UnityEngine;

public class FishData : MonoBehaviour
{
    [Header("Phishing Question")]
    [TextArea(3, 5)] 
    public string questionText;

    [Header("Status")]
    [Tooltip("Check if safe, leave empty if harmful/virus")]
    public bool isSafe;
}