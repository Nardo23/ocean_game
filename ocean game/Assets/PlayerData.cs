using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float[] position;


    public PlayerData(Player player)
    {
        position = new float[3];
        if (player.inUnderworld)
        {
            
            position[0] = player.doorStore.x;
            position[1] = player.doorStore.y;
            position[2] = player.transform.position.z;
        }
        else
        {
            position[0] = player.transform.position.x;
            position[1] = player.transform.position.y;
            position[2] = player.transform.position.z;
        }
    }


}
