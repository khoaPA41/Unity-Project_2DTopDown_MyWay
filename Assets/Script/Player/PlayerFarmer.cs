using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerFarmer : MonoBehaviour
{
    [Header("Tile map for plant")]
    [SerializeField] Tilemap groundMap;

    [Header("Settings")]
    [SerializeField] float distanceInteract;

    [Header("Seed Object Pool")]
    [SerializeField] ObjectPooling getSeed;


    public GameObject PlantSeed(string seed, Vector2 playerDirection, int index)
    {
        Vector3 targetPos = transform.position + (Vector3)(playerDirection * distanceInteract);
        Vector3Int cellPos = groundMap.WorldToCell(targetPos);

        if (groundMap.HasTile(cellPos))
        {
            Debug.Log("Plant Succesfully");
            Vector3 plantPos = groundMap.GetCellCenterWorld(cellPos);
            PooledObject seedPoolObject = getSeed.GetObjectPooled(seed, plantPos);
            Inventories.Instance.inventoriesList[index].itemData.UseItem(seedPoolObject.gameObject);
            Inventories.Instance.SubtractItem(index);
            return seedPoolObject.gameObject;
        }
        return null;
    }

}
