using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptchaWindow : MonoBehaviour
{
    public void ResolveCaptcha()
    {
        if (GalaxyManager.Instance != null)
        {
            GalaxyManager.Instance.AddStability();
        }
        Destroy(gameObject);
    }
}
