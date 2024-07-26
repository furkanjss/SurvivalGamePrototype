using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour,ICollectable
{
  [FormerlySerializedAs("sOitem")] [FormerlySerializedAs("inventoryItem")] public BaseItem baseItem;

  public BaseItem GetItem()
  {
    return baseItem;
  }
}
