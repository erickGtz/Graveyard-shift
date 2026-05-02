using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;

    [SerializeField] private RectTransform uiToShake;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float magnitude = 15f;

    private void Awake()
    {
        Instance = this;
    }

    public void TriggerShake()
    {
        StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        Vector2 originalPos = uiToShake.anchoredPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPos.x + Random.Range(-1f, 1f) * magnitude;
            float y = originalPos.y + Random.Range(-1f, 1f) * magnitude;

            uiToShake.anchoredPosition = new Vector2(x, y);

            elapsed += Time.deltaTime;

            yield return null;
        }

        uiToShake.anchoredPosition = originalPos;
    }
}
