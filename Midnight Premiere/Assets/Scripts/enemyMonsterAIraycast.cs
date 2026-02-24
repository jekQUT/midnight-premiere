using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMonsterAIraycast : MonoBehaviour
{
    public AudioSource chaseMusic;
    public NavMeshAgent ai;
    public Animator aiAnim;
    int randNum;
    public Transform playerTrans, aiTrans, randDest1, randDest2, randDest3, randDest4, randDest5, randDest6, randDest7, randDest8;
    public bool walking, chasing, idle;
    public Vector3 dest;
    public float chaseTime, idleTime;
    public float sightLostDuration; // Duration to continue chasing after losing sight of the player
    private float timeSinceLastSight; // Time since losing sight of the player

    public float detectionAngle = 90f; // Angle within which the enemy can detect the player

    void Start()
    {
        walking = true;
        randNum = Random.Range(0, 8);
        aiAnim.SetTrigger("walk");
        SetRandomDestination();
    }

    void Update()
    {
        if (chasing)
        {
            // AI is already chasing the player
            dest = playerTrans.position;
            ai.destination = dest;
            ai.speed = 4;

            // Check if player is still visible
            if (!IsPlayerVisible())
            {
                timeSinceLastSight += Time.deltaTime;

                // Stop chasing if sight is lost for too long
                if (timeSinceLastSight >= sightLostDuration)
                {
                    StopChasingPlayer();
                }
            }
            else
            {
                // Reset the timer if player is still visible
                timeSinceLastSight = 0f;
            }
        }
        else if (walking)
        {
            // AI is walking to the destination
            ai.destination = dest;
            ai.speed = 2;

            // Check if player is visible
            if (IsPlayerVisible())
            {
                StartChasingPlayer();
                return; // Exit the update method to avoid continuing to walk towards the destination
            }

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

    bool IsPlayerVisible()
    {
        Vector3 directionToPlayer = playerTrans.position - aiTrans.position;
        float angle = Vector3.Angle(aiTrans.forward, directionToPlayer);

        // Check if the player is within the detection angle and there are no obstacles blocking the view
        if (angle <= detectionAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(aiTrans.position, directionToPlayer, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
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
        chasing = false;
        idle = false;

        aiAnim.ResetTrigger("idle");
        aiAnim.SetTrigger("walk");

        StartCoroutine("nextDest"); // Start the coroutine to determine the next destination
    }

    void StartChasingPlayer()
    {
        chasing = true;
        walking = false;
        idle = false;

        //aiAnim.ResetTrigger("idle");
        aiAnim.ResetTrigger("walk");
        //aiAnim.SetTrigger("run");
        
        aiAnim.SetTrigger("walk");
        
        chaseMusic.Play();

        StopCoroutine("nextDest");
        //StopCoroutine("chase");
        StartCoroutine("chase");
    }

    public void StopChasingPlayer()
    {
        chasing = false;
        walking = true;
        idle = false;

        chaseMusic.Stop();

        aiAnim.ResetTrigger("idle");
        //aiAnim.ResetTrigger("run");
        aiAnim.SetTrigger("walk");

        SetRandomDestination();
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
        chasing = false;
        idle = true;

        aiAnim.ResetTrigger("walk");
        aiAnim.SetTrigger("idle");

        yield return new WaitForSeconds(idleTime); // Wait for the idle time

        SetRandomDestination(); // Set the new random destination
    }

    IEnumerator chase()
    {
        yield return new WaitForSeconds(chaseTime);

        StopChasingPlayer();
    }
}
