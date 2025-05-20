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
        if (parentBuilding.isBuilt)
        {
            float CurrentRepairCost = parentBuilding.RepairCost - parentBuilding.currentAmountDeposited;
            buildingCostText.text = CurrentRepairCost.ToString();
        }
        else if (!parentBuilding.isBuilt)
        {
            float CurrentBuildCost = parentBuilding.BuildCost - parentBuilding.currentAmountDeposited;
            buildingCostText.text = CurrentBuildCost.ToString();
        }

    }
}
