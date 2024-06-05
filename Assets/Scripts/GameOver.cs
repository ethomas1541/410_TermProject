using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public HealthController playerHealth;
    public HealthController campHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth.OnDeath += OnGameOver;
        campHealth.OnDeath += OnGameOver;
    }

    public void OnGameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
