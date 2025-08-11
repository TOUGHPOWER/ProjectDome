using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : Entity
{
    
    private float currentHP => CurrentHealth;
    private float maxHP => MaxHealth;
    [field: SerializeField] public int BaseMaxHealth { get; set; }

    [field:SerializeField] public int MaxNumEnergy { get; set; }
    [field: SerializeField] public int CurrentNumEnergy { get; set; }
    [field: SerializeField] public int MaxNumCrystals { get; set; }
    [field: SerializeField] public int CurrentNumCrystals { get; set; }

    [SerializeField] float despositRate = 1f;
    [field: SerializeField] public bool isDepositing { get; set; }
    [field: SerializeField] public bool canDeposit { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        SetupHealthValues(BaseMaxHealth);
        canDeposit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentNumEnergy <= 0)
        {
            CurrentNumEnergy = 0;

            canDeposit = false;
        }
        


    }

    public IEnumerator Depositing(Building targetBuilding)
    {
        isDepositing = true;

        while (canDeposit && targetBuilding.PlayerInBuilding)
        {
            if (CurrentNumEnergy <= 0)
            {
                break;
            }

            CurrentNumEnergy -= 1;
            targetBuilding.CurrentAmountDeposited += 1;

            // Check if enough has been deposited
            if (targetBuilding.IsBuilt)
            {
                if (targetBuilding.BuildingType == BuildingTypes.Reactor)
                {
                    Reactor reactor = targetBuilding.GetComponent<Reactor>();
                    if (targetBuilding.currentHPPercentage < 100 &&
                        targetBuilding.CurrentAmountDeposited >= targetBuilding.RepairCost)
                    {
                        targetBuilding.FullHPHeal();
                        targetBuilding.CurrentAmountDeposited = 0;
                        break;
                    }
                    else if (targetBuilding.CurrentAmountDeposited >= reactor.upgradeCost)
                    {
                        reactor.Upgrade();
                        targetBuilding.CurrentAmountDeposited = 0;
                        break;
                    }
                }
                else if (targetBuilding.CurrentAmountDeposited >= targetBuilding.RepairCost)
                {
                    targetBuilding.FullHPHeal();
                    targetBuilding.CurrentAmountDeposited = 0;
                    break;
                }
            }
            else if (!targetBuilding.IsBuilt && targetBuilding.CurrentAmountDeposited >= targetBuilding.BuildCost)
            {
                targetBuilding.Build();
                targetBuilding.CurrentAmountDeposited = 0;
                break;
            }

            yield return new WaitForSeconds(despositRate);
        }

        canDeposit = false;
        isDepositing = false;
    }

    public void DisableDepositing(Building thebuilding)
    {
        print("hello");
        canDeposit = false;
        print("Stop Deposit");
        StopAllCoroutines();
        
    }

    public void AddEnergy(int amountToAdd)
    {
        CurrentNumEnergy += amountToAdd;
    }
    
}
