using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectionSystem : MonoBehaviour
{
    public static int amountCollected;
    public int finalAmount;
    public Text collectionText;
    public GameObject triggerObj, triggerObj1, moneyText, dialogueTrigger;
    
    void Start()
    {
        amountCollected = 0;
    }
    
    void Update()
    {
        if (amountCollected >= finalAmount)
        {
            triggerObj.SetActive(true);
            triggerObj1.SetActive(false);
            moneyText.SetActive(true);
            dialogueTrigger.SetActive(false);
        }
        
        collectionText.text = amountCollected.ToString();
    }
}