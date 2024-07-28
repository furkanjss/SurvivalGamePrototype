using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour,ICollectable
{
  public BaseItem baseItem;

  public BaseItem GetItem()
  {
    return baseItem;
  }
}
