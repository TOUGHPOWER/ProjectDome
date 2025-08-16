using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [field:SerializeField] public List<GameObject> EnemiesToSpawn {get; private set; }

    public GameObject recentSpawnedEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        recentSpawnedEnemy = Instantiate(EnemiesToSpawn[Random.Range(0, EnemiesToSpawn.Count)],transform.position, Quaternion.identity);
    }
}
