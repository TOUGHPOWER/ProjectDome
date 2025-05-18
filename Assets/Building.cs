using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{
    public PlayerInfo playerInfo;

    private float lastCurrentHealth;
    [field: SerializeField] public float StartingMaxHealth { get; private set; }
    [SerializeField] public float currentHPPercentage;

    [field: SerializeField] public float BuildCost {get; private set;}
    [field: SerializeField] public float RepairCost { get; private set; }
    [field: SerializeField] public float currentAmountDeposited { get; set; }

    [SerializeField] bool isBuilt;
    [SerializeField] bool isInBuilding;

    // Start is called before the first frame update
    void Start()
    {
        SetupHealthValues(StartingMaxHealth);

        print(CurrentHealth);
        print(MaxHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth != lastCurrentHealth)
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
        currentHPPercentage = CurrentHealth / MaxHealth;
        print(currentHPPercentage);
        float RepairCostMultiplier = 1f + currentHPPercentage;
        print(RepairCostMultiplier);
        RepairCost = Mathf.Round(RepairCost * RepairCostMultiplier);
        print(RepairCost);
        
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

                }
            }
            else if (!isBuilt)
            {
                if (currentAmountDeposited >= BuildCost)
                {

                }
            }
        }

        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInBuilding = true;
        }
    }



}
