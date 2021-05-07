using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreasureData
{
    public float[] chests;


    public TreasureData(TreasureManager treasureManager)
    {
        chests = treasureManager.chestArray;


    }
}
