using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class demoManager : MonoBehaviour
    {
        int visitCount;
        public GameObject Cam;
        public GameObject camHolder;
        Animator anim;
        public GameObject player;
        Player playerScript;
        public GameObject canvas;
        public GameObject drawMap;
        // Start is called before the first frame update
        void Start()
        {
            anim = camHolder.GetComponent<Animator>();
            playerScript = player.GetComponent<Player>();
            //endDemo();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void IncreaseVisitCount()
        {
            visitCount += 1;

            if (visitCount >= 2)
            {
                endDemo();
            }


        }

        
        void endDemo()
        {
            playerScript.canMove = false;
            canvas.SetActive(false);
            drawMap.SetActive(false);
            camHolder.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10f);
            Cam.transform.parent = null;
            Cam.transform.position = camHolder.transform.position;
            Cam.transform.parent = camHolder.transform;
            anim.SetTrigger("play");
        }

    }
}

