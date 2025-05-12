using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [field:SerializeField] public int maxNumEnergy { get; set; }
    [field: SerializeField] public int currentNumEnergy { get; set; }
    [field: SerializeField] public int maxNumCrystals { get; set; }
    [field: SerializeField] public int currentNumCrystals { get; set; }
    [field: SerializeField] public int currentHealth { get; set; }
    [field: SerializeField] public int maxHealth { get; set;}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
