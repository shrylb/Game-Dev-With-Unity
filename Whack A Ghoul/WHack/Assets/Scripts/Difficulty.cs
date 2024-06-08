using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    public GameManagerX gameManager;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    // [SerializeField] int diffLev;
    void Start()
    {
        easyButton.onClick.AddListener(() => SetDifficulty(1));
        mediumButton.onClick.AddListener(() => SetDifficulty(2));
        hardButton.onClick.AddListener(() => SetDifficulty(3));
    }

    void SetDifficulty(int difficulty)
    {
        gameManager.StartGame(difficulty);
        // gameManager.ghoulSpawner.StartSpawning(difficulty);

        easyButton.gameObject.SetActive(false);
        mediumButton.gameObject.SetActive(false);
        hardButton.gameObject.SetActive(false);
    }
}
