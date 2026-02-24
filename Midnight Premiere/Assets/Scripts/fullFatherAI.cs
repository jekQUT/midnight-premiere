using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class fullFatherAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public Animator aiAnim;
    public Transform[] fixedDestinations;
    private int currentDestinationIndex = 0;
    public bool walking, idle;
    public float idleTime;

    public AudioSource chaseMusic;
    public Transform playerTrans, aiTrans;
    public bool chasing;
    public Vector3 dest;
    public float chaseTime;
    public float sightLostDuration; // Duration to continue chasing after losing sight of the player
    private float timeSinceLastSight; // Time since losing sight of the player
    public float detectionAngle = 90f; // Angle within which the enemy can detect the player
    
    void Start()
    {
        walking = true;
        aiAnim.SetTrigger("walk");
        SetNextDestination();
    }

    void Update()
    {
        if (chasing)
        {
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
            ai.destination = fixedDestinations[currentDestinationIndex].position;
            ai.speed = 2;
            
            // Check if player is visible
            if (IsPlayerVisible())
            {
                // Start chasing the player
                StartChasingPlayer();
                return; // Exit the update method to avoid continuing to walk towards the destination
            }

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
        chasing = false;
        idle = false;

        aiAnim.ResetTrigger("idle");
        aiAnim.SetTrigger("walk");

        StartCoroutine("NextDest"); // Start the coroutine to determine the next destination
    }
    
    void StartChasingPlayer()
    {
        chasing = true;
        walking = false;
        idle = false;

        aiAnim.ResetTrigger("idle");
        aiAnim.ResetTrigger("walk");
        aiAnim.SetTrigger("run");

        chaseMusic.Play();
        
        StopCoroutine("NextDest");
        StopCoroutine("chase");
        StartCoroutine("chase");
    }
    
    void StopChasingPlayer()
    {
        chasing = false;
        walking = true;
        idle = false;
        
        chaseMusic.Stop();
        
        aiAnim.ResetTrigger("idle");
        aiAnim.ResetTrigger("run");
        aiAnim.SetTrigger("walk");
        
        SetNextDestination();
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
        chasing = false;
        idle = true;

        aiAnim.ResetTrigger("walk");
        aiAnim.SetTrigger("idle");

        yield return new WaitForSeconds(idleTime); // Wait for the idle time

        SetNextDestination(); // Set the next destination in the series
    }

    IEnumerator chase()
    {
        yield return new WaitForSeconds(chaseTime);
        
        StopChasingPlayer();
    }
}