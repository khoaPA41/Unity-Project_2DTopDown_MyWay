using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerFarmer : MonoBehaviour
{
    [Header("Tile map for plant")]
    [SerializeField] Tilemap groundMap;

    [Header("Settings")]
    [SerializeField] float distanceInteract;
    [SerializeField] PlayerSelectItem seclectItem;

    [Header("Seed Object Pool")]
    [SerializeField] ObjectPooling getSeed;

    [Header("UI")]
    [SerializeField] GameObject virtualBox;

    PlayerStateMachine playerStateMachine;
    Vector3 targetPos;
    Vector3Int cellPos;
    Vector3 plantPos;

    void Start()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    void Update()
    {
        GetTheBoxPllant();
    }

    public GameObject PlantSeed(string seed, Vector2 playerDirection, int index)
    {
        if (groundMap.HasTile(cellPos))
        {
            PooledObject seedPoolObject = getSeed.GetObjectPooled(seed, plantPos);
            Inventories.Instance.inventoriesList[index].itemData.UseItem(seedPoolObject.gameObject);
            Inventories.Instance.SubtractItem(index, 1);
            return seedPoolObject.gameObject;
        }
        return null;
    }

    public void GetTheBoxPllant()
    {
        targetPos = transform.position + (Vector3)(playerStateMachine.prevDirection * distanceInteract);
        cellPos = groundMap.WorldToCell(targetPos);
        plantPos = groundMap.GetCellCenterWorld(cellPos);
        virtualBox.transform.position = plantPos;
    }

    public void ActiveVirtualBox(bool isActive)
    {
        virtualBox.SetActive(isActive);
    }
}
