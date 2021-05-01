using FreeDraw;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StampData 
{

    public float[] stamps;



    public StampData(DrawingSettings drawSet)
    {
        stamps = drawSet.stampArray;

    }


}
