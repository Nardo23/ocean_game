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
        public GameObject cutSor;
        bool playing = false;
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
            if (Input.GetKeyDown("x") && Input.GetKeyDown("c") && !playing)
            {
                endDemo();
            }



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
            playing = true;
            playerScript.canMove = false;
            canvas.SetActive(false);
            drawMap.SetActive(false);
            camHolder.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10f);
            Cam.transform.parent = null;
            Cam.transform.position = camHolder.transform.position;
            Cam.transform.parent = camHolder.transform;
            anim.SetTrigger("play");
            cutSor.SetActive(true);
            if (playerScript.inUnderworld)
            {
                playerScript.SwapWorld();
            }
        }
        public void resumGame()
        {
            canvas.SetActive(true);
            //drawMap.SetActive(true);
            Cam.transform.parent = null;
            camHolder.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
            camHolder.transform.parent = player.transform;
            Cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
            Cam.transform.parent = player.transform;
            playerScript.canMove = true;
            playing = false;
            //cutSor.SetActive(false);
        }


    }
}

