using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveToPlayer : MonoBehaviour
{
    public NavMeshAgent ai;
    public GameObject target;
    
    void Start ()
    {
        ai = GetComponent<NavMeshAgent>();
        
        try
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        catch
        {
            target = null;
        }
    }
    
    void Update ()
    {
        Movement();
    }

    private void Movement()
    {
        if (target)
            ai.destination = target.transform.position;
    }
}