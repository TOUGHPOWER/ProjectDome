using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    [field: SerializeField] public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public List<ScriptableObject> Perks { get; set; }
    public virtual void ReduceCurrentHP(float amountToReduce)
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
    public virtual void IncreaseCurrentHP(float amountToIncrease)
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
    public virtual void ReduceMaxHP(float amountToReduce)
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
    public virtual void IncreaseMaxHP(float amountToIncrease)
    {
        MaxHealth += amountToIncrease;
    }

    public virtual void SetupHealthValues(float buildingMaxHealth)
    {
        MaxHealth = buildingMaxHealth;
        print(MaxHealth);
        CurrentHealth = MaxHealth;
        print(CurrentHealth);
    }

}
