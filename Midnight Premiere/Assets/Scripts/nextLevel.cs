using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    public string sceneName, sceneName1, sceneName2;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectionSystem.amountCollected < 500)
            {
                SceneManager.LoadScene(sceneName);
            }
            else if (collectionSystem.amountCollected < 9000)
            {
                SceneManager.LoadScene(sceneName1);
            }
            else if (collectionSystem.amountCollected >= 9000)
            {
                SceneManager.LoadScene(sceneName2);
            }
        }
    }
}
