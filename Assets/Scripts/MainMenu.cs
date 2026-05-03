using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Magia para cambiar de escenas

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Esto carga tu juego. ¡Ojo! Asegúrate de que el nombre entre comillas sea EXACTAMENTE el de tu escena principal.
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Debug.Log("Apagando sistema...");
        Application.Quit(); // Esto cerrará el .exe del juego final
    }
}

