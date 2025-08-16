using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour
{
    private Building buildingSystem;

    [SerializeField] int amountEnergyPerRegen;
    [SerializeField] int secondsToRegen;

    //public RuntimeAnimatorController extractorAnim;
    // Start is called before the first frame update
    void Awake()
    {
        buildingSystem = GetComponent<Building>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ProduceEnergy()
    {
        while (true)
        {
            if (buildingSystem.IsBuilt)
            {
                buildingSystem.playerInfo.AddEnergy(amountEnergyPerRegen);
                yield return new WaitForSeconds(secondsToRegen);
            }
            else
            {
                break;
            }

        }
    }
}
