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
}
