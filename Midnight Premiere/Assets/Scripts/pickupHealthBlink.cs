using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupHealthBlink : MonoBehaviour
{
    public GameObject interact;
    public AudioSource pickupSound;
    public string intString;
    public Text intText;
    public bool interactable;
    public lookAtSlender playerHealthScript;
    public Shader interactableShader; // Reference to the interactable shader

    private Shader originalShader; // Original shader of the object
    private Renderer objectRenderer; // Renderer component of the object

    private Coroutine shaderSwapCoroutine; // Coroutine reference for the shader swapping loop

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalShader = objectRenderer.material.shader;
        UpdateShader();
    }

    private void UpdateShader()
    {
        if (interactable)
        {
            objectRenderer.material.shader = interactableShader;
        }
        else
        {
            if (shaderSwapCoroutine == null)
            {
                shaderSwapCoroutine = StartCoroutine(SwapShaders());
            }
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
            UpdateShader(); // Update the shader when interactable is false
        }
    }
    
    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            pickupSound.Play();
            playerHealthScript.health = 100f;
            interact.SetActive(false);
            gameObject.SetActive(false);
            interactable = false;
            UpdateShader(); // Update the shader when interactable is false
        }
    }

    IEnumerator SwapShaders()
    {
        while (!interactable)
        {
            objectRenderer.material.shader = interactableShader;
            yield return new WaitForSeconds(0.5f);
            objectRenderer.material.shader = originalShader;
            yield return new WaitForSeconds(0.5f);
        }

        // Reset the shader to original when interaction becomes true
        objectRenderer.material.shader = originalShader;
        shaderSwapCoroutine = null;
    }
}
