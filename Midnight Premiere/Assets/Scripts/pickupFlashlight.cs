using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupFlashlight : MonoBehaviour
{
    public GameObject interaction, flashlight_table, flashlight_hand, childObject, childObject2;
    public AudioSource pickup;
    public bool interactable;
    public Text intText;
    public string intString;
    private Shader originalShader, originalShader2;
    private Renderer childRenderer, childRenderer2;
    public Shader newShader, newShader2;

    void Start()
    {
        childRenderer = childObject.GetComponent<Renderer>();
        originalShader = childRenderer.material.shader;
        
        childRenderer2 = childObject2.GetComponent<Renderer>();
        originalShader2 = childRenderer2.material.shader;
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interaction.SetActive(true);
            interactable = true;
            intText.text = intString;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interaction.SetActive(false);
            interactable = false;
        }
    }
    
    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interaction.SetActive(false);
                interactable = false;
                //pickup.Play();
                flashlight_hand.SetActive(true);
                flashlight_table.SetActive(false);
            }
            // Change shader on child object
            childRenderer.material.shader = newShader;
            childRenderer2.material.shader = newShader2;
        }
        else
        {
            // Change shader on child object
            childRenderer.material.shader = originalShader;
            childRenderer2.material.shader = originalShader2;
        }
    }
}
