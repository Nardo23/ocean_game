using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FreeDraw
{
    // Helper methods used to set drawing settings
    public class DrawingSettings : MonoBehaviour
    {
        public static bool isCursorOverUI = false;
        public float Transparency = 1f;
        public Color mapBgColor;
        Color prevCol = Color.white;
        
        public Texture2D pencilIcon;
        [SerializeField]
        public GameObject[] stamps;
        public GameObject skull;


        private void Start()
        {
            prevCol = pencilIcon.GetPixel(4, 4);
            colorSwap(prevCol);
        }

        // Changing pen settings is easy as changing the static properties Drawable.Pen_Colour and Drawable.Pen_Width
        public void SetMarkerColour(Color new_color)
        {
            Drawable.Pen_Colour = new_color;
        }
        // new_width is radius in pixels
        public void SetMarkerWidth(int new_width)
        {
            Drawable.Pen_Width = new_width;
        }
        public void SetMarkerWidth(float new_width)
        {
            SetMarkerWidth((int)new_width);
        }

        public void SetTransparency(float amount)
        {
            Transparency = amount;
            Color c = Drawable.Pen_Colour;
            c.a = amount;
            Drawable.Pen_Colour = c;
        }

        public void swapIconColor(Color col)
        {
            if( col != prevCol)
            {
                 Color colSample = pencilIcon.GetPixel(4, 4);
                Debug.Log("hiii");
                int y = 0;
                while (y < pencilIcon.height)
                {
                    int x = 0;
                    while (x < pencilIcon.width)
                    {
                        if (pencilIcon.GetPixel(x, y) == colSample)
                        {
                            pencilIcon.SetPixel(x, y, col);
                        }
                        else
                        {
                           
                        }
                        ++x;
                    }
                    ++y;
                }
                prevCol = col;
                pencilIcon.Apply();
            }
            
        }

        public void stampIconColor(Color col)
        {
            for (int i =0; i< stamps.Length; i++)
            {
                stamps[i].GetComponent<Button>().GetComponent<Image>().color = col;
            }
        }

        // Call these these to change the pen settings


        public void SetMarkerPrev()
        {
            Color c = prevCol;
            c.a = Transparency;
            SetMarkerColour(c);
            Drawable.drawable.SetPenBrush();
        }

        public void SetStamp1 ()
        {
            Color c = prevCol;
            c.a = Transparency;
            Drawable.drawable.SetStamp1Brush();
        }



        public void colorSwap(Color newColor)
        {
            Color c = newColor;
            c.a = Transparency;
            SetMarkerColour(c);

            stampIconColor(c);
            swapIconColor(c);
            Drawable.drawable.stampColor = c;
            

        }

        public void SetMarkerRed()
        {
            Color c = Color.red;
            c.a = Transparency;
            SetMarkerColour(c);
            swapIconColor(c);
            stampIconColor(c);
            Drawable.drawable.SetPenBrush();
            Drawable.drawable.stampColor = c;
        }
        public void SetMarkerGreen()
        {
            Color c = Color.green;
            c.a = Transparency;
            SetMarkerColour(c);
            swapIconColor(c);
            stampIconColor(c);
            Drawable.drawable.SetPenBrush();
            Drawable.drawable.stampColor = c;
        }
        public void SetMarkerBlue()
        {
            Color c = Color.blue;
            c.a = Transparency;
            SetMarkerColour(c);
            stampIconColor(c);
            swapIconColor(c);
            Debug.Log("hmmm");
            Drawable.drawable.SetPenBrush();
            Drawable.drawable.stampColor = c;
        }
        public void SetEraser()
        {
            SetMarkerColour(mapBgColor);
        }

        public void PartialSetEraser()
        {
            SetMarkerColour(new Color(255f, 255f, 255f, 0.5f));
        }

        /////// set Stamps////////////
        ///

        public void SkullStamp()
        {


            Drawable.drawable.stampObj = skull;
            Drawable.drawable.SetStamp1Brush();
        }




    }
}