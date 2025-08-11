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

    private (float min, float max) easyTimeframe = (0f, 3f);

    private (float min, float max) mediumTimeframe = (3f, 6f);

    private (float min, float max) hardTimeframe = (6f, Mathf.Infinity);

    private double[] difficultyTimeFrames;

    private float elapsedTime;
    public string elapsedTimeStr {private set; get; }
    public int elapsedTimeMinutes { private set; get; }

    private int lastElapsedTimeMinute;

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

        if(elapsedTimeMinutes != lastElapsedTimeMinute)
        {
            lastElapsedTimeMinute = elapsedTimeMinutes;
            UpdateDificulty();
        }

    }

    private void RunTimer()
    {
        elapsedTime += Time.deltaTime;

        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        elapsedTimeMinutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        elapsedTimeStr = string.Format("{0:00}:{1:00}:{2:00}", hours, elapsedTimeMinutes, seconds);
    }

    private void UpdateDificulty()
    {
        if(elapsedTimeMinutes >= easyTimeframe.min && elapsedTimeMinutes < easyTimeframe.max)
        {
            currentDifficulty = difficulties[0];

        }
        else if(elapsedTimeMinutes >= mediumTimeframe.min && elapsedTimeMinutes < mediumTimeframe.max)
        {
            currentDifficulty = difficulties[1];
        }
        else if (elapsedTimeMinutes >= hardTimeframe.min && elapsedTimeMinutes < hardTimeframe.max)
        {
            currentDifficulty = difficulties[2];
        }


    }

    private void UpdateEnemyDifficulty()
    {
        foreach (GameObject enemy in enemyPrefabs)
        {
            BaseEnemy enemyScript = enemy.GetComponent<BaseEnemy>();

            enemyScript.startingMaxHealth = Convert.ToInt32(MathF.Round(enemyScript.startingMaxHealth * difficultyIncModifier));
            enemyScript.agent.speed = Convert.ToInt32(MathF.Round(enemyScript.agent.speed * difficultyIncModifier));
            if (enemyScript)
            {
                
            }
        }
    }


}
