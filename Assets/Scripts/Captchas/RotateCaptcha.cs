using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateCaptcha : MonoBehaviour
{
    [SerializeField] private Slider rotationSlider;
    [SerializeField] private RectTransform objectToRotate;
    [SerializeField] private RectTransform targetIndicator;

    private float targetAngle;
    private float tolerance = 15f;

    void Start()
    {
        rotationSlider.minValue = 0f;
        rotationSlider.maxValue = 360f;

        targetAngle = Random.Range(0f, 360f);
        targetIndicator.localEulerAngles = new Vector3(0, 0, -targetAngle);

        float startAngle = Random.Range(0f, 360f);
        rotationSlider.value = startAngle;
        RotateObject(startAngle);

        rotationSlider.onValueChanged.AddListener(RotateObject);
    }

    private void RotateObject(float value)
    {
        objectToRotate.localEulerAngles = new Vector3(0, 0, -value);
    }

    public void CheckRotation()
    {
        float difference1 = Mathf.Abs(Mathf.DeltaAngle(rotationSlider.value, targetAngle));

        float difference2 = Mathf.Abs(Mathf.DeltaAngle(rotationSlider.value, targetAngle + 180f));

        if (difference1 <= tolerance || difference2 <= tolerance)
        {
            GetComponent<CaptchaWindow>().ResolveCaptcha();
        }
    }
}

