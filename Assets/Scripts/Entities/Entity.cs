using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    [field: SerializeField] public int CurrentHealth { get; set; }
    [field: SerializeField] public int MaxHealth { get; set; }

    [field: SerializeField] public List<ScriptableObject> CurrentPerks { get; set; }

    [field: SerializeField] public  int MaxPerkAmount { get; private set; }
    public virtual void SubtractCurrentHP(int amountToReduce)
    {
        if((CurrentHealth - amountToReduce) <= 0) 
        {
            CurrentHealth = 0;
        }
        else
        {
            CurrentHealth -= amountToReduce;
        }
        
    }
    public virtual void AddCurrentHP(int amountToIncrease)
    {
        if ((CurrentHealth + amountToIncrease) >= 100)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
            CurrentHealth += amountToIncrease;
        }
    }
    public virtual void SubtractMaxHP(int amountToReduce)
    {
        if ((MaxHealth - amountToReduce) <= 0)
        {
            MaxHealth = 10;
        }
        else
        {
            MaxHealth -= amountToReduce;
        }
    }
    public virtual void AddMaxHP(int amountToIncrease)
    {
        MaxHealth += amountToIncrease;
        Heal();
    }

    public virtual void SetupHealthValues(int buildingMaxHealth)
    {
        MaxHealth = buildingMaxHealth;
        print(MaxHealth);
        CurrentHealth = MaxHealth;
        print(CurrentHealth);
    }

    public void Heal()
    {
        CurrentHealth = MaxHealth;
    }

}
