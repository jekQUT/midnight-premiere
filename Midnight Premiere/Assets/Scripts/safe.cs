using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class safe : MonoBehaviour
{
    public GameObject safecode, numtext, incorrecttext, correcttext, interaction, childObject, childObject2, childObject3, childObject4;
    public SC_FPSController playerscript;
    public Animator safeOpen;
    public Text numTex;
    public Text intText;
    public string intString;
    public string codeString, correctCode;
    public int stringCharacters = 0;
    public bool interactable, codeDone, safeactive;
    public Button but1, but2, but3, but4, but5, but6, but7, but8, but9, but0;
    private int token = 0;
    public Rigidbody playerRigid;
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
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (codeDone == false)
            {
                interaction.SetActive(true);
                interactable = true;
                intText.text = intString;
            }
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
                safecode.SetActive(true);
                playerRigid.constraints = RigidbodyConstraints.FreezeAll;
                playerscript.enabled = false;
                safeactive = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                interactable = false;
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

        if (safeactive == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                numtext.SetActive(true);
                correcttext.SetActive(false);
                incorrecttext.SetActive(false);
                stringCharacters = 0;
                codeString = "";
                but1.interactable = true;
                but2.interactable = true;
                but3.interactable = true;
                but4.interactable = true;
                safeactive = false;
                but5.interactable = true;
                but6.interactable = true;
                but7.interactable = true;
                token = 0;
                but8.interactable = true;
                but9.interactable = true;
                but0.interactable = true;
                safecode.SetActive(false);
                playerRigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX |
                                          RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ |
                                          RigidbodyConstraints.FreezeRotationX;
                playerscript.enabled = true;
                interactable = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            numTex.text = codeString;

            if (stringCharacters == 4)
            {
                if (codeString == correctCode)
                {
                    numtext.SetActive(false);
                    correcttext.SetActive(true);
                    but1.interactable = false;
                    but2.interactable = false;
                    but3.interactable = false;
                    but4.interactable = false;
                    but5.interactable = false;
                    but6.interactable = false;
                    but7.interactable = false;
                    but8.interactable = false;
                    but9.interactable = false;
                    but0.interactable = false;
                    codeDone = true;
                    if (token == 0)
                    {
                        safeOpen.SetTrigger("open");
                        StartCoroutine(endSesh());
                        token = 1;
                    }
                }
                else
                {
                    numtext.SetActive(false);
                    incorrecttext.SetActive(true);
                    but1.interactable = false;
                    but2.interactable = false;
                    but3.interactable = false;
                    but4.interactable = false;
                    but5.interactable = false;
                    but6.interactable = false;
                    but7.interactable = false;
                    but8.interactable = false;
                    but9.interactable = false;
                    but0.interactable = false;
                    if (token == 0)
                    {
                        StartCoroutine(endSesh());
                        token = 1;
                    }
                }
            }
        }
    }

    IEnumerator endSesh()
    {
        yield return new WaitForSeconds(2.5f);
        numtext.SetActive(true);
        correcttext.SetActive(false);
        incorrecttext.SetActive(false);
        stringCharacters = 0;
        codeString = "";
        but1.interactable = true;
        but2.interactable = true;
        but3.interactable = true;
        but4.interactable = true;
        safeactive = false;
        but5.interactable = true;
        but6.interactable = true;
        but7.interactable = true;
        token = 0;
        but8.interactable = true;
        but9.interactable = true;
        but0.interactable = true;
        safecode.SetActive(false);
        playerRigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX |
                                  RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |
                                  RigidbodyConstraints.FreezeRotationX;
        playerscript.enabled = true;
        interactable = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void pressedOne()
    {
        codeString = codeString + "1";
        stringCharacters = stringCharacters + 1;
    }

    public void pressedTwo()
    {
        codeString = codeString + "2";
        stringCharacters = stringCharacters + 1;
    }

    public void pressedThree()
    {
        codeString = codeString + "3";
        stringCharacters = stringCharacters + 1;
    }

    public void pressedFour()
    {
        codeString = codeString + "4";
        stringCharacters = stringCharacters + 1;
    }

    public void pressedFive()
    {
        codeString = codeString + "5";
        stringCharacters = stringCharacters + 1;
    }

    public void pressedSix()
    {
        codeString = codeString + "6";
        stringCharacters = stringCharacters + 1;
    }

    public void pressedSeven()
    {
        codeString = codeString + "7";
        stringCharacters = stringCharacters + 1;
    }

    public void pressedEight()
    {
        codeString = codeString + "8";
        stringCharacters = stringCharacters + 1;
    }

    public void pressedNine()
    {
        codeString = codeString + "9";
        stringCharacters = stringCharacters + 1;
    }

    public void pressedZero()
    {
        codeString = codeString + "0";
        stringCharacters = stringCharacters + 1;
    }
}
    
