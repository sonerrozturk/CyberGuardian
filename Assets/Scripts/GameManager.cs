using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool isGamePaused = false;

    public int correctBrickCount = 0;
    public TMP_Text scoreText; 

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        UpdateScoreUI(); // Display 0/10 at the start
    }

    public void AddCorrectBrick()
    {
        correctBrickCount++;
        UpdateScoreUI(); // Update text when a new correct brick lands

        if (correctBrickCount >= 10)
        {
            NextLevel();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + correctBrickCount + " / 10";
        }
    }

    public void RestartGame()
    {
        // Ensure time flows normally when the game restarts
        Time.timeScale = 1f;
        isGamePaused = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        // Code to transition to the next level (Enter the exact name of your Level 2 scene)
        SceneManager.LoadScene("Level2");
    }

    void Update()
    {
        // When the 'P' key is pressed on the keyboard
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Toggle the state (Pause if running, resume if paused)
            isGamePaused = !isGamePaused;

            if (isGamePaused)
            {
                // Completely stop time (bricks hang in the air)
                Time.timeScale = 0f;
            }
            else
            {
                // Return time to normal flow (bricks continue to fall)
                Time.timeScale = 1f;
            }
        }

        // ESC tuþuna basýldýðýnda
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }   
    }

    // Ana menüye dönme iþlemini yapan fonksiyon
    public void ReturnToMainMenu()
    {
        // ÖNMLÝ: Eðer oyun duraklatýlmýþken çýkýlýrsa, zamanýn donuk kalmasýný engelle
        Time.timeScale = 1f;

        // "MainMenu" sahnesini yükle
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}