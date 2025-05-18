using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : Entity
{
    [SerializeField] float despositRate = 1f;

    [field:SerializeField] public int MaxNumEnergy { get; set; }
    [field: SerializeField] public int CurrentNumEnergy { get; set; }
    [field: SerializeField] public int MaxNumCrystals { get; set; }
    [field: SerializeField] public int CurrentNumCrystals { get; set; }
    [field: SerializeField] public float CurrentHP { get; set; }
    [field: SerializeField] public float PlayerMaxHP { get => base.MaxHealth;}

    [field: SerializeField] public bool isDepositing { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        SetupHealthValues(PlayerMaxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Depositing(Building targetBuilding)
    {
        isDepositing = true;
        while (Input.GetButton("Jump"))
        {
            CurrentNumCrystals -= 1;
            targetBuilding.currentAmountDeposited += 1;
            yield return new WaitForSeconds(despositRate);
        }
        

    }
    
}
