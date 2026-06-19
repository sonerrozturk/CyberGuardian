using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuKeyboardController : MonoBehaviour
{
    void Update()
    {
        // 1 Tuþuna basýldýðýnda (Klavyenin üstündeki 1 veya Numpad'deki 1)
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            LoadLevel("Level1");
        }
        // 2 Tuþuna basýldýðýnda
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            LoadLevel("Level2");
        }
        // 3 Tuþuna basýldýðýnda
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            LoadLevel("Level3");
        }
        // "START GAME" için alternatif olarak Enter tuþu
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            LoadLevel("Level1"); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            LoadLevel("Level4"); 
        }
    }

    private void LoadLevel(string levelName)
    {
        Debug.Log(levelName + " yükleniyor...");
        SceneManager.LoadScene(levelName);
    }
}