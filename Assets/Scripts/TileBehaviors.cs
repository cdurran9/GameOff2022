using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tile Behaviors", menuName = "Tile Behaviors", order = 0)]
public class TileBehaviors : ScriptableObject
{
    public List<TileInfo> TileInfoList = new List<TileInfo>();

    public TileInfo GetTileInfo(Tile tile)
    {
        return TileInfoList.Find(x => x.Tile == tile);
    }

    public bool IsWalkable(Tile tile)
    {
        var foundInfo = GetTileInfo(tile);
        return foundInfo is { Walkable: true };
    }
}