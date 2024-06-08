using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    private GameManagerX gameManagerX;
    public int pointValue;
    public GameObject explosionFx;
    AudioManager audioManager;
    GhoulSpawner ghoulSpawner;

    public float timeOnScreen = 0.5f;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();
        // StartCoroutine(RemoveObjectRoutine()); // begin timer before target leaves screen
        
    }

    // When target is clicked, destroy it, update score, and generate explosion
    private void OnMouseDown()
    {
        if (gameManagerX.isGameActive)
        {
            Destroy(gameObject);
            gameManagerX.UpdateScore(pointValue);
            audioManager.WhackSFX(audioManager.whack);
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
// 
    //     // if (other.gameObject.CompareTag("Sensor") && !gameObject.CompareTag("Bad"))
    //     // {
    //     //     gameManagerX.GameOver();
    //     // } 
    }

    // Display explosion particle at object's position
    void Explode()
    {
        Instantiate(explosionFx, transform.position, Quaternion.identity);
    }

    // After a delay, Moves the object behind background so it collides with the Sensor object
    // IEnumerator RemoveObjectRoutine()
    // {
    //     yield return new WaitForSeconds(timeOnScreen);
    //     if (gameManagerX.isGameActive)
    //     {
    //         transform.Translate(Vector3.forward * 5, Space.World);
    //     }
    // }
}
