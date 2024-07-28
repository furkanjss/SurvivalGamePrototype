using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildObject : MonoBehaviour,IBuildable
{
    public GameObject sidePrefab;
    public BuildObjectType buildType;
  
    public BuildObjectType GetType() => buildType;
    public Vector3 GetPosition() => transform.position;
  
    public void OpenObject(GameObject prefabObject)
    {
        Instantiate(prefabObject, transform);
       ChangeType();
    }

   

    private void OnCollisionStay(Collision other)
    {
        IBuildable buildable = other.gameObject.GetComponent<IBuildable>();
        if (buildable != null)
        {
            if (buildable.GetType() == BuildObjectType.Side&&other.transform.position.y==transform.position.y)
            {
                buildable.ChangeType();
            }
        }
    }

    public void ChangeType()=>  buildType = BuildObjectType.Block;
}

public enum BuildObjectType
{
    Ground,Block,Side
}