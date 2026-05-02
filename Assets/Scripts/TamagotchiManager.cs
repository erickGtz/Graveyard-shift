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
        if (isDead) return;

        if (isEating)
        {
            currentHunger += eatingSpeed * Time.deltaTime;

            if (currentHunger >= maxHunger)
            {
                currentHunger = maxHunger;
                isEating = false;
            }
        }
        else
        {
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

        isEating = true;
    }

    private void UpdatePieChart()
    {
        if (hungerPieChart != null)
        {
            hungerPieChart.fillAmount = currentHunger / maxHunger;
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("GAME OVER! El chango murió.");
    }
}
