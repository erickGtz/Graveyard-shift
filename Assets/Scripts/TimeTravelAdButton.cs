using UnityEngine;

public class TimeTravelAdButton : MonoBehaviour
{
    public void OnAdClicked()
    {
        // Al darle clic, busca el Manager directamente desde el código sin necesitar el Inspector
        if (TimeTravelManager.Instance != null)
        {
            TimeTravelManager.Instance.TriggerTimeTravel();
        }
        else
        {
            Debug.LogError("No se encontró el TimeTravelManager en la escena.");
        }
    }
}
