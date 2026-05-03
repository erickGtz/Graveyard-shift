using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CaptchaWindow : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    public void ResolveCaptcha()
    {
        if (GalaxyManager.Instance != null)
        {
            GalaxyManager.Instance.AddStability();
        }
        Destroy(gameObject);
    }

    public void TriggerError()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(AudioManager.Instance.errorSound);
        StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Vector2 originalPos = rt.anchoredPosition;
        float elapsed = 0f;
        float duration = 0.3f;
        float magnitude = 20f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;
            rt.anchoredPosition = originalPos + new Vector2(offsetX, offsetY);
            yield return null;
        }

        rt.anchoredPosition = originalPos;
    }
}
