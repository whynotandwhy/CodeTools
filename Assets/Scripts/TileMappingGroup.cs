using IEnumerableExtention;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileMappingGroup
{
    public Tilemap tilemap;
    public Tile[] relatedTiles;

    public Tile TranslateTileInfo(TileInfo info)
    {
        if (relatedTiles.Length == default)
            throw new System.InvalidOperationException("No tiles to translate in tilemap group.");
        if (info == default)
            return relatedTiles[0];

        return CalculateTile(info);
    }

    protected virtual Tile CalculateTile(TileInfo info)
    {
        return relatedTiles.ProtectedIndex((int)Mathf.Log((float)info, 2));
    }
}

