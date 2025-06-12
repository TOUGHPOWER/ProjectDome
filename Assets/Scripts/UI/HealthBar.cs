using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    //[SerializeField] private Building building;
    private PlayerInfo playerInfo;
    private Building generator;
    private Shield shield;

    [SerializeField] private EntityType entity;

     private Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        GetRespectiveRef();
        healthBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    private void GetRespectiveRef()
    {
        if (entity == EntityType.Player)
        {
            playerInfo = FindObjectOfType<PlayerInfo>();
        }
        else if (entity == EntityType.Generator)
        {
            generator = FindObjectOfType<ShieldGenerator>().GetComponent<Building>();
        }
        else if (entity == EntityType.Shield)
        {
            shield = FindObjectOfType<Shield>();
        }
    }

    private void UpdateHealthBar()
    {
        if (entity == EntityType.Player)
        {
            SetupHealthBarValues(playerInfo);
        }
        else if (entity == EntityType.Generator)
        {
            SetupHealthBarValues(generator);
        }
        else if (entity == EntityType.Shield)
        {
            SetupHealthBarValues(shield);
        }
    }

    private void SetupHealthBarValues(Entity theEntity)
    {
        healthBar.maxValue = theEntity.MaxHealth;
        healthBar.value = theEntity.CurrentHealth;
    }
    


}
