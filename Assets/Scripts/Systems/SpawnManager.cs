using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] DifficultyManager difficultyManager;

    [SerializeField] List<Spawner> availableSpawners;

    [SerializeField] bool isSpawning;

    [SerializeField] float spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        difficultyManager = GetComponent<DifficultyManager>();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemies()
    {
        isSpawning = true;
        while (isSpawning)
        {
            
            foreach (Spawner spawner in availableSpawners)
            {
                if (spawner.recentSpawnedEnemy != null)
                {
                    difficultyManager.UpdateEnemyStats(spawner.recentSpawnedEnemy.GetComponent<BaseEnemy>());
                }
                
            }
            availableSpawners[Random.Range(0,availableSpawners.Count)].Spawn();
            
            yield return new WaitForSeconds(spawnRate);
        }
    }


}
