using UnityEngine;
using TMPro;

public class InstructionsScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text instructionsText;

    void Start()
    {
        // Reactivamos la música por si el jugador viene de reiniciar la partida tras morir
        if (AudioManager.Instance != null) AudioManager.Instance.PlayMusic();

        // Congelamos el tiempo apenas carga la escena para que no nazcan ventanas ni baje el tiempo
        Time.timeScale = 0f;  

        int employeeId = Random.Range(10, 999);
        int g1 = Random.Range(100, 999);
        int g2 = Random.Range(100, 999);
        int g3 = Random.Range(100, 999);

        string msg = $"Welcome Time Caretaker no. {employeeId}.\n\n" +
                     $"Remember to feed your office monkey.\n\n" +
                     $"A virus has been reported launching pop-up windows on our systems. " +
                     $"Resolve them to safeguard galaxies #{g1}, #{g2}, and #{g3}.\n\n" +
                     $"NOTE: Several employees have reported a trap screen capable of causing spacetime glitches. " +
                     $"REFRAIN from touching it.\n\n" +
                     $"Good day.";

        if (instructionsText != null)
        {
            instructionsText.text = msg;
        }
    }

    public void StartShift()
    {
        Time.timeScale = 1f; // Descongelamos el juego
        if (AudioManager.Instance != null) AudioManager.Instance.PlayClick();
        gameObject.SetActive(false); // Apagamos el panel de instrucciones
    }
}
