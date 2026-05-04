using UnityEngine;
using UnityEngine.EventSystems;

public class WindowFocus : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        // Al darle clic a esta ventana, la pasa hasta el frente del Canvas
        transform.SetAsLastSibling();
    }
}
