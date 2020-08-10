using System.Collections.Generic;
using UnityEngine;

public interface IGridRenderer
{
    void PaintTile(uint TileMapId, IEnumerable<ICell> cells, uint tileId = uint.MaxValue);
    void PaintTile(uint TileMapId, ICell cell, uint tileId = uint.MaxValue);
    void PaintTile(uint TileMapId, Vector3Int gridVector, uint tileId = uint.MaxValue);
}
