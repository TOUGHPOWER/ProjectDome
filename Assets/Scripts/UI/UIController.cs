using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Reactor reactor;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private DifficultyManager difficultyManager;

    [SerializeField] private TextMeshProUGUI reactorLevelText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("HP Bars")]
    [SerializeField] private Image reactorHpFill;

    [Header("Currencies")]
    [SerializeField] private TextMeshProUGUI energyText;


    // Start is called before the first frame update
    void Start()
    {
        playerInfo = FindAnyObjectByType<PlayerInfo>();
        reactor = FindAnyObjectByType<Reactor>();

    }

    // Update is called once per frame
    void Update()
    {
        energyText.text = playerInfo.CurrentNumEnergy.ToString();
        UpdateLevels();
        UpdateTimer();
    }

    private void UpdateLevels()
    {
        reactorLevelText.text = $"Level Reached for Restoration: {reactor.currentUpgradeLevel} / {reactor.maxUpgradeLevel}";
    }

    private void UpdateTimer()
    {
        timerText.text = difficultyManager.elapsedTimeStr;
    }

   
}
