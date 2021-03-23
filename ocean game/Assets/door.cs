using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public Transform Exit;
    public Transform Playerposition;
    public Vector2 ExitOffset;
    private Vector3 NewExit;
    public Player playerScript;
    public bool changeWorld;

    // Start is called before the first frame update
    void Start()
    {
        NewExit = new Vector3 (ExitOffset.x+Exit.position.x, ExitOffset.y+ Exit.position.y, 0f); 
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Piss");
            Playerposition.position = NewExit;
            if (changeWorld)
            {
                playerScript.SwapWorld();
                
            }

        }
            
    }

}
