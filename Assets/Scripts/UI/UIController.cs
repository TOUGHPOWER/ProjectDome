using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Building generatorBuilding;



    [Header("HP Bars")]
    [SerializeField] private Image reactorHpFill;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void UpdateHPBars()
    {
        reactorHpFill.fillAmount = generatorBuilding.currentHPPercentage / 10;
    }

   
}
