using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FreeDraw
{
    public class ColorSwap : MonoBehaviour
    {
        public Color color;
        public DrawingSettings drawSetScript;
        public bool defaultColor=false;
        public void setColor()
        {
            drawSetScript.colorSwap(color);
        }

        private void Start()
        {
            if (defaultColor)
            {
                drawSetScript.colorSwap(color);
            }
        }
    }



}