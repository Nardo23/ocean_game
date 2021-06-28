using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class animalLand : MonoBehaviour
{
    bool land = false;
    public float offset;

    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private Tilemap decorationMap;
    [SerializeField]
    private Tilemap objectMap;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }



    public bool LandCheck()
    {


        // check tile data for ground
        Vector3 transOff = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);

        Vector3Int gridPosition = map.WorldToCell(transOff);
        TileBase CurrentTile = map.GetTile(gridPosition);




        //print("At position " + gridPosition + " tile type: "+ CurrentTile);
        
       // if (inUnderworld && triggerOcean)
       // {
        //    return false;
       // }

        if (CurrentTile == null)
        {
            return false;
        }
        else
        {
            //Debug.Log(dataFromTiles[CurrentTile].isLand);  
            return true;
        }
      
    }


  
}
