using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class CustomButtonData
{
    public GameObject buttonObj;
    public string targetScene;
}

public class MenuMouseController : MonoBehaviour
{
    [Header("Renk Ayarlarý")]
    public Color hoverColor = new Color(0f, 0f, 1f, 1f);
    public Color clickColor = new Color(1f, 0f, 0f, 1f);

    [Header("Buton ve Sahne Baðlantýlarý")]
    public List<CustomButtonData> buttons = new List<CustomButtonData>();

    private GameObject lastHoveredObject = null;
    private bool isClicking = false;

    void Start()
    {
        // Baþlangýçta listeyi tara ve sadece geçerli butonlarý saydam yap
        foreach (var btnData in buttons)
        {
            if (btnData != null && btnData.buttonObj != null)
            {
                Image img = btnData.buttonObj.GetComponent<Image>();
                if (img != null) img.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }

    void Update()
    {
        if (EventSystem.current == null || isClicking) return;

        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        GameObject currentHoveredObject = null;
        string targetSceneToLoad = "";

        // Çarpan objeler listemizdeki saðlam butonlardan biri mi?
        foreach (RaycastResult result in results)
        {
            foreach (var btnData in buttons)
            {
                if (btnData.buttonObj != null && result.gameObject == btnData.buttonObj)
                {
                    currentHoveredObject = result.gameObject;
                    targetSceneToLoad = btnData.targetScene;
                    break;
                }
            }
            if (currentHoveredObject != null) break;
        }

        // 1. DURUM: Fare yeni bir butona girdiyse
        if (currentHoveredObject != lastHoveredObject)
        {
            // Eski buton saðlamsa tekrar saydam yap
            if (lastHoveredObject != null)
            {
                Image img = lastHoveredObject.GetComponent<Image>();
                if (img != null) img.color = new Color(1f, 1f, 1f, 0f);
            }

            // Yeni buton saðlamsa rengini deðiþtir
            if (currentHoveredObject != null)
            {
                Image img = currentHoveredObject.GetComponent<Image>();
                if (img != null) img.color = hoverColor;
            }

            lastHoveredObject = currentHoveredObject;
        }

        // 2. DURUM: Fare listedeki bir butonun üzerindeyken TIKLANIRSA
        if (Input.GetMouseButtonDown(0) && currentHoveredObject != null)
        {
            isClicking = true;
            StartCoroutine(HandleClick(currentHoveredObject, targetSceneToLoad));
        }
    }

    IEnumerator HandleClick(GameObject clickedObj, string targetScene)
    {
        // Sadece obje yok edilmemiþse kýrmýzý yap 
        if (clickedObj != null)
        {
            Image img = clickedObj.GetComponent<Image>();
            if (img != null) img.color = clickColor;
        }

        yield return new WaitForSecondsRealtime(0.15f);

        Time.timeScale = 1f;
        SceneManager.LoadScene(targetScene);
    }
}