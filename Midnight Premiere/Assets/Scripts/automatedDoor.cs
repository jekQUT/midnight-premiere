using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class automatedDoor : MonoBehaviour
{
    public Animator doorAnim;
    public AudioSource doorSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            doorAnim.SetTrigger("open");
            doorSound.Play();
        }
    }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Monster"))
    //     {
    //         doorAnim.SetTrigger("close");
    //     }
    // }
}
