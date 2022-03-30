using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{


    GameObject[] chests;
    treasure treasureScript;
    public float[] chestArray;

    public void TreasureManagerStart() // call this from player
    {
        
        GetChests();

    }

    

    public void GetChests()
    { 
        chests = GameObject.FindGameObjectsWithTag("chest");
        //Debug.Log(chests.Length);       
    }
  

    public bool checkChest(float x, float y)
    {

        string path = Application.persistentDataPath + "/treasure.piss";
        if (System.IO.File.Exists(path))
        {
            TreasureData data = SaveSystem.LoadTreasure();
            float[] chestsToCheck = data.chests;

            int i = 0;

            while (i < chestsToCheck.Length)
            {
                if (x == chestsToCheck[i] && y == chestsToCheck[i + 1])
                {
                    return true;
                }
                i += 2;

            }

            return false;
        }

        return false;
    }


    public void SaveChests()
    {
        int l = 0;

        foreach (GameObject CurrentChest in chests)
        {
            if (CurrentChest.GetComponent<treasure>() != null)
            {
                treasureScript = CurrentChest.GetComponent<treasure>();
                if (treasureScript.givenTreasure)
                {
                    l += 2;
                }


            }


        }
        chestArray = new float[l];


        int i = 0;
        foreach (GameObject CurrentChest in chests)
        {
            if (CurrentChest.GetComponent<treasure>() != null)
            {
                //Debug.Log("script found");
                treasureScript = CurrentChest.GetComponent<treasure>();
                if (treasureScript.givenTreasure)
                {
                    //Debug.Log("treasureGiven");
                    chestArray[i] = CurrentChest.transform.position.x;
                    chestArray[i + 1] = CurrentChest.transform.position.y;
                    //Debug.Log(chestArray[i]);
                    i += 2;
                }


            }


        }
        //Debug.Log(chestArray);
        SaveSystem.SaveTreasure(this);
    }

    





}
