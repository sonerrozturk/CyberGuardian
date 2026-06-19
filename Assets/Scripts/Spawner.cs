using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Object")]
    public GameObject passwordBoxPrefab; 

    [Header("Spawn Settings")]
    public float initialDelay = 5f; // Oyun baþýndaki ilk bekleme süresi
    public float spawnRate = 2.5f;   // Drop every X seconds
    public float xLimit = 7f;        // Right/left limits of the screen

    private float timer;

    void Start()
    {
        timer = initialDelay;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Spawn a new box when the time resets
        if (timer <= 0f)
        {
            SpawnBox();
            timer = spawnRate; 
        }
    }

    void SpawnBox()
    {
        if (passwordBoxPrefab == null) return;

        // Choose a random X point within our determined limits
        float randomX = Random.Range(-xLimit, xLimit);

        Vector3 spawnPoint = new Vector3(randomX, transform.position.y, 0);

        Instantiate(passwordBoxPrefab, spawnPoint, Quaternion.identity);
    }
}