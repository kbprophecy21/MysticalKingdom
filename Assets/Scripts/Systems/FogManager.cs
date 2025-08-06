using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    public GameObject fogTilePrefab;
    public int mapWidth = 60;
    public int mapHeight = 60;
    public float tileSize = 1f;

    private Dictionary<Vector2Int, GameObject> fogTiles = new();

    void Start()
    {
        GenerateFog();
    }

    void GenerateFog()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                Vector3 pos = new Vector3(x * tileSize + tileSize / 2, 0.1f, z * tileSize + tileSize / 2);
                GameObject fog = Instantiate(fogTilePrefab, pos, Quaternion.identity, transform);
                fogTiles[new Vector2Int(x, z)] = fog;
            }
        }
    }

    public void RevealFog(Vector3 worldPosition, float radius)
    {
        int centerX = Mathf.FloorToInt(worldPosition.x / tileSize);
        int centerZ = Mathf.FloorToInt(worldPosition.z / tileSize);

        for (int x = centerX - 5; x <= centerX + 5; x++)
        {
            for (int z = centerZ - 5; z <= centerZ + 5; z++)
            {
                Vector2Int tileCoord = new Vector2Int(x, z);
                if (fogTiles.ContainsKey(tileCoord))
                {
                    float dist = Vector2.Distance(new Vector2(x, z), new Vector2(centerX, centerZ));
                    if (dist <= radius)
                    {
                        fogTiles[tileCoord].SetActive(false); // or fade out
                    }
                }
            }
        }
    }
}
