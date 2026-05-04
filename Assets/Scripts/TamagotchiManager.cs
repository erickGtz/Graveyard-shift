using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TamagotchiManager : MonoBehaviour
{
    [SerializeField] private Image hungerPieChart;
    [SerializeField] private float maxHunger = 20f;
    [SerializeField] private float hungerDrainRate = 5f;
    [SerializeField] private float eatingSpeed = 10f;

    [Header("Sprites del Chango")]
    [SerializeField] private Image monkeyImage;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite sadSprite;
    [SerializeField] private Sprite eatingSprite;
    [SerializeField] private Sprite deadSprite;

    private float currentHunger;
    private bool isDead = false;
    private bool isEating = false;

    void Start()
    {
        currentHunger = maxHunger;
        UpdatePieChart();
    }

    void Update()
    {
        if (isDead)
        {
            if (monkeyImage != null && deadSprite != null) monkeyImage.sprite = deadSprite;
            return;
        }

        if (isEating)
        {
            if (monkeyImage != null && eatingSprite != null) monkeyImage.sprite = eatingSprite;
            
            currentHunger += eatingSpeed * Time.deltaTime;

            if (currentHunger >= maxHunger)
            {
                currentHunger = maxHunger;
                isEating = false;
            }
        }
        else
        {
            // Cambiar a sprite triste si baja del 40%
            if (monkeyImage != null)
            {
                if (currentHunger <= maxHunger * 0.4f)
                    monkeyImage.sprite = sadSprite;
                else
                    monkeyImage.sprite = normalSprite;
            }

            currentHunger -= hungerDrainRate * Time.deltaTime;

            if (currentHunger <= 0)
            {
                currentHunger = 0;
                Die();
            }
        }

        UpdatePieChart();
    }

    public void FeedBanana()
    {
        if (isDead || isEating) return;

        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(AudioManager.Instance.monkeyEatSound);
        isEating = true;
    }

    private void UpdatePieChart()
    {
        if (hungerPieChart != null)
        {
            hungerPieChart.fillAmount = currentHunger / maxHunger;
        }
    }

    public void ResetHunger()
    {
        currentHunger = maxHunger;
        UpdatePieChart();
    }


    private void Die()
    {
        isDead = true;
        Debug.Log("GAME OVER! El chango murió.");
        if (GameOverManager.Instance != null)
        {
            GameOverManager.Instance.TriggerGameOver("NEGLECTED THE OFFICE MONKEY");
        }
    }
}
