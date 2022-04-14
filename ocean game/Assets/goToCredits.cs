using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToCredits : MonoBehaviour
{
    public bool skipToCred = false;
   

    // Update is called once per frame
    void Update()
    {
        if (skipToCred)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(2);
        }
    }
}
