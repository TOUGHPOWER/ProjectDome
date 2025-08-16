using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reactor : MonoBehaviour
{

    [Header("Refs")]
    private Building buildingSystem;
    [field: SerializeField] public GameObject shieldObj { get; private set; }
    [SerializeField] SceneLoader sceneLoader;

    [Header("Generator Upgrading")]
    [SerializeField] public int currentUpgradeLevel;
    [SerializeField] public int maxUpgradeLevel;
    [field: SerializeField] public int upgradeCost { get; private set;}
    [field: SerializeField] public float upgCostIncreaseModifier { get; private set; }
    [SerializeField] private int upgGenMaxHPAmount;

    [Header("Shield Upgrading")]
    [SerializeField] private float upgradeScaleModifier;
    [SerializeField] private int upgShieldMaxHPAmount;
    // Start is called before the first frame update
    void Start()
    {
        buildingSystem = GetComponent<Building>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buildingSystem.CurrentHealth <= 0)
        {
            sceneLoader.LoadLoseScene();
            print("Load Lose Scene");
        }
    }

    public void Upgrade()
    {
        Shield shield = shieldObj.GetComponent<Shield>();
        currentUpgradeLevel++;
        if(currentUpgradeLevel >= maxUpgradeLevel)
        {
            sceneLoader.LoadWinMenu();
        }
        
        //Increase Generator Max HP and Heal to Max
        buildingSystem.IncreaseMaxHP(upgGenMaxHPAmount);

        //Increase Shield Max HP and Heal to Max
        shield.IncreaseMaxHP(upgShieldMaxHPAmount);

        //Upgrade Shield Radius/Scale
        shield.transform.localScale = new Vector2 (shield.transform.localScale.x*upgradeScaleModifier, shield.transform.localScale.y * upgradeScaleModifier);

        //Grants Player Upgrade
        //Make upgrade menu appear

        upgradeCost = Mathf.RoundToInt(upgradeCost * upgCostIncreaseModifier);


    }

}
