using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour
{

    [Header("Refs")]
    [SerializeField] private Building buildingSystem;
    [SerializeField] private Shield shield;

    [Header("Generator Upgrading")]
    [SerializeField] private int maxUpgradeLevel;
    [field: SerializeField] public float upgradeCost { get; private set;}
    [field: SerializeField] public float upgCostIncreaseModifier { get; private set; }
    [SerializeField] private float upgGenMaxHPAmount;

    [Header("Shield Upgrading")]
    [SerializeField] private float upgradeScaleModifier;
    [SerializeField] private float upgShieldMaxHPAmount;
    // Start is called before the first frame update
    void Start()
    {
        buildingSystem = GetComponent<Building>();
        shield = GetComponentInChildren<Shield>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade()
    {
        //Increase Generator Max HP and Heal to Max
        buildingSystem.AddMaxHP(upgGenMaxHPAmount);

        //Increase Shield Max HP and Heal to Max
        shield.AddMaxHP(upgShieldMaxHPAmount);

        //Upgrade Shield Radius/Scale
        shield.transform.localScale = new Vector2 (shield.transform.localScale.x*upgradeScaleModifier, shield.transform.localScale.y * upgradeScaleModifier);

        //Grants Player Upgrade
        //Make upgrade menu appear

        upgradeCost = upgradeCost * upgCostIncreaseModifier;
    }

}
