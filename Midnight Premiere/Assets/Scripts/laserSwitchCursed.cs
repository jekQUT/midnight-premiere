using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class laserSwitchCursed : MonoBehaviour
{
    public Animator switchAnim;
    public GameObject lasers, interaction, monster, centertext, childObject;
    public AudioSource warningSound, spawnSound;
    public bool interactable, toggle;
    public Text intText, centerText;
    public string intString, centerString;
    public static int lasersActive;
    public Transform dest, dest1;
    float delayBeforeWarning = 2f;
    float delayBeforeSpawn = 2f;
    float delayBeforeDespawn = 2f;
    private Shader originalShader;
    private Renderer childRenderer;
    public Shader newShader;

    private void UpdateShader()
    {
        if (interactable)
        {
            childRenderer.material.shader = newShader;
        }
        else
        {
            childRenderer.material.shader = originalShader;
        }
    }
    
    private IEnumerator Start()
    {
        // Start interacting with the switch
        lasersActive = 3;
        
        childRenderer = childObject.GetComponent<Renderer>();
        originalShader = childRenderer.material.shader;

        while (true)
        {
            if (interactable && Input.GetKeyDown(KeyCode.E))
            {
                laserSystem.lasersActive = laserSystem.lasersActive - 1;

                switchAnim.SetTrigger("pull");
                lasers.SetActive(false);
                interaction.SetActive(false);
                toggle = true;
                interactable = false;
                UpdateShader();
                
                yield return new WaitForSeconds(delayBeforeWarning);
                
                centertext.SetActive(true);
                centerText.text = centerString;
                warningSound.Play();
                
                yield return new WaitForSeconds(delayBeforeSpawn);

                centertext.SetActive(false);
                spawnSound.Play();
                
                yield return new WaitForSeconds(delayBeforeSpawn);
                
                monster.transform.position = dest.position;
                
                yield return new WaitForSeconds(delayBeforeDespawn);

                monster.transform.position = dest1.position;
            }
            else
            {
                UpdateShader();
            }

            yield return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (toggle == false)
            {
                interaction.SetActive(true);
                interactable = true;
                intText.text = intString;
                UpdateShader();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interaction.SetActive(false);
            interactable = false;
            UpdateShader();
        }
    }
}
