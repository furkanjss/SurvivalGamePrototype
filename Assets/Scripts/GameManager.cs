using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button reloadButton;
    [SerializeField] private GameObject gameOverPanel;
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

    
    void Start()
    {
       
            reloadButton.onClick.AddListener(ReloadScene);
        
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        OnPlayerDie += GameOverPanel;
        InventoryManager.OnResourceFinish += GameStatusCombatMode;
    } private void OnDisable()
    {
       OnInventoryPanelOpen -= TogglePause;
        OnPlayerDie -= GameStatusPlayerDie;
        OnPlayerDie -= GameOverPanel;

        InventoryManager.OnResourceFinish -= GameStatusCombatMode;

    }

    void GameOverPanel() => gameOverPanel.SetActive(true);
}

public enum GameStatus
{
    
    Combat,   
    Building, 
    Dead      
}
