using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{
    [Header("Class Variables")]

    public PlayerInfo playerInfo;

    private float lastCurrentHealth;
    [field: SerializeField] public float BuildingMaxHP { get; private set; }
    [field: SerializeField] public float currentHPPercentage { get; private set; }

    private bool nothing;
    [field: SerializeField] public float BuildCost {get; private set;}
    [field: SerializeField] public float BaseRepairCost { get; private set; }
    [field: SerializeField] public float RepairCost { get; private set; }
    [field: SerializeField] public float CurrentAmountDeposited { get; set; }
    
    [field: SerializeField] public bool IsBuilt { get; set; }
    [field: SerializeField] public bool PlayerInBuilding { get; set; }

    [Header("Others")]
    [SerializeField] public BuildingTypes BuildingType;
    [field: SerializeField] public Sprite BuildingSprite { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (BuildingType == BuildingTypes.ShieldGenerator)
        {
            Build();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Makes
        if(IsBuilt && CurrentHealth != lastCurrentHealth)
        {
            
            lastCurrentHealth = CurrentHealth;
            UpdateRepairCost();
        }

        if (PlayerInBuilding)
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

    private void TryBuying() 
    {

        if (!playerInfo.isDepositing) 
        {
            StartCoroutine(playerInfo.Depositing(this));

            if (IsBuilt)
            {
                if(BuildingType == BuildingTypes.ShieldGenerator)
                {
                    ShieldGenerator sg = gameObject.GetComponent<ShieldGenerator>();
                    if (currentHPPercentage < 100)
                    {
                        if (CurrentAmountDeposited >= RepairCost)//Repairs Building if it is already built
                        {
                            playerInfo.EnableDepositing(false);
                            Heal();
                            //Change Sprite
                            CurrentAmountDeposited = 0;
                        }
                    }
                    else if(CurrentAmountDeposited>= sg.upgradeCost)
                    {
                        sg.Upgrade();
                        CurrentAmountDeposited = 0;
                        playerInfo.EnableDepositing(false);
                    }
                    
                }
                else if (CurrentAmountDeposited >= RepairCost)
                {
                    //Repairs Building if it is already built
                    playerInfo.EnableDepositing(false);
                    Heal();
                    //Change Sprite
                    CurrentAmountDeposited = 0;
                }
            }
            else if (!IsBuilt) //Builds Building instead
            {
                if (CurrentAmountDeposited >= BuildCost)
                {
                    playerInfo.EnableDepositing(false);
                    Build();
                    CurrentAmountDeposited = 0;
                }
            }
        }
    }

    private void Build()
    {
        IsBuilt = true;
        SetupHealthValues(BuildingMaxHP);
    }



}
