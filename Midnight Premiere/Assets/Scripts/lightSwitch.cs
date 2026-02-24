using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lightSwitch : MonoBehaviour
{
    public GameObject interact, light, childObject;
    public bool toggle = true, interactable;
    public Renderer lightBulb;
    public Material offLight, onLight;
    public AudioSource lightSwitchSound;
    public Text intText;
    public string intString;
    private Shader originalShader;
    private Renderer childRenderer;
    public Shader newShader;
    
    void Start()
    {
        childRenderer = childObject.GetComponent<Renderer>();
        originalShader = childRenderer.material.shader;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interact.SetActive(true);
            interactable = true;
            intText.text = intString;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interact.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                toggle = !toggle;
                lightSwitchSound.Play();
            }
            // Change shader on child object
            childRenderer.material.shader = newShader;
        }
        else
        {
            // Revert back to the original shader
            childRenderer.material.shader = originalShader;
        }

        if (toggle == false)
        {
            light.SetActive(false);
            lightBulb.material = offLight;
        }

        if (toggle == true)
        {
            light.SetActive(true);
            lightBulb.material = onLight;
        }
        
    }
}
