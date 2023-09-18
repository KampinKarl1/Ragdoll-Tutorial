using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Wanderer : MonoBehaviour
{
    NavMeshAgent agent = null;

    [SerializeField] float wanderRadius = 12f;
    bool justArrived = false;

    float idleStart = 0;
    float timeToSpendIdle = 0;
    [SerializeField] float minStopTime = 5.0f;
    [SerializeField] float maxStopTime = 15.0f;

    [SerializeField] private bool checkIdleTime = false;

    private float timeIdle = 0;
    bool wasIdle = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        MoveToPosition(RandomSpotNearby());
    }

    void Update()
    {
        if (agent.remainingDistance > agent.stoppingDistance)
            return;

        if (checkIdleTime)
        {
            if (agent.velocity.sqrMagnitude < float.Epsilon)
            {
                if (!wasIdle)
                {
                    wasIdle = true;
                }

                timeIdle += Time.deltaTime;
            }
            else if (wasIdle)
            {
                wasIdle = false;
                timeIdle = 0;
            }
        }

        if (!justArrived) 
        {
            justArrived = true;
            idleStart = Time.time;
        }

        if (Time.time - idleStart > timeToSpendIdle)
        {
            timeToSpendIdle = Random.Range(minStopTime, maxStopTime);

            MoveToPosition(RandomSpotNearby());

            justArrived = false;
        }
    }

    private Vector3 RandomSpotNearby() 
    {
        Vector3 randomSpot = transform.position + Random.insideUnitSphere * wanderRadius;

        if (Physics.Raycast(randomSpot + Vector3.up * 25f, Vector3.down, out RaycastHit hit, 200f)) 
        {
            var path = new NavMeshPath();
            if (agent.CalculatePath(hit.point, path) && path.status == NavMeshPathStatus.PathComplete)
                return hit.point; 
        }

        return Vector3.zero;
    }


    public void MoveToPosition(Vector3 pos)
    {
        agent.SetDestination(pos);
        agent.isStopped = false;
    }

}
