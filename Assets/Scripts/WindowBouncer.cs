using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBouncer : MonoBehaviour
{
    public float speed = 150f;
    private Vector2 direction;
    private RectTransform rectTransform;
    private RectTransform parentRect;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRect = transform.parent.GetComponent<RectTransform>();

        Vector3 worldPos = rectTransform.position;
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.position = worldPos;

        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }


    void Update()
    {
        if (parentRect == null) return;

        rectTransform.anchoredPosition += direction * speed * Time.deltaTime;

        float limitX = (parentRect.rect.width / 2f) - (rectTransform.rect.width / 2f);
        float limitY = (parentRect.rect.height / 2f) - (rectTransform.rect.height / 2f);

        Vector2 pos = rectTransform.anchoredPosition;

        if (pos.x >= limitX || pos.x <= -limitX)
        {
            direction.x *= -1;
            pos.x = Mathf.Clamp(pos.x, -limitX, limitX);
        }
        if (pos.y >= limitY || pos.y <= -limitY)
        {
            direction.y *= -1;
            pos.y = Mathf.Clamp(pos.y, -limitY, limitY);
        }

        rectTransform.anchoredPosition = pos;
    }
}

