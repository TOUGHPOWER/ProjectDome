using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NavMeshPlus;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;            // Reference to the ground Tilemap
    [SerializeField] GameObject[] props;               // Array of props to spawn
    [SerializeField] int numberOfProps = 20;           // Number of props to spawn
    [SerializeField] float minDistance = 3f;         // Minimum distance between props
    [SerializeField] NavMeshSurface navMeshSurface;
    private List<Vector3> occupiedPositions = new List<Vector3>(); // Stores placed prop positions

    private void Awake()
    {
        
        
    }

    private void Start()
    {
        SpawnProps(); // Run the spawning logic at the start
        navMeshSurface.BuildNavMesh();
    }

    void SpawnProps()
    {
        BoundsInt bounds = groundTilemap.cellBounds; // Get tilemap bounds
        int attempts = 0;
        int placed = 0;

        while (placed < numberOfProps && attempts < numberOfProps * 10)
        {
            int x = Random.Range(bounds.xMin, bounds.xMax);
            int y = Random.Range(bounds.yMin, bounds.yMax);
            Vector3Int tilePosition = new Vector3Int(x, y, 0);

            if (groundTilemap.HasTile(tilePosition))
            {
                Vector3 worldPos = groundTilemap.GetCellCenterWorld(tilePosition);

                // Check if worldPos is far enough from all occupied positions
                if (IsFarEnough(worldPos))
                {
                    // Place the prop
                    Instantiate(props[Random.Range(0, props.Length)], worldPos, Quaternion.identity);
                    occupiedPositions.Add(worldPos); // Track this position
                    placed++;
                }
            }

            attempts++;
        }
    }

    // Checks if newPos is far enough from all previously placed props
    bool IsFarEnough(Vector3 newPos)
    {
        foreach (Vector3 pos in occupiedPositions)
        {
            if (Vector3.Distance(newPos, pos) < minDistance)
                return false; // Too close to another prop
        }
        return true; // Safe to place
    }
}
