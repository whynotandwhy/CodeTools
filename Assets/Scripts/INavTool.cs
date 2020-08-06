using System;
using System.Collections.Generic;
using UnityEngine;

public interface INavTool
{
    Vector3 GetWorldLocation(ICell location);
    IEnumerable<ICell> GetNeighborCells(ICell origin, int range = 1);
    IEnumerable<ICell> GetTraversableArea(ICell origin, int range, IUnit unit, Func<IUnit, TileInfo, bool> canTraverse);
    IEnumerable<ICell> GetPath(ICell origin, ICell destination, int range = int.MaxValue, bool traversable = true);
}