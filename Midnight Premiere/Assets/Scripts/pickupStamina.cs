using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupStamina : MonoBehaviour
{
    public GameObject interact;
    public AudioSource pickupSound;
    public string intString;
    public Text intText;
    public bool interactable;
    public SC_FPSController staminaScript;

    // Highlighting interactable objects
    public Color highlightColor;
    private Color originalColor;
    private Renderer objectRenderer;

    // Shader for interactable objects
    public Shader interactableShader;
    private Shader originalShader;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        originalShader = objectRenderer.material.shader;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interact.SetActive(true);
            interactable = true;
            intText.text = intString;
            objectRenderer.material.shader = interactableShader;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interact.SetActive(false);
            interactable = false;
            objectRenderer.material.shader = originalShader;
        }
    }

    void Update()
    {
        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickupSound.Play();
                staminaScript.staminaLife = 100f;
                interact.SetActive(false);
                gameObject.SetActive(false);
                interactable = false;
            }
        }
    }
}