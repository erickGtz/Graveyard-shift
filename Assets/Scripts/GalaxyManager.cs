using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalaxyManager : MonoBehaviour
{
    public static GalaxyManager Instance;

    [SerializeField] private Slider stabilitySlider;
    [SerializeField] private float maxStability = 100.0f;
    [SerializeField] private float drainRate = 8.0f;
    [SerializeField] private float stabilityPerCaptcha = 15.0f;

    [SerializeField] private int maxGalaxies = 3;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private RectTransform spaceArea;
    private int currentGalaxies;
    private float currentStability;
    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentStability = maxStability;
        currentGalaxies = maxGalaxies;

        if (stabilitySlider != null)
        {
            stabilitySlider.maxValue = maxStability;
            stabilitySlider.value = currentStability;
        }
    }

    void Update()
    {
        if (isGameOver) return;

        currentStability -= drainRate * Time.deltaTime;

        if (stabilitySlider != null)
        {
            stabilitySlider.value = currentStability;
        }

        if (currentStability <= 0)
        {
            ExplodeGalaxy();
        }
    }

    public void AddStability()
    {
        if (isGameOver) return;
        currentStability += stabilityPerCaptcha;
        if (currentStability > maxStability) currentStability = maxStability;
    }

    private void ExplodeGalaxy()
    {
        currentGalaxies--;
        Debug.Log($"¡PUUFFF! Una galaxia explotó. Quedan: {currentGalaxies}");

        if (ScreenShake.Instance != null) ScreenShake.Instance.TriggerShake();

        if (explosionPrefab != null && spaceArea != null)
        {
            GameObject newExplosion = Instantiate(explosionPrefab, spaceArea);
            RectTransform explRect = newExplosion.GetComponent<RectTransform>();

            float limiteX = spaceArea.rect.width / 2f;
            float limiteY = spaceArea.rect.height / 2f;

            float randomX = Random.Range(-limiteX, limiteX);
            float randomY = Random.Range(-limiteY, limiteY);

            explRect.anchoredPosition = new Vector2(randomX, randomY);
        }

        if (currentGalaxies <= 0)
        {
            isGameOver = true;
            Debug.Log("GAME OVER: Colapso Universal. Te despiden.");
        }
        else
        {
            currentStability = maxStability;
        }
    }

    public void ResetStability()
    {
        currentStability = maxStability;
        if (stabilitySlider != null) stabilitySlider.value = currentStability;
    }


}
