using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform panelParent;
    private InventoryManager _playerInventoryManager;

    private List<InventorySlotUI> slotsUI = new List<InventorySlotUI>();

    private void Start()
    {
        _playerInventoryManager = InventoryManager.Instance;
    }

    private void OnEnable()
    {
        InventoryManager.OnInventoryChanged += UpdateUI;
    }  private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= UpdateUI;
    }

    public void UpdateUI(List<ItemSlot> slots)
    {
        foreach (Transform child in panelParent)
        {
            Destroy(child.gameObject);
        }

        slotsUI.Clear();

        foreach (ItemSlot slot in slots)
        {
            InventorySlotUI slotUI = Instantiate(slotPrefab, panelParent);
            slotUI.SetSlot(slot);
            slotsUI.Add(slotUI);
        }
    }
}

