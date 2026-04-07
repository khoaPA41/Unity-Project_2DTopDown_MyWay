using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerFarmer : MonoBehaviour
{
    [Header("Tile map for plant")]
    [SerializeField] Tilemap groundMap;
    //[SerializeField]

    [Header("Settings")]
    [SerializeField] float distanceInteract;

    [Header("Seed Object Pool")]
    [SerializeField] ObjectPooling getSeed;


    public void PlantSeed(string seed, Vector2 playerDirection)
    {
        Vector3 targetPos = transform.position + (Vector3)(playerDirection * distanceInteract);
        Vector3Int cellPos = groundMap.WorldToCell(targetPos);

        if (groundMap.HasTile(cellPos))
        {
            Debug.Log("Plant Succesfully");
            Vector3 plantPos = groundMap.GetCellCenterWorld(cellPos);
            getSeed.GetObjectPooled(seed, plantPos);
        }
    }

}
