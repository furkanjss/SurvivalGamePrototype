using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalManager : MonoBehaviour
{
    [Header("Survival Settings")]
    [SerializeField] private float maxHunger = 100f;
    [SerializeField] private float maxThirst = 100f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float hungerDecayRate = 0.1f; // Her saniye açlığın düşme miktarı
    [SerializeField] private float thirstDecayRate = 0.2f; // Her saniye susuzluğun düşme miktarı
    [SerializeField] private SurvivalCanvas _survivalCanvas;
    private float currentHunger;
    private float currentThirst;
    private float currentHealth;
    public static Action<float> OnFoodEaten;
    public static Action<float> OnWaterDrink;
    private void Start()
    {
        currentHunger = maxHunger;
        currentThirst = maxThirst;
        currentHealth = maxHealth;
        StartCoroutine(DecaySurvivalStats());
    }

    private IEnumerator DecaySurvivalStats()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentHunger -= hungerDecayRate;
            currentThirst -= thirstDecayRate;

            currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
            currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);
            SetSlidersValue();
         
            if (currentHunger == 0 || currentThirst == 0)
            {
               
              GameManager.OnPlayerDie?.Invoke();
            }

           
        }
    }

    void SetSlidersValue()
    {
        _survivalCanvas.SetSlidersValue(currentHunger / maxHunger, currentThirst / maxThirst,currentHealth/maxHealth);
     
    }
    public void Eat(float amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        HealthIncrease(amount);
    }

    public void Drink(float amount)
    {
        print("drink");
        currentThirst += amount;
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);
        HealthIncrease(amount);

    }

    void HealthIncrease(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        SetSlidersValue();
    }
 

    public void LoseHealth(float damageAmount)
    {
        currentHealth -= damageAmount;
        SetSlidersValue();
        if (currentHealth<=0)
        {
            GameManager.OnPlayerDie?.Invoke();

        }
    }

    private void OnEnable()
    {
        OnFoodEaten += Eat;
        OnWaterDrink += Drink;
    }  private void OnDisable()
    {
        OnFoodEaten -= Eat;
        OnWaterDrink -= Drink;

    }
}
