using UnityEngine;
using System.Collections;

public class HideAfterTime : MonoBehaviour
{
    [Header("Ekranda Kalma Süresi (Saniye)")]
    public float displayTime = 10f;

    void Start()
    {
        // Sahne baþladýðý an zamanlayýcýyý (Coroutine) çalýþtýr
        StartCoroutine(HideRoutine());
    }

    IEnumerator HideRoutine()
    {
        // displayTime deðiþkenindeki saniye kadar bekle
        yield return new WaitForSeconds(displayTime);

        // Süre dolduðunda bu objeyi (yazýyý) görünmez yap
        gameObject.SetActive(false);
    }
}