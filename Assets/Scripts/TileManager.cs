using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] TileBehaviors tileBehaviors;
    private Dictionary<Tile, TileInfo> tileInfos = new Dictionary<Tile, TileInfo>();

    private void Awake()
    {
        foreach (TileInfo info in tileBehaviors.TileInfoList)
        {
            tileInfos.Add(info.Tile, info);
        }
    }
    
    public TileInfo GetTileInfo(Tile tile)
    {
        tileInfos.TryGetValue(tile, out var foundTileInfo);

        return foundTileInfo;
    }
}