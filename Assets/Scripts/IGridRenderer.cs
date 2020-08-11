using System.Collections.Generic;
using UnityEngine;

public interface IGridRenderer
{
    void PaintTile(uint TileMapId, IEnumerable<ICell> cells, TileInfo info, bool clearFirst = false);
    void PaintTile(uint TileMapId, ICell cell, TileInfo info);
    void PaintTile(uint TileMapId, Vector3Int gridVector, TileInfo info);
}
