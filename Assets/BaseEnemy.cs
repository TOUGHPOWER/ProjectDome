using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus;
using System.IO;

public class BaseEnemy : Entity
{
    [Header("Class variables")]
    [SerializeField] private float startingMaxHealth;

    [SerializeField] private List<GameObject> targets;
    [SerializeField] private GameObject currentTarget;

    [SerializeField] private float distanceToAttack;
    [SerializeField] float detectionRadius = 5f;

    private NavMeshAgent agent;
    [SerializeField] float secondsToUpdateTarget;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
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
        

        foreach (GameObject target in targets)
        {
            Vector3 targetPosition;

            // Get the Rigidbody2D position if available, else use Transform
            Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
            if (rb != null)
                targetPosition = rb.position;
            else
                targetPosition = target.transform.position;

            // Ensure the position is actually on the NavMesh
            NavMeshHit hit;
            if (!NavMesh.SamplePosition(targetPosition, out hit, detectionRadius, NavMesh.AllAreas))
            {
                Debug.LogWarning($"Target {target.name} is not near a valid NavMesh area.");
                continue;
            }

            Vector3 sampledPosition = hit.position;

            // Calculate path to the sampled position
            if (NavMesh.CalculatePath(agent.transform.position, sampledPosition, NavMesh.AllAreas, pathToTarget))
            {
                if (pathToTarget.status != NavMeshPathStatus.PathComplete)
                {
                    Debug.LogWarning($"Path to {target.name} is not complete.");
                    agent.SetDestination(sampledPosition);
                    continue;
                }

                float distance = GetPathLength(pathToTarget);
                Debug.Log($"Valid path to {target.name}, path length: {distance}");

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    currentTarget = target;
                    agent.SetDestination(sampledPosition);

                }
            }
            else
            {
                Debug.LogWarning($"Failed to calculate path to {target.name}");
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
