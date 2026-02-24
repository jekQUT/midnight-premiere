using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camLookAt : MonoBehaviour
{
    public Transform destination;
    public Transform cam, player;
    public camSecure detectedCam;
    public slenderAI slenderAIscript;
    public enemyMonsterAI enemyMonsterAIscript;
    private bool camAlert = true;

    void OnBecameVisible()
    {
        camAlert = true;
    }

    void OnBecameInvisible()
    {
        camAlert = false;
    }

    void Start()
    {
        StartCoroutine(AlertCoroutine());
    }

    void Update()
    {
        cam.LookAt(player);
        //transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    IEnumerator AlertCoroutine()
    {
        while (camAlert)
        {
            if (detectedCam.detected)
            {
                slenderAIscript.transform.position = destination.position;
                enemyMonsterAIscript.dest = destination.position;
            }
            yield return null;
        }
    }
}