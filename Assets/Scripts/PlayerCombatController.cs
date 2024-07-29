using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public  event Action OnAttackTriggered;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.instance.currentStatus!=GameStatus.Building)
            {
                Vector3 mousePosition = Input.mousePosition;
                float screenHeight = Screen.height;

               
                if (mousePosition.y >= screenHeight * 0.15f)
                {
                    OnAttackTriggered?.Invoke();
                }
            }
           
           
           
        }
    }
}
