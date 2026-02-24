using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveToFixedDestinations : MonoBehaviour
{
    public NavMeshAgent ai;
    public Animator aiAnim;
    public Transform[] fixedDestinations;
    private int currentDestinationIndex = 0;
    public bool walking, idle;
    public float idleTime;

    void Start()
    {
        walking = true;
        aiAnim.SetTrigger("walk");
        SetNextDestination();
    }

    void Update()
    {
        if (walking)
        {
            // AI is walking to the destination
            ai.destination = fixedDestinations[currentDestinationIndex].position;
            ai.speed = 2;

            // Check if AI has reached the current destination
            if (!ai.pathPending && ai.remainingDistance <= ai.stoppingDistance)
            {
                if (!ai.hasPath || ai.velocity.sqrMagnitude == 0f)
                {
                    // AI has reached the current destination
                    SetNextDestination();
                }
            }
        }
        else if (idle)
        {
            // AI is idle
            ai.speed = 0;
        }
    }

    void SetNextDestination()
    {
        currentDestinationIndex++;

        // If all destinations have been visited, reset to the first destination
        if (currentDestinationIndex >= fixedDestinations.Length)
        {
            currentDestinationIndex = 0;
        }

        // Start walking after setting the destination
        walking = true;
        idle = false;

        aiAnim.ResetTrigger("idle");
        aiAnim.SetTrigger("walk");

        StartCoroutine("NextDest"); // Start the coroutine to determine the next destination
    }

    IEnumerator NextDest()
    {
        // Wait until the enemy reaches the current destination
        while (Vector3.Distance(ai.transform.position, fixedDestinations[currentDestinationIndex].position) > 0.1f)
        {
            yield return null;
        }

        // Set the AI state to idle before setting the new destination
        walking = false;
        idle = true;

        aiAnim.ResetTrigger("walk");
        aiAnim.SetTrigger("idle");

        yield return new WaitForSeconds(idleTime); // Wait for the idle time

        SetNextDestination(); // Set the next destination in the series
    }
}