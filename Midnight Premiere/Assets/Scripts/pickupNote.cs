using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class pickupNote : MonoBehaviour
{
    public bool interactable, toggle;
    public GameObject interact, noteimage, notetext;
    public AudioSource pickupSound;
    public MeshRenderer noteMeshRenderer;
    public string intString;
    public string noteString;
    public Text intText;
    public Text noteText;
    public float noteTime;
    public Shader interactableShader; // Reference to the interactable shader

    private Shader originalShader; // Original shader of the object
    private Renderer objectRenderer; // Renderer component of the object

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalShader = objectRenderer.material.shader;
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
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (toggle == false)
            {
                interact.SetActive(true);
                interactable = true;
                intText.text = intString;
                UpdateShader(); // Update the shader when interactable is true
            }
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

    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickupSound.Play();
                noteText.text = noteString;
                noteimage.SetActive(true);
                notetext.SetActive(true);
                interact.SetActive(false);
                StartCoroutine(disableDialogue());
                toggle = true;
                interactable = false;
                noteMeshRenderer.enabled = false;
                UpdateShader(); // Update the shader when interactable is true
            }
        }
    }

    IEnumerator disableDialogue()
    {
        yield return new WaitForSeconds(noteTime);
        noteimage.SetActive(false);
        notetext.SetActive(false);
    }
}
