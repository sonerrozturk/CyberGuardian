using UnityEngine;
using TMPro; 

public class PlayerCarry : MonoBehaviour
{
    [Header("Carry Settings")]
    public Transform holdPoint; // The hand point where the object will stay
    public KeyCode interactKey = KeyCode.E;
    public TMP_Text infoText; // Information text to appear on the screen

    private GameObject currentCarryingObject = null;
    private GameObject objectInRange = null;

    void Start()
    {
        // Hide the text at the start of the game
        if (infoText != null) infoText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (currentCarryingObject != null)
            {
                DropObject();
            }
            else if (objectInRange != null)
            {
                PickUpObject(objectInRange);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If there is a carryable object nearby and our hands are empty
        if (other.CompareTag("VeriTasi") && currentCarryingObject == null)
        {
            objectInRange = other.gameObject;
            CarryableObject objectCode = other.GetComponent<CarryableObject>();

            // Print the object-specific text to the screen and make it visible
            if (infoText != null && objectCode != null)
            {
                infoText.text = objectCode.interactionMessage;
                infoText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // When we walk away from the object
        if (other.CompareTag("VeriTasi") && objectInRange == other.gameObject)
        {
            objectInRange = null;

            // Hide the text again
            if (infoText != null) infoText.gameObject.SetActive(false);
        }
    }

    private void PickUpObject(GameObject obj)
    {
        currentCarryingObject = obj;

        // Disable the object's physical collision so it doesn't get stuck while walking
        obj.GetComponent<Collider2D>().enabled = false;

        // Take the object to the hand point
        obj.transform.SetParent(holdPoint);
        obj.transform.localPosition = Vector3.zero;

        // Hide the text once picked up
        if (infoText != null) infoText.gameObject.SetActive(false);
    }

    private void DropObject()
    {
        currentCarryingObject.transform.SetParent(null);

        currentCarryingObject.GetComponent<Collider2D>().enabled = true;

        currentCarryingObject = null;
    }
}