using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class laserSystem : MonoBehaviour
{
    public static int lasersActive;
    public int finalAmount = 0;
    public Text laserText;
    public GameObject securityText, dialogueTrigger;
    public GameObject lasertext;
    public GameObject laserimage;
    
    void Start()
    {
        lasersActive = 3;
    }

    void Update()
    {
        if (lasersActive <= finalAmount)
        {
            securityText.SetActive(true);
            lasertext.SetActive(false);
            laserimage.SetActive(false);
            dialogueTrigger.SetActive(false);
        }

        laserText.text = lasersActive.ToString();
        
    }
}
