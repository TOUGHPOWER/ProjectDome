using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    private Building parentBuilding;

    [SerializeField] TextMeshProUGUI buildingCostText;
    [SerializeField] Image statusIcon;
    [SerializeField] Sprite repairIcon;
    [SerializeField] Sprite upgradeIcon;
    [SerializeField] Sprite buildIcon;


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
        UpdateStatusIcon();
    }

    private void UpdateCostText()
    {
        float currentRepairCost = parentBuilding.RepairCost - parentBuilding.CurrentAmountDeposited;
        float currentBuildCost = parentBuilding.BuildCost - parentBuilding.CurrentAmountDeposited;
        

        if (parentBuilding.BuildingType == BuildingTypes.Reactor)
        {
            float currentUpgCost = parentBuilding.GetComponent<Reactor>().upgradeCost - parentBuilding.CurrentAmountDeposited;
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

    private void UpdateStatusIcon()
    {

        if (!parentBuilding.IsBuilt)
        {
            statusIcon.gameObject.SetActive(true);
            statusIcon.sprite = buildIcon;
        }
        else if(parentBuilding.currentHPPercentage < 100)
        {
            statusIcon.gameObject.SetActive(true);
            statusIcon.sprite = repairIcon;
        }
        else
        {
            statusIcon.gameObject.SetActive(false);
        }

        if (parentBuilding.BuildingType == BuildingTypes.Reactor && parentBuilding.currentHPPercentage >= 100)
        {
            statusIcon.gameObject.SetActive(true);
            statusIcon.sprite = upgradeIcon;
        }
    }
}
