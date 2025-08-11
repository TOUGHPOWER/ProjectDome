using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public abstract class Entity: MonoBehaviour
{
    [field: SerializeField] public int CurrentHealth { get; set; }
    [field: SerializeField] public int MaxHealth { get; set; }

    [field: SerializeField] public List<ScriptableObject> CurrentPerks { get; set; }

    [field: SerializeField] public  int MaxPerkAmount { get; private set; }

    [field: SerializeField] public EntityType entityType { get; private set; }

    private Vector2 RespawnLocation;

    [SerializeField] private Animator entAnimator;
    [SerializeField] private GameObject onDeathDropObject;
    [SerializeField] private GameObject dieVFX;
    [SerializeField] private SceneLoader sceneLoader;
    private void Start()
    {
        entAnimator = GetComponent<Animator>();
        RespawnLocation = transform.position;
        sceneLoader = FindAnyObjectByType<SceneLoader>();
    }

    public virtual void SubtractCurrentHP(int amountToReduce)
    {
        entAnimator.SetTrigger("GetHit");
        CurrentHealth -= amountToReduce;
        if (CurrentHealth <= 0) 
        {

            Die();
            
        }
    }
    public virtual void AddCurrentHP(int amountToIncrease)
    {
        if ((CurrentHealth + amountToIncrease) >= 100)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
            CurrentHealth += amountToIncrease;
        }
    }
    public virtual void SubtractMaxHP(int amountToReduce)
    {
        if ((MaxHealth - amountToReduce) <= 0)
        {
            Die();
        }
        else
        {
            MaxHealth -= amountToReduce;
        }
    }
    public virtual void AddMaxHP(int amountToIncrease)
    {
        MaxHealth += amountToIncrease;
        FullHPHeal();
    }

    public virtual void SetupHealthValues(int entityMaxHealth)
    {
        MaxHealth = entityMaxHealth;
        print(MaxHealth);
        FullHPHeal();
        print(CurrentHealth);
        
    }

    public void FullHPHeal()
    {
        CurrentHealth = MaxHealth;
    }

    public void Respawn()
    {
        transform.position = RespawnLocation;
        FullHPHeal();
    }

    public void DisableEntity()
    {
        
        switch (entityType)
        {
            case EntityType.Building:
                gameObject.SetActive(false);
                break;
            case EntityType.Shield:
                gameObject.SetActive(false);
                break;
            case EntityType.Reactor:
                gameObject.SetActive(false);
                break;

        }
    }

    public void EnableEntity()
    {
        gameObject.SetActive(true);

        switch (entityType)
        {
            case EntityType.Building:
                SetupHealthValues(MaxHealth);
                break;
            case EntityType.Shield:
                SetupHealthValues(MaxHealth);
                break;
            case EntityType.Reactor:
                SetupHealthValues(MaxHealth);
                break;

        }
    }

    public void Die()
    {
        if (!(entityType == EntityType.Building || entityType == EntityType.Reactor || entityType == EntityType.Shield || entityType == EntityType.Player))
        {
            Instantiate(dieVFX, transform.position, Quaternion.identity);

            Instantiate(onDeathDropObject, transform.position, Quaternion.identity);
        }
        


        switch (entityType)
        {
            case EntityType.Enemy:
                Destroy(gameObject);
                break;
            case EntityType.Player:
                Respawn();
                break;
            case EntityType.Building:
                DisableEntity();
                break;
            case EntityType.Shield:
                DisableEntity();
                break;
            case EntityType.Reactor:
                sceneLoader.LoadLoseScene();
                break;

        }
        
    }

}
