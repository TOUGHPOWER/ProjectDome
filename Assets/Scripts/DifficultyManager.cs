using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private float difficultyIncModifier;

    [SerializeField] private float modifierIncreaseValue;

    [SerializeField] private string[] difficulties;

    [SerializeField] private string currentDifficulty;

    //Timeframe in Minutes
    private (float min, float max) easyTimeframe = (0f, 2f);

    private (float min, float max) mediumTimeframe = (2.1f, 3f);

    private (float min, float max) hardTimeframe = (3.1f, Mathf.Infinity);

    private float elapsedTime;
    public string elapsedTimeStr {private set; get; }

    public int elapsedTimeHours { private set; get; }
    public int elapsedTimeMinutes { private set; get; }

    public int elapsedTimeSeconds { private set; get; }

    private int lastElapsedTimeMinutes;

    [SerializeField] List<GameObject> enemyPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        UpdateDificulty();
    }

    // Update is called once per frame
    void Update()
    {
        RunTimer();

        if(elapsedTimeMinutes != lastElapsedTimeMinutes)
        {
            lastElapsedTimeMinutes = elapsedTimeMinutes;
            UpdateDificulty();
        }

    }

    private void RunTimer()
    {
        elapsedTime += Time.deltaTime;

        elapsedTimeHours = Mathf.FloorToInt(elapsedTime / 3600f);
        elapsedTimeMinutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
        elapsedTimeSeconds = Mathf.FloorToInt(elapsedTime % 60f);
        elapsedTimeStr = string.Format("{0:00}:{1:00}:{2:00}", elapsedTimeHours, elapsedTimeMinutes, elapsedTimeSeconds);
    }

    private void UpdateDificulty()
    {
        if(elapsedTimeMinutes == easyTimeframe.min)
        {
            currentDifficulty = difficulties[0];

        }
        else if(elapsedTimeMinutes == mediumTimeframe.min)
        {
            currentDifficulty = difficulties[1];
            UpdateDifficultyModifier();
        }
        else if (elapsedTimeMinutes == hardTimeframe.min)
        {
            currentDifficulty = difficulties[2];
            UpdateDifficultyModifier();
        }
    }

    private void UpdateDifficultyModifier()
    {
        difficultyIncModifier += modifierIncreaseValue;
    }

    public void UpdateEnemyStats(BaseEnemy enemyScript)
    {
        enemyScript.MaxHealth = Convert.ToInt32(MathF.Round(enemyScript.MaxHealth * difficultyIncModifier));
        
        enemyScript.Agent.speed = Convert.ToInt32(MathF.Round(enemyScript.Agent.speed * difficultyIncModifier));

        enemyScript.Damage = Convert.ToInt32(MathF.Round(enemyScript.Damage * difficultyIncModifier));

    }


}
