using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    private Building parentBuilding;

    [SerializeField] TextMeshProUGUI buildingCostText;


    // Start is called before the first frame update
    void Start()
    {
        parentBuilding = GetComponentInParent<Building>();
        buildingCostText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCostText();
    }

    private void UpdateCostText()
    {
        float currentRepairCost = parentBuilding.RepairCost - parentBuilding.CurrentAmountDeposited;
        float currentBuildCost = parentBuilding.BuildCost - parentBuilding.CurrentAmountDeposited;
        

        if (parentBuilding.BuildingType == BuildingTypes.ShieldGenerator)
        {
            float currentUpgCost = parentBuilding.GetComponent<ShieldGenerator>().upgradeCost - parentBuilding.CurrentAmountDeposited;
            if (parentBuilding.currentHPPercentage < 100)
            {
                buildingCostText.text = currentRepairCost.ToString();
            }
            else
            {
                buildingCostText.text = currentUpgCost.ToString();
            }

        }
        else if (parentBuilding.IsBuilt)
        {
            buildingCostText.text = currentRepairCost.ToString();
        }
        else if (!parentBuilding.IsBuilt)
        {
            buildingCostText.text = currentBuildCost.ToString();
        }

        if (buildingCostText.text == "0")
        {
            buildingCostText.text = "";
        }
        else if(int.Parse(buildingCostText.text) < 0)
        {
            buildingCostText.text = "";
        }

    }
}
