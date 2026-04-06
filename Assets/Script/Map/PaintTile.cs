using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PaintTile : MonoBehaviour
{
    [SerializeField] Tilemap tileMap;
    [SerializeField] TileBase ruleTile;
    [SerializeField] List<int> startPos;
    [SerializeField] List<Vector2> zoneSize;
    [SerializeField] float scale = .1f;
    [SerializeField] float threshold = .5f;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < startPos.Count; i++)
        {
            for (int x = startPos[i]; x < startPos[i] + zoneSize[i].x; x++)
            {
                for (int y = startPos[i]; y < startPos[i] + zoneSize[i].y; y++)
                {
                    float noise = Mathf.PerlinNoise(x * scale, y * scale);
                    if (noise > threshold)
                    {
                        tileMap.SetTile(new Vector3Int(x, y, 0), ruleTile);
                    }
                }
            }
        }

        tileMap.RefreshAllTiles();
    }
}
