using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField]
    private GameObject windowPrefab;

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
        if (timer >= spawnInterval)
        {
            SpawnWindow();
            timer = 0.0f;
        }
    }

    private void SpawnWindow()
    {
        GameObject newWindow = Instantiate(windowPrefab, desktopArea);
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
    }
}
