using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    private GameObject spawnObject;
    private GameObject spawnObjectPreview;
  
    public LayerMask buildableLayerMask;
    private Camera mainCam;
    private GameObject previewObject;
    private GameManager _gameManager;
    public static Action<GameObject,GameObject,ItemSlot> OnResourceChoice;
    private int totalObjectCount = 0;
    private ItemSlot selectedSlot;
    private void Start()
    {
        _gameManager = GameManager.instance;
        mainCam = GetComponent<Camera>();
    }

    public void GetBuildableObject(GameObject _spawnObject,GameObject _previewObject,ItemSlot itemSlot)
    {
        spawnObject = _spawnObject;
        spawnObjectPreview = _previewObject;
        selectedSlot = itemSlot;
        totalObjectCount = itemSlot.amount;
    }

    private void OnEnable()
    {
        OnResourceChoice += GetBuildableObject;
    } private void OnDisable()
    {
        OnResourceChoice -= GetBuildableObject;
    }

    private void Update()
        {
            if (_gameManager.currentStatus!=GameStatus.Building)return;
            if (totalObjectCount>0)
            {
                 RaycastHit hit;
                Ray ray =mainCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity,buildableLayerMask))
                {
                    IBuildable buildable = hit.collider.GetComponent<IBuildable>();
                    print(hit.collider.name);
                    if (buildable != null)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (buildable.GetType() == BuildObjectType.Side)
                            {
                                buildable.OpenObject(spawnObject);
                                DecreaseResourceItem();
                            }
                            else if (buildable.GetType() == BuildObjectType.Ground)
                            {
                                GameObject tempSpawn=  Instantiate(spawnObject, new Vector3(hit.point.x,hit.point.y+.5f,hit.point.z), Quaternion.identity);
                                tempSpawn.transform.eulerAngles = new Vector3(0, 0, 90);
                                DecreaseResourceItem();
                            }
                           

                        }
                        else
                        {
                            if (buildable.GetType() == BuildObjectType.Side)
                            {
                                if (previewObject==null)
                                {
                                    previewObject = Instantiate(spawnObjectPreview,hit.transform);
                                
                                }
                                
                                 previewObject.transform.parent = hit.transform;
                                 previewObject.transform.localPosition = Vector3.zero;

                            }else if (buildable.GetType() == BuildObjectType.Ground)
                            {
                                if (previewObject==null)
                                {
                                    previewObject = Instantiate(spawnObjectPreview,hit.transform);
                                
                                }

                                previewObject.transform.position = new Vector3(hit.point.x, hit.point.y + .5f, hit.point.z);
                                previewObject.transform.eulerAngles = new Vector3(0, 0, 90);
                            }
                            else
                            {
                                Destroy(previewObject);
                            }
                           

                        

                        }
                    }
            } 
            }
            else
            {
                if (previewObject!=null)
                {
                    Destroy(previewObject);
                    previewObject = null;
                }
            }
           
               
        }


    void DecreaseResourceItem()
    {
        totalObjectCount -= 1;
        
        ResourceItem resource= selectedSlot.item as ResourceItem;
        InventoryManager.Instance.UseResource(resource);
        if (totalObjectCount<=0)
        {
            Destroy(previewObject);
            InventoryManager.OnResourceFinish?.Invoke();
        }
    }
}



