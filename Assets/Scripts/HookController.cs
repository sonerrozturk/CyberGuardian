using UnityEngine;

public class HookController : MonoBehaviour
{
    // Added a new state: WaitingAtQuestion
    public enum HookState { Swinging, Thrown, Retracting, WaitingAtQuestion }
    public HookState currentState = HookState.Swinging;

    [Header("Swing Settings")]
    public float minAngle = 140f;
    public float maxAngle = 160f;
    public float swingSpeed = 2f;

    [Header("Throw Settings")]
    public float throwSpeed = 5f;
    public float retractSpeed = 5f;
    public float maxRange = 6f;

    private Vector3 startPosition;
    private float timeCounter = 0f;
    private LineRenderer ropeLine;

    // NEW VARIABLES FOR CAUGHT FISH
    private GameObject caughtFish;
    private QuestionManager questionManager;

    void Start()
    {
        startPosition = transform.position;
        ropeLine = GetComponent<LineRenderer>();

        if (ropeLine != null) ropeLine.positionCount = 2;

        questionManager = FindAnyObjectByType<QuestionManager>();
    }

    void Update()
    {
        UpdateRopeVisuals();

        switch (currentState)
        {
            case HookState.Swinging:
                SwingMechanism();
                if (Input.GetKeyDown(KeyCode.Space)) currentState = HookState.Thrown;
                break;
            case HookState.Thrown:
                ThrowMechanism();
                break;
            case HookState.Retracting:
                RetractMechanism();
                break;
            case HookState.WaitingAtQuestion:
                // Hook does nothing and just waits while the question is on screen
                break;
        }
    }

    // PHYSICAL COLLISION (CATCHING THE FISH) HAPPENS HERE
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only grab if it is being thrown and only if it hits a fish
        if (currentState == HookState.Thrown && other.CompareTag("Balik"))
        {
            caughtFish = other.gameObject;

            // 1. Stop the fish's own swimming movement
            FishMovement fishMovement = caughtFish.GetComponent<FishMovement>();
            if (fishMovement != null) fishMovement.enabled = false;

            // 2. Make the fish a child of the hook (so the fish goes where the hook goes)
            caughtFish.transform.SetParent(this.transform);

            currentState = HookState.Retracting;
        }
    }

    void UpdateRopeVisuals()
    {
        if (ropeLine != null)
        {
            ropeLine.SetPosition(0, startPosition);
            ropeLine.SetPosition(1, transform.position);
        }
    }

    void SwingMechanism()
    {
        timeCounter += Time.deltaTime * swingSpeed;
        float interpolation = Mathf.PingPong(timeCounter, 1f);
        float currentAngle = Mathf.Lerp(minAngle, maxAngle, interpolation);
        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    void ThrowMechanism()
    {
        transform.Translate(-transform.up * throwSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(startPosition, transform.position) >= maxRange)
        {
            currentState = HookState.Retracting;
        }
    }

    void RetractMechanism()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition, retractSpeed * Time.deltaTime);

        // When the hook reaches back to the boat
        if (transform.position == startPosition)
        {
            if (caughtFish != null)
            {
                // If a fish is caught, open UI and put the hook in a waiting state
                currentState = HookState.WaitingAtQuestion;
                questionManager.DisplayQuestion(caughtFish, this);
            }
            else
            {
                // If no fish was caught, return to normal swinging
                currentState = HookState.Swinging;
            }
        }
    }

    public void ReturnToSwinging()
    {
        caughtFish = null;
        currentState = HookState.Swinging;
    }
}