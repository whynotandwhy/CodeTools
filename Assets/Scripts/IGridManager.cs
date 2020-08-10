using UnityEngine;
using System.Collections.Generic;


public interface IGridManager
{
    bool TryGetCell(Vector3Int threeAxis, out ICell foundCell);
    IEnumerable<ICell> TryGetCells(Vector3Int origin, IEnumerable <Vector3Int> threeAxisList);
    IEnumerable<ICell> TryGetCells(Vector3Int origin, uint radius = 1);
    void GenerateGrid(uint TileMapId, uint gridHeight, MapShape mapShape);
    void ChangeTileInfo(ICell cell, TileInfo info);
    void ChangeTileOccupant(ICell cell, IOccupant occupant);
}