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
    [SerializeField] bool canDeposit;


    // Start is called before the first frame update
    void Start()
    {
        SetupHealthValues(BaseMaxHealth);
        EnableDepositing(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Depositing(Building targetBuilding)
    {
        if (canDeposit)
        {
            isDepositing = true;
            while ((Input.GetButton("Jump") && isDepositing) && canDeposit)
            {
                CurrentNumEnergy -= 1;
                targetBuilding.currentAmountDeposited += 1;
                yield return new WaitForSeconds(despositRate);
            }

            isDepositing = false;
        }
        


    }

    public void EnableDepositing(bool canPlayerDeposit)
    {
        canDeposit = canPlayerDeposit;
    }
    
}
