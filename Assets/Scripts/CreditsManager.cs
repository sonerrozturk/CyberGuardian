using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    void Update()
    {
        // Sadece ESC tuþuna basýlýp basýlmadýðýný dinler
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Olasý bir donmayý önlemek için zamaný normal hýzýna garantiler
            Time.timeScale = 1f;
            // Ana Menü sahnesini yükler
            SceneManager.LoadScene("MainMenu");
        }
    }
}