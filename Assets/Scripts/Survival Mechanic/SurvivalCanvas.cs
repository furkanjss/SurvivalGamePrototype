using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalCanvas : MonoBehaviour
{
    [Header("Survival Ui")]
    [SerializeField]
    private Image hungerSlider;
    [SerializeField]
    private Image thirstSlider;
    [SerializeField]
    private Image healthSlider;

    [Header("PopUp")] [SerializeField] private TextMeshProUGUI hungerPopUp;
    [SerializeField] private TextMeshProUGUI thirstPopUp;
    [SerializeField] private TextMeshProUGUI healthPopUp;

    public void SetSlidersValue(float hungerPercentage,float thirstPercentage,float healthPercentage)
    {
        
        hungerSlider.fillAmount = hungerPercentage;
        thirstSlider.fillAmount = thirstPercentage;
        healthSlider.fillAmount = healthPercentage;
    }

    private void OnEnable()
    {
        SurvivalManager.OnFoodEaten += HungerPopUp;
    }private void OnDisable()
    {
        SurvivalManager.OnFoodEaten -= HungerPopUp;
    }

    void HungerPopUp(float amount)
    {
        hungerPopUp.text = "";
        if (amount>0)
        {
            hungerPopUp.text += "+";
        }

        hungerPopUp.text += amount.ToString();
        DOVirtual.DelayedCall(1, () =>
        {
            hungerPopUp.text = "";
        });
    }
}
