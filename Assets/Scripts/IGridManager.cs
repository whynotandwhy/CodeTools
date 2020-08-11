using UnityEngine;
using System.Collections.Generic;


public interface IGridManager
{
    bool TryGetCell(Vector3Int threeAxis, out ICell foundCell);
    IEnumerable<ICell> TryGetCells(Vector3Int origin, IEnumerable <Vector3Int> threeAxisList);
    /// <summary>
    /// Gets origin + neighbors up to radius away.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="threeAxisList"></param>
    /// <returns></returns>
    IEnumerable<ICell> TryGetCells(Vector3Int origin, uint radius = 1);
    void GenerateGrid(uint gridHeight, MapShape mapShape);
    void ChangeTileInfo(ICell cell, TileInfo info);
    void ChangeTileOccupant(ICell cell, IOccupant occupant);
}