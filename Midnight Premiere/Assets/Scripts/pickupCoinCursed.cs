using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupCoinCursed : MonoBehaviour
{
    public GameObject interact, monster, centertext;
    public AudioSource pickupSound, warningSound, spawnSound;
    public MeshRenderer coinMeshRenderer;
    public bool interactable;
    public Text intText, centerText;
    public string intString, centerString;
    public static int coinsCollected;
    public Transform dest, dest1;
    float delayBeforeWarning = 2f;
    float delayBeforeSpawn = 2f;
    float delayBeforeDespawn = 2f;
    public Shader interactableShader; // Reference to the interactable shader

    private Shader originalShader; // Original shader of the object
    private Renderer objectRenderer; // Renderer component of the object

    // private void Start()
    // {
    //     objectRenderer = GetComponent<Renderer>();
    //     originalShader = objectRenderer.material.shader;
    // }

    private void UpdateShader()
    {
        if (interactable)
        {
            objectRenderer.material.shader = interactableShader;
        }
        else
        {
            objectRenderer.material.shader = originalShader;
        }
    }
    
    private IEnumerator Start()
    {
        coinsCollected = 0;
        
        objectRenderer = GetComponent<Renderer>();
        originalShader = objectRenderer.material.shader;

        while (true)
        {
            if (interactable && Input.GetKeyDown(KeyCode.E))
            {
                coinsCollected = coinsCollected + 1;
                collectionSystem.amountCollected = collectionSystem.amountCollected + 1; // from collectCoin.cs
                
                pickupSound.Play();
                interact.SetActive(false);
                interactable = false;
                UpdateShader(); // Update the shader when interactable is true
                coinMeshRenderer.enabled = false;
                
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
                
                //These values change when a coin is picked up, making Slender more difficult.
                // slenderScript.drainRate = slenderScript.drainRate + 0.5f;
                // slenderScript.healthDamage = slenderScript.healthDamage + 5f;
                // slenderScript.audioIncreaseRate = slenderScript.audioIncreaseRate + 0.5f;
                
                // if (coinsCollected == 1)
                // {
                //
                // }
                //
                // if (coinsCollected == 2)
                // {
                //
                // }
                //
                // if (coinsCollected == 3)
                // {
                //
                // }
                //
                // if (coinsCollected == 4)
                // {
                //
                // }
                //
                // if (coinsCollected == 5)
                // {
                //
                // }
                //
                // if (coinsCollected == 6)
                // {
                //
                // }
                //
                // if (coinsCollected == 7)
                // {
                // 
                // }
                //
                // if (coinsCollected == 8)
                // {
                //
                // }
                //
                // if (coinsCollected == 9)
                // {
                //
                // }
                //
                // if (coinsCollected == 10)
                // {
                //
                // }
                
                this.gameObject.SetActive(false);
            }
            
            yield return null;
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interact.SetActive(true);
            interactable = true;
            intText.text = intString;
            UpdateShader(); // Update the shader when interactable is true
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interact.SetActive(false);
            interactable = false;
            UpdateShader(); // Update the shader when interactable is true
        }
    }
}
    
