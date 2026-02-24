using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class hidingPlace : MonoBehaviour
{
    public GameObject interact;
    public GameObject stopHideText;
    public AudioSource hidingSound;
    public GameObject normalPlayer, hidingPlayer;
    public enemyMonsterAIraycast monsterScript;
    public Transform monsterTransform;
    bool interactable, hiding;
    public Text intText;
    public string intString;
    public float loseDistance;
    public GameObject walkFootsteps, runFootsteps; // so i can mute them when hiding
    public Shader interactableShader; // Reference to the interactable shader

    private Shader originalShader; // Original shader of the object
    private Renderer objectRenderer; // Renderer component of the object
    
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalShader = objectRenderer.material.shader;
        
        interactable = false;
        UpdateShader(); // Update the shader when interactable is true
        hiding = false;
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

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            //hideText.SetActive(true);
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

    void Update()
    {
        if(interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hidingSound.Play();
                interact.SetActive(false);
                interactable = false;
                intText.text = "";
                hidingPlayer.SetActive(true);
                float distance = Vector3.Distance(monsterTransform.position, normalPlayer.transform.position);
                if(distance > loseDistance)
                {
                    if(monsterScript.chasing == true)
                    {
                        monsterScript.StopChasingPlayer();
                    }
                }
                stopHideText.SetActive(true);
                hiding = true;
                normalPlayer.SetActive(false);
                UpdateShader(); // Update the shader when interactable is true
            }
        }
        
        if(hiding == true)
        {
            interact.SetActive(false);
            walkFootsteps.SetActive(false);
            runFootsteps.SetActive(false);
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                interact.SetActive(true);
                interactable = true;
                intText.text = intString;
                stopHideText.SetActive(false);
                normalPlayer.SetActive(true);
                hidingPlayer.SetActive(false);
                hiding = false;
                walkFootsteps.SetActive(true);
                runFootsteps.SetActive(true);
                UpdateShader(); // Update the shader when interactable is true
            }
        }
    }
}