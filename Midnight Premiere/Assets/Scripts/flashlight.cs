using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flashlight : MonoBehaviour
{
    public GameObject lightObject;
    public bool toggle;
    public AudioSource toggleSound;

    public float batteryLife, batteryDrainRate;
    public Slider batterySlider;

    void Start()
    {
        batterySlider.maxValue = 100f;
        batteryLife = 100f;
        batteryDrainRate = 5f;

        if (!toggle)
        {
            lightObject.SetActive(false);
        }
        else
        {
            lightObject.SetActive(true);
        }
    }

    void Update()
    {
        batterySlider.value = batteryLife;

        if (Input.GetKeyDown(KeyCode.F))
        {
            toggle = !toggle;
            
            if (!toggle)
            {
                lightObject.SetActive(false);
                toggleSound.Play();
            }
            else
            {
                lightObject.SetActive(true);
            }
        }

        if (toggle && batteryLife > 0f)
        {
            batteryLife -= batteryDrainRate * Time.deltaTime;
        }

        if (batteryLife <= 0f)
        {
            batteryLife = 0f;
            lightObject.SetActive(false);
        }
    }
}