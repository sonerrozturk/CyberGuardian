using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    public void GoToLevel(string levelName)
    {
        // Oyunu duraklatýp çýktýysak zamanýn donuk kalmasýný engellemek için
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
    }
}