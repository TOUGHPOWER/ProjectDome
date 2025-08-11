using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Perks/IncreasedPlayerMaxHp")]
public class IncreasePlayerMaxHP : Perk
{
    public int addAmountMaxHP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Apply(PlayerInfo player, Building building = null)
    {
        player.AddMaxHP(addAmountMaxHP);
    }
}
