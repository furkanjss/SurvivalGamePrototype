using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform handRigParent;
    private GameObject currentTool;
    public static Action<GameObject> OnSetNewTool;
    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.GetComponent<ICollectable>();
        if (collectable != null)
        {
            InventoryManager.OnPickUpItem?.Invoke(collectable.GetItem());
          
            Destroy(other.gameObject);
        }
    }

    public void SetNewTool(GameObject toolPrefab)
    {
        if (currentTool!=null)
        {
            Destroy(currentTool);
            currentTool = null;
        }

        currentTool = Instantiate(toolPrefab, handRigParent);
        currentTool.transform.localScale = new Vector3(2, 2, 2);
        currentTool.transform.localPosition=Vector3.zero;
        currentTool.transform.localEulerAngles=Vector3.zero;
    }

    private void OnEnable()
    {
        OnSetNewTool += SetNewTool;
    } private void OnDisable()
    {
        OnSetNewTool -= SetNewTool;
    }
}