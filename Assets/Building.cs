using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{

    public PlayerInfo playerInfo;

    private float lastCurrentHealth;
    [field: SerializeField] public float BuildingMaxHP { get; private set; }
    [SerializeField] float currentHPPercentage;

    [field: SerializeField] public float BuildCost {get; private set;}
    [field: SerializeField] public float BaseRepairCost { get; private set; }
    [field: SerializeField] public float RepairCost { get; private set; }
    [field: SerializeField] public float currentAmountDeposited { get; set; }
    [field: SerializeField] public bool isBuilt { get; set; }
    [field: SerializeField] public bool isInBuilding { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isBuilt && CurrentHealth != lastCurrentHealth)
        {
            
            lastCurrentHealth = CurrentHealth;
            UpdateRepairCost();
        }

        if (isInBuilding)
        {
            if (Input.GetButton("Jump"))
            {
                TryBuying();
            }
            
            
        }
        
    }

    private void UpdateRepairCost()
    {
        currentHPPercentage = (CurrentHealth / MaxHealth) * 100;
        print($"Current HP %:{ currentHPPercentage}");

        float RepairCostMultiplier = 0;
       
        if (currentHPPercentage < 1)
        {
            RepairCostMultiplier = 2;
        }
        else if (currentHPPercentage < 100)
        {
            RepairCostMultiplier = float.Parse($"1.{100 - currentHPPercentage}");
        }
        
        print($"Current R.C.M:{RepairCostMultiplier}");
        RepairCost = Mathf.Round(BaseRepairCost * RepairCostMultiplier);
        print($"Current Repair Cost:{RepairCost}");

    }

    public void TryBuying() 
    {
        if (!playerInfo.isDepositing) 
        {
            StartCoroutine(playerInfo.Depositing(this));
            if (isBuilt)
            {
                if (currentAmountDeposited >= RepairCost)
                {
                    IncreaseCurrentHP(BuildingMaxHP);
                    //Change Sprite
                }
            }
            else if (!isBuilt)
            {
                if (currentAmountDeposited >= BuildCost)
                {
                    isBuilt = true;
                    SetupHealthValues(BuildingMaxHP);
                    currentAmountDeposited = 0;
                }
            }
        }



    }



}
