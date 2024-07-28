using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform panelParent;
    [SerializeField] private int maxSlotCount;
    private InventoryManager _playerInventoryManager;
    private GameObject inventoryPanel;
    [HideInInspector]
    public List<InventorySlotUI> slotsUI = new List<InventorySlotUI>();
   

    private void Start()
    {
        inventoryPanel = transform.GetChild(0).gameObject;
        _playerInventoryManager = InventoryManager.Instance;
    }

    private void OnEnable()
    {
        InventoryManager.OnInventoryChanged += UpdateUI;
        GameManager.OnInventoryPanelOpen += ToggleInventoryPanel;
    }  private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= UpdateUI;
        GameManager. OnInventoryPanelOpen -= ToggleInventoryPanel;

    }

    public void ClearSlots()
    {
        foreach (Transform child in panelParent)
        {
            Destroy(child.gameObject);
        }

        slotsUI.Clear();
    }
    public virtual void UpdateUI(List<ItemSlot> slots)
    {
        ClearSlots();

        int slotCount = Mathf.Min(slots.Count, maxSlotCount); 
        for (int i = 0; i < slotCount; i++)
        {
            InventorySlotUI slotUI = Instantiate(slotPrefab, panelParent);
            slotUI.SetSlot(slots[i]);
            slotsUI.Add(slotUI);
        }
    }

    void ToggleInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

    }
}

