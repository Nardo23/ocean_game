using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{

    public class demoCollision : MonoBehaviour
    {
        public demoManager demoScript;
        bool visited =false;
    

        void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                if (!visited)
                {
                    demoScript.IncreaseVisitCount();
                    visited = true;
                }
                
                
            }

        }

    }

}
