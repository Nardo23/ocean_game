using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu]

public class TileData : ScriptableObject
{

    public TileBase[] tiles;

    public bool isLand;
    [Header("1 = sand, 2 = grass, 3 = stone, 4 = wood, 5 = cave water, 6 = ice, 7 = snow" )]
    public int terrainSoundType;

}
