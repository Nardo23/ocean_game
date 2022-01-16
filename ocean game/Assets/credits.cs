using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
    public Animator anim;
    public Animator sandAnim;
    [SerializeField]
    GameObject[] creditsObj;
    public GameObject playtesters;
    int objCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        advanceCredits();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.Play("texRinse");
            
        }



    }

    public void advanceCredits()
    {
        if(objCount< creditsObj.Length)
        {
            foreach (GameObject obj in creditsObj)
            {
                obj.SetActive(false);


            }
            creditsObj[objCount].SetActive(true);

            if (objCount <= 3)
            {
                playtesters.SetActive(true);
            }
            else
            {
                playtesters.SetActive(false);
            }
            objCount++;
        }

    }




}
