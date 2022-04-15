using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeDraw
{

    public class credits : MonoBehaviour
    {
        public Animator anim;
        public Animator sandAnim;
        [SerializeField]
        GameObject[] creditsObj;
        public GameObject playtesters;
        public Drawable drawScript;
        public GameObject startStuff;
        bool started = false;
        public float slideTime = 2f;
        public float timer = 0;

        int objCount = 0;

        // Start is called before the first frame update
        void Start()
        {
            advanceCredits();
            // now that we're in the credits scene we can draw with the controller!
            InputAbstraction.inputInstance.AllowControllerMoveCursor(true);
        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.Play("texRinse");

            }
            */
            if (!started)
            {
                timer += Time.deltaTime;
                if(timer>= slideTime)
                {
                    anim.Play("texRinse");
                    timer = 0;
                }
                
            }



        }

        public void advanceCredits()
        {
            if (objCount < creditsObj.Length)
            {
                foreach (GameObject obj in creditsObj)
                {
                    obj.SetActive(false);


                }
                creditsObj[objCount].SetActive(true);

                if (objCount <= 2)
                {
                    playtesters.SetActive(true);
                }
                else
                {
                    playtesters.SetActive(false);
                }
                objCount++;
                drawScript.ResetCanvas();
            }
            else if (!started)
            {
                creditsObj[objCount-1].SetActive(false);
                started = true;
                drawScript.ResetCanvas();
                startStuff.SetActive(true);
            }

        }




    }
}
