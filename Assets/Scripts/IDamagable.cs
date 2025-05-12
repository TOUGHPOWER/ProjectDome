using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    void ReduceCurrentHP(float amountToReduce);
    void IncreaseCurrentHP(float amountToIncrease);
    void ReduceMaxHP(float amountToReduce);
    void IncreaseMaxHP(float amountToIncrease);

}
