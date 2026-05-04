using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Reproductores")]
    [SerializeField] private AudioSource sfxSource; // Para los efectos (cortos)
    [SerializeField] private AudioSource bgmSource; // Para la música de fondo (loop)

    [Header("Audios Principales")]
    public AudioClip clickSound;
    public AudioClip errorSound;
    public AudioClip windowSpawnSound;
    public AudioClip explosionSound;
    public AudioClip monkeyEatSound;
    public AudioClip timeTravelSound;
    public AudioClip gameOverSound;
    public AudioClip winSound;

    void Awake()
    {
        // Esto hace que la música no se corte al cambiar del Menú al Juego
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Llama a esta función desde cualquier lado para reproducir un efecto
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // Para botones normales de la UI (arrastrando en el Inspector)
    public void PlayClick()
    {
        PlaySFX(clickSound);
    }

    public void StopMusic()
    {
        if (bgmSource != null) bgmSource.Stop();
    }

    public void PlayMusic()
    {
        if (bgmSource != null && !bgmSource.isPlaying) bgmSource.Play();
    }
}
