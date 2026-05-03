using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [SerializeField] private GameObject gameOverPanel; // El panel que tapará la pantalla
    [SerializeField] private TMP_Text causeOfDeathText; // Para decirle por qué perdió

    void Awake()
    {
        Instance = this;
    }

    public void TriggerGameOver(string reason)
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(AudioManager.Instance.gameOverSound);
        
        // Congelamos el tiempo del juego para que todo se detenga
        Time.timeScale = 0f;

        // Mostramos la pantalla de Game Over
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (causeOfDeathText != null) causeOfDeathText.text = "REASON: " + reason;
    }

    public void RestartShift()
    {
        // Descongelamos el tiempo y recargamos la escena
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
