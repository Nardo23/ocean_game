using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPosition : MonoBehaviour
{
    [SerializeField]
    Transform[] positions;
    float timer = 0;
    int i = 0;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 5)
        {
            i = Random.Range(0, positions.Length);
            Debug.Log("index: "+i);
            transform.position = positions[i].position;
            timer = 0;
        }
        

    }



}
