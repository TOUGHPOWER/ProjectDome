using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Perk : ScriptableObject
{
    public string perkName;
    public string description;
    public Sprite icon;

    public abstract void Apply(PlayerInfo player, Building building = null);
}
