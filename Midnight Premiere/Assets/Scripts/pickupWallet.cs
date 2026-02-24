using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupWallet : MonoBehaviour
{
    public GameObject interact;
    public AudioSource pickupSound;
    public bool interactable;
    public Text intText;
    public string intString;
    public static int coinsCollected;
    public Shader interactableShader; // Reference to the interactable shader

    private Shader originalShader; // Original shader of the object
    private Renderer objectRenderer; // Renderer component of the object

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

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalShader = objectRenderer.material.shader;
        
        coinsCollected = 0;
    }
    
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

    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //coinsCollected = coinsCollected + 500;
                collectionSystem.amountCollected = collectionSystem.amountCollected + 500; // from collectCoin.cs
                
                this.gameObject.SetActive(false);
                pickupSound.Play();
                interact.SetActive(false);
                interactable = false;
                UpdateShader(); // Update the shader when interactable is false
            }
        }
    }
}
