using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TamagotchiManager : MonoBehaviour
{
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private float maxHunger = 20f;
    [SerializeField] private float hungerDrainRate = 5f;
    private float currentHunger;
    private bool isDead = false;

    void Start()
    {
        currentHunger = maxHunger;

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        currentHunger -= hungerDrainRate * Time.deltaTime;

        // Actualizar la barra visual
        hungerSlider.value = currentHunger;
        // ¡Game Over si llega a cero!
        if (currentHunger <= 0)
        {
            currentHunger = 0;
            Die();
        }
    }

    public void FeedBanana()
    {
        if (isDead) return; // No puedes revivirlo a platanazos
        currentHunger = maxHunger;
        hungerSlider.value = currentHunger;
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("GAME OVER! El chango murió.");
    }
}
