using UnityEngine;
using System.Collections.Generic;


public interface IGridManager
{
    bool TryGetCell(Vector3Int threeAxis, out ICell foundCell);
    IEnumerable<ICell> TryGetCells(Vector3Int origin, IEnumerable <Vector3Int> threeAxisList);
    IEnumerable<ICell> TryGetCells(Vector3Int origin, int radius = 1);
    void GenerateGrid(uint TileMapId, int gridHeight, MapShape mapShape);
    void ChangeTileInfo(ICell cell, TileInfo info);
    void ChangeTileOccupant(ICell cell, IOccupant occupant);
}

public interface IGridRenderer
{
    IReadOnlyList<uint> TilemapIds { get; }
    IReadOnlyList<uint> TileIds { get; }
    void HighlightTileMap(uint TileMapId, IEnumerable<ICell> cells, uint TileBaseIds = 0);
    void ChangeTile(uint TileMapId, IEnumerable<ICell> cells, uint TileBaseIds = 0);
}