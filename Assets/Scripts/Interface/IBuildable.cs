using UnityEngine;

public interface IBuildable
{
   public BuildObjectType GetType();
   public Vector3 GetPosition();
   public void ChangeType();
   public void OpenObject(GameObject prefabObjec);
}