using UnityEngine;

public class BreathingEffect : MonoBehaviour
{
    [Header("Respiración (Latido)")]
    public float scaleAmount = 0.05f;
    public float scaleSpeed = 3f;

    [Header("Rotación (Para las Galaxias)")]
    public bool canRotate = false;
    public float rotationSpeed = -15f;

    private Vector3 initialScale;

    void Start()
    {
        // Guardamos el tamaño original para no deformarlo
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Usa una onda Seno (Mathf.Sin) para ir de 1 a -1 suavemente con el tiempo
        float scaleOffset = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;

        // Le sumamos ese poquito al tamaño original (respira)
        transform.localScale = initialScale + new Vector3(scaleOffset, scaleOffset, 0);

        // Si es una galaxia, la rotamos muy lentito
        if (canRotate)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}
