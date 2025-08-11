using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public List<Perk> activePerks = new List<Perk>();

    public void AddPerk(Perk newPerk, PlayerInfo player, Building building = null)
    {
        newPerk.Apply(player, building);
        activePerks.Add(newPerk);
    }


}
