using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 

public class DirectButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Gidilecek Sahnenin Adý")]
    public string sceneToLoad = "Level1";

    private Image buttonImage;

    void Start()
    {
        // Objenin üzerindeki Image (Resim) bileþenini bul
        buttonImage = GetComponent<Image>();
    }

    // FARE BUTONUN ÜZERÝNE GELDÝÐÝNDE ÇALIÞIR (Mavi Renk)
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            // Saydamlýktan çýkarýp MAVÝ yap
            buttonImage.color = new Color(0f, 0f, 1f, 1f);
        }
    }

    // FARE BUTONUN ÜZERÝNDEN ÇIKTIÐINDA ÇALIÞIR (Tekrar Saydam/Görünmez)
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            // Tekrar görünmez (Alpha 0) yap
            buttonImage.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    // FARE BUTONA TIKLADIÐINDA ÇALIÞIR (Iþýnlama)
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("KENDÝ BUTONUMUZ ÇALIÞTI! Sahneye gidiliyor: " + sceneToLoad);
        Time.timeScale = 1f; // Zaman donmuþsa düzelt
        SceneManager.LoadScene(sceneToLoad);
    }
}