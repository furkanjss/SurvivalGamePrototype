using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnPlayerDie;
    public GameStatus currentStatus;
    bool isPaused = false;
    public static GameManager instance;
    public static event Action OnInventoryPanelOpen;
    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void TogglePause()
    {
        print("pause");
        if (!isPaused)
        {
            Time.timeScale = 0;  
            isPaused = true;
            
        }
        else
        {
            Time.timeScale = 1; 
            isPaused = false;
           
        }
    }
    public void ChangeStatus(GameStatus newStatus)
    {
        currentStatus = newStatus;
 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInventoryPanelOpen?.Invoke();
        }
    }

    private void GameStatusPlayerDie()
    {
        ChangeStatus(GameStatus.Dead);
    } private void GameStatusCombatMode()
    {
        ChangeStatus(GameStatus.Combat);
    }
    private void OnEnable()
    {
       OnInventoryPanelOpen += TogglePause;
        OnPlayerDie += GameStatusPlayerDie;
        InventoryManager.OnResourceFinish += GameStatusCombatMode;
    } private void OnDisable()
    {
       OnInventoryPanelOpen -= TogglePause;
        OnPlayerDie -= GameStatusPlayerDie;
        InventoryManager.OnResourceFinish -= GameStatusCombatMode;

    }
}

public enum GameStatus
{
    Normal,   // Oyuncunun silahı yok
    Combat,   // Oyuncunun silahı var
    Building, // Oyuncu bir şeyler inşa ediyor
    Dead      // Oyuncu öldü
}
