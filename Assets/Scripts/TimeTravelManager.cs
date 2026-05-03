using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeTravelManager : MonoBehaviour
{
    public static TimeTravelManager Instance;

    [Header("Ventanas Fijas (Para la Locura)")]
    [SerializeField] private GameObject monkeyWindow;
    [SerializeField] private GameObject spaceMonitorWindow;


    [Header("Referencias Visuales")]
    [SerializeField] private Image flashImage;
    [SerializeField] private TMP_Text clockText;

    [Header("Referencias de Sistemas")]
    [SerializeField] private WindowManager windowManager;
    [SerializeField] private GalaxyManager galaxyManager;
    [SerializeField] private TamagotchiManager tamagotchiManager;

    [Header("Estado")]
    public int weirdnessLevel = 0;
    private int currentHour = 5;
    private int currentMinute = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateClock();
        if (flashImage != null)
        {
            Color c = flashImage.color;
            c.a = 0;
            flashImage.color = c;
            flashImage.gameObject.SetActive(false);
        }
    }

    private float clockTimer = 0f;

    void Update()
    {
        clockTimer += Time.deltaTime;
        
        // Cada 10 segundos reales, avanza 15 minutos en el juego
        if (clockTimer >= 10f)
        {
            clockTimer = 0f;
            currentMinute += 15;

            if (currentMinute >= 60)
            {
                currentMinute = 0;
                currentHour++;
                
                // Si llega a las 13, se reinicia a la 1 (formato 12 horas)
                if (currentHour > 12) currentHour = 1;
            }
            
            UpdateClock();
        }
    }

    public void TriggerTimeTravel()
    {
        Debug.Log("¡VIAJE EN EL TIEMPO INICIADO!");
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(AudioManager.Instance.timeTravelSound);
        
        weirdnessLevel++;

        if (weirdnessLevel == 2)
        {
            if (monkeyWindow != null && monkeyWindow.GetComponent<WindowBouncer>() == null)
            {
                monkeyWindow.AddComponent<WindowBouncer>();
            }
            if (spaceMonitorWindow != null && spaceMonitorWindow.GetComponent<WindowBouncer>() == null)
            {
                spaceMonitorWindow.AddComponent<WindowBouncer>();
            }
        }

        currentHour -= Random.Range(1, 3);
        if (currentHour <= 0) currentHour += 12;
        UpdateClock();

        StartCoroutine(FlashRoutine());

        if (windowManager != null) windowManager.ClearAllWindows();
        if (galaxyManager != null) galaxyManager.ResetStability();
        if (tamagotchiManager != null) tamagotchiManager.ResetHunger();

        if (ScreenShake.Instance != null) ScreenShake.Instance.TriggerShake();
    }

    private void UpdateClock()
    {
        if (clockText != null)
        {
            clockText.text = currentHour.ToString("00") + ":" + currentMinute.ToString("00") + " AM";
        }
    }

    private IEnumerator FlashRoutine()
    {
        flashImage.gameObject.SetActive(true);
        Color c = flashImage.color;
        
        // Flasheo suave (40% opacidad) en vez de blanco puro para evitar epilepsia
        float maxAlpha = 0.4f;
        c.a = maxAlpha;
        flashImage.color = c;
        
        float duration = 1.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(maxAlpha, 0f, elapsed / duration);
            flashImage.color = c;
            yield return null;
        }

        flashImage.gameObject.SetActive(false);
    }
}

