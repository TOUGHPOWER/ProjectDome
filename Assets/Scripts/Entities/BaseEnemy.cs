using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus;
using System.IO;
using System;

public class BaseEnemy : Entity
{
    
    [field:SerializeField] public int startingMaxHealth { set; get; }

    [Header("AI")]
    [SerializeField] private List<EntityType> targetPreferences;

    [SerializeField] private GameObject currentTarget;
    

    [SerializeField] private float distanceToAttack;
    [SerializeField] float detectionRadius = 5f;

    [field: SerializeField] public NavMeshAgent agent { private set; get; }
    [SerializeField] float secondsToUpdateTarget;
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        SetupHealthValues(startingMaxHealth);
        StartCoroutine(UpdateTarget());
    }

    // Update is called once per frame
    void Update()
    {
        //CheckDistanceToAttack();
        

    }

    private bool CheckDistanceToAttack()
    {
        if (!agent.isStopped && agent.remainingDistance <= distanceToAttack )
        {
            return true;
        }
        else
        {
            agent.SetDestination(currentTarget.transform.position);
            return false;
        }

        
    }

    private void Attack()
    {
        if (CheckDistanceToAttack() == true)
        {
            agent.isStopped = true;
            //Do attack here
        }
        
    }

    private float GetPathLength(NavMeshPath path)
    {
        float length = 0f;

        for (int i = 1; i < path.corners.Length; i++)
        {
            length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }

        return length;
    }

    private void GetClosestTarget()
    {
        float shortestDistance = Mathf.Infinity;
        NavMeshPath pathToTarget = new NavMeshPath();
        GameObject bestTarget = null;

        foreach (EntityType type in targetPreferences)
        {
            Target[] candidates = GameObject.FindObjectsOfType<Target>();
            foreach (Target candidate in candidates)
            {
                if (candidate.targetType != type) continue;

                Vector3 targetPosition;
                Rigidbody2D rb = candidate.GetComponent<Rigidbody2D>();
                targetPosition = rb != null ? rb.position : candidate.transform.position;

                NavMeshHit hit;
                if (!NavMesh.SamplePosition(targetPosition, out hit, detectionRadius, NavMesh.AllAreas)) continue;

                Vector3 sampledPosition = hit.position;
                if (!NavMesh.CalculatePath(agent.transform.position, sampledPosition, NavMesh.AllAreas, pathToTarget)) continue;
                if (pathToTarget.status != NavMeshPathStatus.PathComplete) continue;

                float distance = GetPathLength(pathToTarget);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    bestTarget = candidate.gameObject;
                    agent.SetDestination(sampledPosition);
                }
            }

            if (bestTarget != null)
            {
                currentTarget = bestTarget;
                return; // stop after finding the best valid target of highest priority
            }
        }

    }

    IEnumerator UpdateTarget()
    {
        while (true)
        {
            GetClosestTarget();
            yield return new WaitForSeconds(secondsToUpdateTarget);
        }
    }

    
}
