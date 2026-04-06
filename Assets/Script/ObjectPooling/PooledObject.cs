using UnityEngine;

public class PooledObject : MonoBehaviour
{
    ObjectPooling Instance;
    public ObjectPooling _Instance { get => Instance; set => Instance = value; }

    public void Release()
    {
        if (Instance != null)
        {
            Debug.Log($"Return to pool : {Instance.gameObject.name}");
            Instance.ReturnToPool(this);
        }
    }

    public void Spawn(Vector2 position)
    {
        if (Instance != null)
        {
            Instance.GetObjectPooled(this.name, position);
        }
    }
}
