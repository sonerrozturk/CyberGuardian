using UnityEngine;
using UnityEngine.SceneManagement; 

public class DeliveryPoint : MonoBehaviour
{
    [Header("Level Transition Settings")]
    public int collectedCorrectStones = 0; // Our hidden counter
    public int targetStoneCount = 4;       // How many stones to finish the level?
    public string nextLevelName = "Level3"; // Exact name of the scene to load

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the object entering is a Data Stone
        if (other.CompareTag("VeriTasi"))
        {
            CarryableObject obj = other.GetComponent<CarryableObject>();

            if (obj != null)
            {
                if (obj.isCorrectBox)
                {
                    // 1. CORRECT STONE: Destroy the stone
                    Destroy(other.gameObject);

                    // 2. Increment the counter by 1
                    collectedCorrectStones++;

                    Debug.Log("Correct stone delivered! Total: " + collectedCorrectStones); // Prints to console, for testing

                    // 3. If counter reaches target, GO TO NEW LEVEL!
                    if (collectedCorrectStones >= targetStoneCount)
                    {
                        SceneManager.LoadScene(nextLevelName);
                    }
                }
                else
                {
                    // WRONG STONE: Game restarts
                    GameManager.instance.RestartGame();
                }
            }
        }
    }
}