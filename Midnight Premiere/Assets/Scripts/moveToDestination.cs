using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveToDestination : MonoBehaviour
{
    public NavMeshAgent ai;
    public Animator aiAnim;
    int randNum;
    public Transform aiTrans, randDest1, randDest2, randDest3, randDest4, randDest5, randDest6, randDest7, randDest8;
    public bool walking, idle;
    public Vector3 dest;
    public float idleTime;

    void Start()
    {
        walking = true;
        randNum = Random.Range(0, 8);
        aiAnim.SetTrigger("walk");
        SetRandomDestination();
    }

    void Update()
    {
        if (walking)
        {
            // AI is walking to the destination
            ai.destination = dest;
            ai.speed = 2;

            // Check if AI has reached the current destination
            if (!ai.pathPending && ai.remainingDistance <= ai.stoppingDistance)
            {
                if (!ai.hasPath || ai.velocity.sqrMagnitude == 0f)
                {
                    // AI has reached the current destination
                    SetRandomDestination();
                }
            }
        }
        else if (idle)
        {
            // AI is idle
            ai.speed = 0;
        }
    }

    void SetRandomDestination()
    {
        randNum = Random.Range(0, 8);

        if (randNum == 0)
        {
            dest = randDest1.position;
        }
        else if (randNum == 1)
        {
            dest = randDest2.position;
        }
        else if (randNum == 2)
        {
            dest = randDest3.position;
        }
        else if (randNum == 3)
        {
            dest = randDest4.position;
        }
        else if (randNum == 4)
        {
            dest = randDest5.position;
        }
        else if (randNum == 5)
        {
            dest = randDest6.position;
        }
        else if (randNum == 6)
        {
            dest = randDest7.position;
        }
        else if (randNum == 7)
        {
            dest = randDest8.position;
        }

        // Start walking after setting the destination
        walking = true;
        idle = false;

        aiAnim.ResetTrigger("idle");
        aiAnim.SetTrigger("walk");

        StartCoroutine("nextDest"); // Start the coroutine to determine the next destination
    }

    IEnumerator nextDest()
    {
        // Wait until the enemy reaches the current destination
        while (Vector3.Distance(ai.transform.position, dest) > 0.1f)
        {
            yield return null;
        }

        // Set the AI state to idle before setting the new random destination
        walking = false;
        idle = true;

        aiAnim.ResetTrigger("walk");
        aiAnim.SetTrigger("idle");

        yield return new WaitForSeconds(idleTime); // Wait for the idle time

        SetRandomDestination(); // Set the new random destination
    }
}
