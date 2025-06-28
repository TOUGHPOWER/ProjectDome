using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Building : Entity
{
    [Header("Class Variables")]

    public PlayerInfo playerInfo;

    private float lastCurrentHealth;
    [field: SerializeField] public int BuildingMaxHP { get; private set;}
    [field: SerializeField] public float currentHPPercentage { get; private set;}

    [field: SerializeField] public int BuildCost {get; private set;}
    [field: SerializeField] public int BaseRepairCost { get; private set;}
    [field: SerializeField] public int RepairCost { get; private set;}
    [field: SerializeField] public int CurrentAmountDeposited { get; set;}

    [field: SerializeField] public bool isShielded { get; set; }
    [field: SerializeField] public bool IsBuilt { get; set; }
    [field: SerializeField] public bool PlayerInBuilding { get; set; }

    

    [Header("Others")]
    [SerializeField] public BuildingTypes BuildingType;
    [SerializeField] private Sprite builtSprite;
    [SerializeField] private Sprite destroyedSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (BuildingType == BuildingTypes.Reactor)
        {
            isShielded = true;
            Build();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isShielded)
        {
            if (IsBuilt && CurrentHealth != lastCurrentHealth)
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

        RepairCost = Convert.ToInt32(Mathf.Round(BaseRepairCost * RepairCostMultiplier));

        print($"Current Repair Cost:{RepairCost}");

    }

    /// <summary>
    /// Checks the type of buying the player is doing (Reparing / Building / Upgrading)
    /// </summary>
    private void TryBuying() 
    {

        if (!playerInfo.isDepositing) 
        {
            StartCoroutine(playerInfo.Depositing(this));

            if (IsBuilt)
            {
                if(BuildingType == BuildingTypes.Reactor)
                {
                    Reactor sg = gameObject.GetComponent<Reactor>();
                    if (currentHPPercentage < 100)
                    {
                        if (CurrentAmountDeposited >= RepairCost) //Repairs Building if it is already built
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            Debug.Log("Collided with: " + collision.gameObject.name);
            isShielded = true;
        }
    }

}
