using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GhoulSpawner : MonoBehaviour
{
    public GameManagerX gameManager;
    public GameObject[] ghoulPrefabs; // Array to hold different ghoul prefabs
    public Transform[] spawnPoints; // Array to hold spawn points
    public GameObject explosionFx;

    private int ghoulsToSpawn = 1; 
    private float spawnRate;
    private float spawnDelay = 3f; // Delay before another ghoul can spawn at the same point
    private List<float> spawnPointTimers;
    private Coroutine spawnRoutine;
    Difficulty Difficulty;

    private void Start()
    {
        // Initialize the timers for each spawn point
        spawnPointTimers = new List<float>(new float[spawnPoints.Length]);
    }

    public void StartSpawning(int difficulty)
    {
        ghoulsToSpawn = difficulty; 
        Debug.Log("Numbah of ghouls: " + ghoulsToSpawn);

        spawnRate = difficulty == 3 ? 1.5f : 3f;
         // Stop any previous spawning coroutine to avoid duplicates
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
        
        // Start a new spawning coroutine
        spawnRoutine = StartCoroutine(SpawnGhouls());
    }

    IEnumerator SpawnGhouls()
    {
        while (gameManager.isGameActive)
        {
            int spawnedCount = 0;   //ghouls spawned tracker
            List<int> usedSpawnPoints = new List<int>(); //used spawnPoints tracker

            while (spawnedCount < ghoulsToSpawn)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                
                if (!usedSpawnPoints.Contains(spawnIndex) && spawnPointTimers[spawnIndex] <= 0)
                {
                    Transform spawnPoint = spawnPoints[spawnIndex]; //retrieve transform of pooint
                    GameObject ghoulPrefab = ghoulPrefabs[Random.Range(0, ghoulPrefabs.Length)]; //get random ghoul
                    GameObject ghoul = Instantiate(ghoulPrefab, spawnPoint.position, spawnPoint.rotation);
                    Explosion(spawnPoint.position);
                    StartCoroutine(DestroyGhoulAfterDelay(ghoul, spawnDelay));
                    spawnPointTimers[spawnIndex] = spawnDelay; // Reset the timer for this spawn point
                    usedSpawnPoints.Add(spawnIndex);
                    spawnedCount++;
                }
            }
            // Wait for next spawn interval
            yield return new WaitForSeconds(spawnRate);
        }
    }

    IEnumerator DestroyGhoulAfterDelay(GameObject ghoul, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (ghoul != null)
        {
            Explosion(ghoul.transform.position);
            Destroy(ghoul);
        }
    }

    public void Explosion(Vector3 position)
    {
        Instantiate(explosionFx, position, Quaternion.identity);
    }

    private void Update()
    {
        for (int i = 0; i < spawnPointTimers.Count; i++)
        {
            if (spawnPointTimers[i] > 0) //meaning it is in use
            {
                spawnPointTimers[i] -= Time.deltaTime; //decrement with deltaTime
            }
        }
    }
}
