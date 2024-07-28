using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour,IDamagable
{
   public float resourceHealth = 100f;
   public GameObject resourcePartsPrefab;
   public int dropResourceCount = 3;
   public virtual void TakeDamage(float damage)
   {
      resourceHealth -= damage;

      if (resourceHealth <= 0)
      {
         DestroyResource();
      }
   }

   protected void DestroyResource()
   {
      print("ResourceDrop");
      for (int i = 0; i < dropResourceCount; i++)
      {
       GameObject tempPart=  Instantiate(resourcePartsPrefab, new Vector3( transform.position.x+Random.Range(-5,5),transform.position.y+Random.Range(1,5),transform.position.z+Random.Range(1,5)), transform.rotation);
       tempPart.transform.parent = null;
   
      }
      Destroy(gameObject);
   }
}
