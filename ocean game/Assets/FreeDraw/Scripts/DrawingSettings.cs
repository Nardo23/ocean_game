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
        public AudioSource sor;
        [SerializeField]
        AudioClip[] colorclips;

        public Sprite cSmall, cMed, cLarge, cStamp;
        public SpriteRenderer drawCursorRend;
        public float CursorAlpha = 185;

        public static bool isCursorOverUI = false;
        public float Transparency = 1f;
        public Color mapBgColor;
        Color prevCol = Color.white;
        public float prevWidth=.25f;

        public Texture2D pencilIcon;
        [SerializeField]
        public GameObject[] stamps;
        public GameObject star, heart, flag, xMark, wind, diamond, mushroom, circle, door, chest, frown, smile, skull;
        public GameObject player;
        
        GameObject currentStamp;
        public float[] stampArray;

        public Color EraserCursorColor;
        
        private bool loaded = false;
        public bool credits = false;
        public Color creditsEraserColor;

        private void Start()
        {
            EraserCursorColor.a = CursorAlpha;
            prevCol = pencilIcon.GetPixel(4, 4);
            colorSwap(prevCol);
            if(!credits)
                LoadStamps(); // Load Stamps!!
            loaded = true;
            
        }

        private void OnApplicationQuit()
        {
            if (!credits)
                saveStamps(); // Save Stamps on quit!!
        }
        private void OnApplicationPause(bool pause)
        {
            if (pause == true)
            {
                if (!credits)
                    saveStamps(); // Also Save Stamps on Pause for Mobile!!

            }
        }       

        public void saveStamps()
        {
            int i = 0;
            foreach(Transform child in transform)
            {
                if (child.gameObject.tag == "stamp")
                {
                    i++;                                    
                }
                
            }
            stampArray = new float[(i*6)];
            i = 0;
            foreach (Transform child in transform)
            {
                if (child.gameObject.tag == "stamp")
                {
                    
                    Sprite sprite1 = child.gameObject.GetComponent<SpriteRenderer>().sprite;
                    stampArray[i] = GetStampId(sprite1);
                    stampArray[i + 1] = child.position.x - player.transform.position.x;
                    stampArray[i + 2] = child.position.y - player.transform.position.y;
                    stampArray[i + 3] = child.gameObject.GetComponent<SpriteRenderer>().color.r;
                    stampArray[i + 4] = child.gameObject.GetComponent<SpriteRenderer>().color.g;
                    stampArray[i + 5] = child.gameObject.GetComponent<SpriteRenderer>().color.b;

                    i += 6;
                }

            }

            SaveSystem.SaveStamp(this);

        }

        public void LoadStamps()
        {
            if (System.IO.File.Exists(Application.persistentDataPath + "/stamps.piss"))
            {
                StampData data = SaveSystem.LoadStamp();
                int i = 0;
                while (i < data.stamps.Length)
                {
                    GameObject newStamp = StampCheck(data.stamps[i]);
                    var newStampLoad = Instantiate(newStamp, transform);
                    newStampLoad.transform.position = new Vector3(data.stamps[i + 1] + player.transform.position.x, data.stamps[i + 2] + player.transform.position.y, transform.position.z);
                    Color stampColor1 = new Vector4(data.stamps[i + 3], data.stamps[i + 4], data.stamps[i + 5], 1f);
                    //Debug.Log(newStampLoad.transform.position);
                    newStampLoad.GetComponent<SpriteRenderer>().color = stampColor1;
                    i += 6;
                }
            }
            


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
            prevWidth = new_width;
        }
        public void SetMarkerWidth(float new_width)
        {
            SetMarkerWidth((int)new_width);
            prevWidth = new_width;
        }
        public void SetCursorSmall()
        {
            drawCursorRend.sprite = cSmall;
        }
        public void SetCursorMed()
        {
            drawCursorRend.sprite = cMed;
        }
        public void SetCursorLarge()
        {
            drawCursorRend.sprite = cLarge;
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
                //Debug.Log("hiii");
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
            c.a = CursorAlpha;
            drawCursorRend.color = c;
            CursorSpritePreviousWidth();

        }

        public void CursorSpritePreviousWidth()
        {
            if (prevWidth == .25f)
            {
                SetCursorSmall();
            }
            else if(prevWidth == 1f)
            {
                SetCursorMed();
            }
            else if(prevWidth == 2)
            {
                SetCursorLarge();
            }
        }

        public void SetStamp1 ()
        {
            Color c = prevCol;
            c.a = Transparency;
            Drawable.drawable.SetStamp1Brush();
        }



        public void colorSwap(Color newColor)
        {
            sor.pitch = Random.Range(.8f, 1.1f);
            sor.PlayOneShot(colorclips[Random.Range(0, colorclips.Length)]);

            Color c = newColor;
            c.a = Transparency;
            SetMarkerColour(c);

            stampIconColor(c);
            swapIconColor(c);
            Drawable.drawable.stampColor = c;

            newColor.a = CursorAlpha;
            drawCursorRend.color = newColor;
           
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
            //Debug.Log("hmmm");
            Drawable.drawable.SetPenBrush();
            Drawable.drawable.stampColor = c;
        }
        public void SetEraser()
        {
            SetMarkerColour(mapBgColor);
            Drawable.drawable.SetEraser();
            CursorSpritePreviousWidth();
            drawCursorRend.color = EraserCursorColor;
        }

        public void CreditsEraser()
        {
            SetMarkerColour(creditsEraserColor);
            Drawable.drawable.SetPenBrush();
            CursorSpritePreviousWidth();
            drawCursorRend.color = EraserCursorColor;

        }

        public void PartialSetEraser()
        {
            SetMarkerColour(new Color(255f, 255f, 255f, 0.5f));
        }

        /////// set Stamps////////////
        ///
        public void stampCursor()
        {
            drawCursorRend.sprite = cStamp;
            Color c = prevCol;
            c.a = Transparency;
            c.a = CursorAlpha;
            drawCursorRend.color = c;
        }


        public void StarStamp()
        {
            Drawable.drawable.stampObj = star;
            Drawable.drawable.SetStamp1Brush();
        }
        public void HeartStamp()
        {
            Drawable.drawable.stampObj = heart;
            Drawable.drawable.SetStamp1Brush();
        }
        public void FlagStamp()
        {
            Drawable.drawable.stampObj = flag;
            Drawable.drawable.SetStamp1Brush();
        }
        public void XStamp()
        {
            Drawable.drawable.stampObj = xMark;
            Drawable.drawable.SetStamp1Brush();
        }
        public void WindStamp()
        {
            Drawable.drawable.stampObj = wind;
            Drawable.drawable.SetStamp1Brush();
        }
        public void DiamondStamp()
        {
            Drawable.drawable.stampObj = diamond;
            Drawable.drawable.SetStamp1Brush();
        }
        public void mushroomStamp()
        {
            Drawable.drawable.stampObj = mushroom;
            Drawable.drawable.SetStamp1Brush();
        }
        public void CircleStamp()
        {
            Drawable.drawable.stampObj = circle;
            Drawable.drawable.SetStamp1Brush();
        }
        public void DoorStamp()
        {
            Drawable.drawable.stampObj = door;
            Drawable.drawable.SetStamp1Brush();
        }
        public void ChestStamp()
        {
            Drawable.drawable.stampObj = chest;
            Drawable.drawable.SetStamp1Brush();
        }
        public void FrownStamp()
        {
            Drawable.drawable.stampObj = frown;
            Drawable.drawable.SetStamp1Brush();
        }
        public void SmileStamp()
        {
            Drawable.drawable.stampObj = smile;
            Drawable.drawable.SetStamp1Brush();
        }
        public void SkullStamp()
        {
            Drawable.drawable.stampObj = skull;
            Drawable.drawable.SetStamp1Brush();
        }
        
        public GameObject StampCheck(float Id)
        {
            if (Id == 1)
                return star;
            if (Id == 2)
                return heart;
            if (Id == 3)
                return flag;
            if (Id == 4)
                return xMark;
            if (Id == 5)
                return wind;
            if (Id == 6)
                return diamond;
            if (Id == 7)
                return mushroom;
            if (Id == 8)
                return circle;
            if (Id == 9)
                return door;
            if (Id == 10)
                return chest;
            if (Id == 11)
                return frown;
            if (Id == 12)
                return smile;
            if (Id == 13)
                return skull;

            //Debug.Log("Id: "+ Id+ "out of range");                           
            return star;
        }
         
        public float GetStampId(Sprite stamp)
        {
            
            if ( stamp.name == "stamps_0")
                return 1;
            if (stamp.name == "stamps_1")
                return 2;
            if (stamp.name == "stamps_2")
                return 3;
            if (stamp.name == "stamps_3")
                return 4;
            if (stamp.name == "stamps_4")
                return 5;
            if (stamp.name == "stamps_5")
                return 6;
            if (stamp.name == "stamps_6")
                return 7;
            if (stamp.name == "stamps_7")
                return 8;
            if (stamp.name == "stamps_8")
                return 9;
            if (stamp.name == "stamps_9")
                return 10;
            if (stamp.name == "stamps_10")
                return 11;
            if (stamp.name == "stamps_11")
                return 12;
            if (stamp.name == "stamps_12")
                return 13;

            //Debug.Log("sprite " + stamp.name + " not recognized");
            return 1;

        }

    }
}