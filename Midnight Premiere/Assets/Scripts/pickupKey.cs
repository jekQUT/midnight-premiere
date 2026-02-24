using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupKey : MonoBehaviour
{
    public GameObject interaction, key, keyimage, childObject, childObject2, childObject3, childObject4;
    public AudioSource pickup;
    public bool interactable;
    public Text intText;
    public string intString;
    private Shader originalShader, originalShader2, originalShader3, originalShader4;
    private Renderer childRenderer, childRenderer2, childRenderer3, childRenderer4;
    public Shader newShader, newShader2, newShader3, newShader4;

    void Start()
    {
        childRenderer = childObject.GetComponent<Renderer>();
        originalShader = childRenderer.material.shader;
        
        childRenderer2 = childObject2.GetComponent<Renderer>();
        originalShader2 = childRenderer2.material.shader;
        
        childRenderer3 = childObject3.GetComponent<Renderer>();
        originalShader3 = childRenderer3.material.shader;
        
        childRenderer4 = childObject4.GetComponent<Renderer>();
        originalShader4 = childRenderer4.material.shader;
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
                pickup.Play();
                key.SetActive(false);
                keyimage.SetActive(true);
            }
            // Change shader on child object
            childRenderer.material.shader = newShader;
            childRenderer2.material.shader = newShader2;
            childRenderer3.material.shader = newShader3;
            childRenderer4.material.shader = newShader4;
        }
        else
        {
            // Change shader back to original
            childRenderer.material.shader = originalShader;
            childRenderer2.material.shader = originalShader2;
            childRenderer3.material.shader = originalShader3;
            childRenderer4.material.shader = originalShader4;
        }
    }
}
