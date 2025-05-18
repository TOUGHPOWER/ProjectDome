using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{
    public PlayerInfo playerInfo;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRepairCost();

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
        float RepairCostMultiplier = float.Parse("1." + currentHPPercentage);
        RepairCost = (int) Mathf.Round((RepairCost * RepairCostMultiplier) / 100);
    }

    public void TryBuying() 
    {
        StartCoroutine(playerInfo.Depositing(this));
        if (isBuilt)
        {
            
            if (currentAmountDeposited >= RepairCost)
            {
                
            }
        }
        else if(!isBuilt)
        {
            if (currentAmountDeposited >= BuildCost)
            {

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
