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
    public static Action<float> OnFoodEaten;
    public static Action<float> OnDrinkUsed;
    private void Start()
    {
        currentHunger = maxHunger;
        currentThirst = maxThirst;
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
               
              
            }

            Debug.Log($"Açlık: {currentHunger}, Susuzluk: {currentThirst}");
        }
    }

    void SetSlidersValue()
    {
        _survivalCanvas.SetSlidersValue(currentHunger / maxHunger, currentThirst / maxThirst);
     
    }
    public void Eat(float amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
    }

    public void Drink(float amount)
    {
        currentThirst += amount;
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);
    }

    public float GetCurrentHunger()
    {
        return currentHunger;
    }

    public float GetCurrentThirst()
    {
        return currentThirst;
    }

    public void LoseHealth(float damageAmount)
    {
        
    }

    private void OnEnable()
    {
        OnFoodEaten += Eat;
        OnDrinkUsed += Drink;
    }  private void OnDisable()
    {
        OnFoodEaten -= Eat;
        OnDrinkUsed -= Drink;

    }
}
