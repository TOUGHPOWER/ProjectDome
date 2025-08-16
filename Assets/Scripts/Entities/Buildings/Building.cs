using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Building : Entity
{
    [Header("Class Variables")]

    public PlayerInfo playerInfo;

    [SerializeField] private float lastCurrentHealth;
    [field: SerializeField] public int BuildingMaxHP { get; private set;}
    [field: SerializeField] public float currentHPPercentage { get; private set;}

    [field: SerializeField] public int BuildCost {get; private set;}
    [field: SerializeField] public int BaseRepairCost { get; private set;}
    [field: SerializeField] public int RepairCost { get; private set;}
    [field: SerializeField] public int CurrentAmountDeposited { get; set;}

    [field: SerializeField] public bool isShielded { get; set; }
    [field: SerializeField] public bool IsBuilt { get; set; }
    [field: SerializeField] public bool needsRepairs { get; set; }
    [field: SerializeField] public bool PlayerInBuilding { get; set; }

    

    [Header("Others")]
    [SerializeField] public BuildingTypes BuildingType;
    [SerializeField] private GameObject buildingSlot;
    [SerializeField] private SpriteRenderer buildingSR;
    [SerializeField] private Sprite currentBuildingSprite;
    [SerializeField] private Sprite builtSprite;
    [SerializeField] private Sprite destroyedSprite;
    [SerializeField] private Color notShieldedColor;

    // Start is called before the first frame update
    void Start()
    {
        Build();
        playerInfo = FindObjectOfType<PlayerInfo>();
        buildingSR = gameObject.GetComponent<SpriteRenderer>();
        buildingSR.sprite = currentBuildingSprite;
        if (BuildingType == BuildingTypes.Reactor)
        {
            isShielded = true;
            Build();
        }

        if (!isShielded)
        {
            buildingSlot.GetComponent<SpriteRenderer>().color = notShieldedColor;
        }

        UpdateRepairCost();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShielded)
        {
            if (IsBuilt && CurrentHealth != lastCurrentHealth)
            {
                lastCurrentHealth = CurrentHealth;

                
                if (CurrentHealth < MaxHealth)
                {
                    UpdateRepairCost();
                }
                else if (CurrentHealth <= 0)
                {
                    IsBuilt = false;
                    //Display Destroyed Sprite
                    currentBuildingSprite = destroyedSprite;
                    buildingSR.sprite = destroyedSprite;
                    Debug.Log("I was destroyed");
                }
                else if(currentHPPercentage >= 100)
                {
                    needsRepairs = false;
                }

            }

            
            if (PlayerInBuilding && !playerInfo.isDepositing && playerInfo.canDeposit)
            {
                StartCoroutine(playerInfo.Depositing(this));
            }

            buildingSlot.GetComponent<SpriteRenderer>().color = Color.white;

        }
    }
    
    private void UpdateRepairCost()
    {
        currentHPPercentage = MathF.Round(((float) CurrentHealth / (float) MaxHealth) * 100);
        print($"Current HP %:{currentHPPercentage}");

        float RepairCostMultiplier = 0;
       
        if (currentHPPercentage < 10)
        {
            RepairCostMultiplier = 2;
            needsRepairs = true;
        }
        else if (currentHPPercentage < 100)
        {
            RepairCostMultiplier = float.Parse($"1.{100 - currentHPPercentage}");
            needsRepairs = true;
        }
        
        //print($"Current R.C.M:{RepairCostMultiplier}");

        RepairCost = Convert.ToInt32(MathF.Round(BaseRepairCost * RepairCostMultiplier));

        //print($"Current Repair Cost:{RepairCost}");

    }

    

    public void Build()
    {
        IsBuilt = true;
        SetupHealthValues(BuildingMaxHP);
        currentBuildingSprite = builtSprite;
        buildingSR.sprite = builtSprite;
        gameObject.AddComponent<Target>();

        if(BuildingType == BuildingTypes.Reactor)
        {
            gameObject.GetComponent<Target>().targetType = EntityType.Reactor;
        }
        else
        {
            gameObject.GetComponent<Target>().targetType = EntityType.Building;
        }

        if(BuildingType == BuildingTypes.Extractor)
        {
            StartCoroutine(GetComponent<Extractor>().ProduceEnergy());
            GetComponent<Animator>().SetBool("ExtractorBuilt", true);
            //GetComponent<Animator>().runtimeAnimatorController = GetComponent<Extractor>().extractorAnim;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Shield"))
        {
            Debug.Log("Collided with Shield");
            isShielded = true;
        }
    }

}
