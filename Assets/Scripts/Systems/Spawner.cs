using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesToSpawn;

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
        Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)],transform.position, Quaternion.identity);
    }
}
