using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class lookAtSlender : MonoBehaviour
{
    public RawImage staticImage;
    public Color color;
    public float drainRate, rechargeRate, health, healthDamage, healthRechargeRate, maxStaticAmount;
    public float audioIncreaseRate, audioDecreaseRate;
    public bool canRecharge;
    public AudioSource staticSound;
    public string deathScene;
    public Slider healthSlider;
    public raycastSlender detectedScript;
    public Transform slenderTransform, playerTransform;

    void Start()
    {
        color.a = 0f;
        healthSlider.maxValue = 100f;
        health = 100f;
    }

    void Update()
    {
        Vector3 targetPosition = playerTransform.position;
        targetPosition.y = slenderTransform.position.y; // Maintain the same y position

        Vector3 direction = targetPosition - slenderTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Apply the side angle adjustment (90-degree angle)
        float sideAngle = -90f;
        targetRotation *= Quaternion.Euler(0f, sideAngle, 0f);

        slenderTransform.rotation = targetRotation;

        healthSlider.value = health;

        if (health <= 50f)
        {
            canRecharge = true;
        }
        else
        {
            canRecharge = false;
        }

        if (color.a > maxStaticAmount)
        {

        }

        else if (color.a < maxStaticAmount)
        {
            staticImage.color = color;
        }

        if (detectedScript.detected == true)
        {
            color.a = color.a + drainRate * Time.deltaTime;
            health = health - healthDamage * Time.deltaTime;
            staticSound.volume = staticSound.volume + audioIncreaseRate * Time.deltaTime;
        }

        if (detectedScript.detected == false)
        {
            color.a = 0f;

            if (canRecharge == true)
            {
                health = health + healthRechargeRate * Time.deltaTime;
            }

            staticSound.volume = staticSound.volume - audioDecreaseRate * Time.deltaTime;
        }

        if (health < 1)
        {
            SceneManager.LoadScene(deathScene);
        }
    }
}
