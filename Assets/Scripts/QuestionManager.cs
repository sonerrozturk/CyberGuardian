using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    [Header("UI Objects")]
    public GameObject questionPanel;
    public TMP_Text questionTextUI;

    [Header("Level Settings (NEW)")]
    public int targetFishCount = 8; 
    public string nextLevelName = "Level4"; 

    private int correctFishCount = 0; 

    private GameObject heldFish;
    private FishData fishData;
    private HookController hookController;

    public void DisplayQuestion(GameObject caughtFish, HookController hook)
    {
        heldFish = caughtFish;
        hookController = hook;
        fishData = caughtFish.GetComponent<FishData>();

        questionPanel.SetActive(true);
        questionTextUI.text = fishData.questionText;
    }

    public void TrashSelected()
    {
        if (fishData.isSafe == false) CorrectAnswerGiven();
        else WrongAnswerGiven();
    }

    public void BucketSelected()
    {
        if (fishData.isSafe == true) CorrectAnswerGiven();
        else WrongAnswerGiven();
    }

    void CorrectAnswerGiven()
    {
        questionPanel.SetActive(false);
        Destroy(heldFish);

        correctFishCount++;

        // IF all fish are completed, GO TO NEXT LEVEL
        if (correctFishCount >= targetFishCount)
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            // If there are more fish, go back to swinging
            hookController.ReturnToSwinging();
        }
    }

    void WrongAnswerGiven()
    {
        // Restarts the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (questionPanel.activeSelf)
        {
            // If Left Arrow or 'A' is pressed, run the Trash function
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                TrashSelected();
            }
            // If Right Arrow or 'D' is pressed, run the Bucket function
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                BucketSelected();
            }
        }
    }
}