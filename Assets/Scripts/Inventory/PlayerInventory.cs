using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform handRigParent;
    public GameObject currentTool;
    public static Action<GameObject,float> OnSetNewTool;
    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.GetComponent<ICollectable>();
        if (collectable != null)
        {
            InventoryManager.OnPickUpItem?.Invoke(collectable.GetItem());
          
            Destroy(other.gameObject);
        }
    }

    public void SetNewTool(GameObject toolPrefab,float _Tooldamage)
    {
        if (currentTool!=null)
        {
            Destroy(currentTool);
            currentTool = null;
        }

        handRigParent.GetComponent<GunCollisionManager>().enabled = false;
        currentTool = Instantiate(toolPrefab, handRigParent);
            Destroy(currentTool.GetComponent<Item>());
        currentTool.transform.localScale = new Vector3(2, 2, 2);
        currentTool.transform.localPosition=Vector3.zero;
        currentTool.transform.localEulerAngles=Vector3.zero;
        currentTool.AddComponent<GunCollisionManager>();
        currentTool.GetComponent<GunCollisionManager>().damage = _Tooldamage;
    }

    private void OnEnable()
    {
        OnSetNewTool += SetNewTool;
    } private void OnDisable()
    {
        OnSetNewTool -= SetNewTool;
    }

    public void DisableAttack()
    {
        GunCollisionManager gunCollisionManager = handRigParent.GetComponent<GunCollisionManager>();

        if (gunCollisionManager != null)
        {
            gunCollisionManager.canAttack = false;
        }
        else if (currentTool != null)
        {
            gunCollisionManager = currentTool.GetComponent<GunCollisionManager>();
            if (gunCollisionManager != null)
            {
                gunCollisionManager.canAttack = false;
            }
        }
    }

}