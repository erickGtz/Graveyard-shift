using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] windowPrefab;

    [SerializeField]
    private GameObject timeTravelAdPrefab; // Hueco exclusivo para el anuncio

    [SerializeField]
    private RectTransform desktopArea;

    [SerializeField]
    private float spawnInterval = 2.0f, timer;

    [SerializeField]
    private int maxWindowsAllowed = 30;

    [SerializeField]
    private float minDistance = 200f;

    private Vector2 lastSpawnPosition = new Vector2(-9999f, -9999f);

    void Update()
    {
        if (desktopArea.childCount >= maxWindowsAllowed)
        {
            Debug.Log("GAME OVER! en WindowManager Updated");
            enabled = false;
            return;
        }

        timer += Time.deltaTime;

        float currentInterval = spawnInterval;
        if (TimeTravelManager.Instance != null)
        {
            currentInterval -= (TimeTravelManager.Instance.weirdnessLevel * 0.5f);

            if (currentInterval < 0.5f) currentInterval = 0.5f;
        }

        if (timer >= currentInterval)
        {
            SpawnWindow();
            timer = 0.0f;
        }
    }

    private void SpawnWindow()
    {
        GameObject prefabToSpawn;
        bool adExists = false;

        // Revisamos si ya hay un anuncio flotando para no spamearlo
        foreach (Transform child in desktopArea)
        {
            if (child.name.Contains("TimeTravel")) adExists = true;
        }

        // 15% de probabilidad de salir el anuncio (solo si no hay uno ya)
        if (timeTravelAdPrefab != null && !adExists && Random.value <= 0.15f)
        {
            prefabToSpawn = timeTravelAdPrefab;
        }
        else
        {
            prefabToSpawn = windowPrefab[Random.Range(0, windowPrefab.Length)];
        }

        GameObject newWindow = Instantiate(prefabToSpawn, desktopArea);
        newWindow.name = prefabToSpawn.name; // Limpiar el nombre de "(Clone)"

        RectTransform windowRectTransform = newWindow.GetComponent<RectTransform>();

        float limiteX = desktopArea.rect.width / 2.0f;
        float limiteY = desktopArea.rect.height / 2.0f;

        limiteX -= windowRectTransform.rect.width / 2.0f;
        limiteY -= windowRectTransform.rect.height / 2.0f;

        Vector2 randomPos;
        int maxAttempts = 10;
        int attempt = 0;

        do
        {
            float randomX = Random.Range(-limiteX, limiteX);
            float randomY = Random.Range(-limiteY, limiteY);
            randomPos = new Vector2(randomX, randomY);
            attempt++;
        }
        while (Vector2.Distance(randomPos, lastSpawnPosition) < minDistance && attempt < maxAttempts);

        lastSpawnPosition = randomPos;
        windowRectTransform.anchoredPosition = randomPos;

        if (TimeTravelManager.Instance != null)
        {
            int weirdness = TimeTravelManager.Instance.weirdnessLevel;
            if (weirdness >= 1)
            {
                float randomZ = Random.Range(-45f, 45f);
                if (Random.value > 0.8f) randomZ += 180f;
                windowRectTransform.localEulerAngles = new Vector3(0, 0, randomZ);
            }

            if (weirdness >= 2)
            {
                newWindow.AddComponent<WindowBouncer>();
            }
        }

    }

    public void ClearAllWindows()
    {
        foreach (Transform child in desktopArea)
        {
            Destroy(child.gameObject);
        }
        timer = 0f;
    }

}
