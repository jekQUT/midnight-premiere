using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupBattery : MonoBehaviour
{
    public GameObject interact;
    public AudioSource pickupSound;
    public string intString;
    public Text intText;
    public bool interactable;
    public flashlight flashlightScript;
    
    public Shader interactableShader; // Reference to the shader to use when interactable is true
    private Renderer renderer;
    private Shader originalShader;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        originalShader = renderer.material.shader;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interact.SetActive(true);
            interactable = true;
            intText.text = intString;
            
            if (interactableShader != null)
            {
                renderer.material.shader = interactableShader;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interact.SetActive(false);
            interactable = false;
            
            // Reset the shader to the original shader
            renderer.material.shader = originalShader;
        }
    }

    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickupSound.Play();
                flashlightScript.batteryLife = 100f;
                interact.SetActive(false);
                this.gameObject.SetActive(false);
                interactable = false;
            }
        }
    }
}