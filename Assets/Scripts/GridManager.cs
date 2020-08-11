using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using IEnumerableExtention;


[System.Serializable]
public class GridManager : MonoBehaviour, IGridManager, IGridRenderer
{
    [Header("Tilemap Variables")]
    [SerializeField] protected TileMappingGroup[] tilemaps;

    protected Dictionary<Vector3Int, Cell> threeAxisCells = new Dictionary<Vector3Int, Cell>();


    #region GridGeneration 

    protected Dictionary<MapShape, Func<uint, IEnumerable<Vector3Int> > > GenerationLogic
         = new Dictionary<MapShape, Func<uint, IEnumerable<Vector3Int>>>()
         {
             { MapShape.Hexagon, CalculateHexagon }
         };

    public void GenerateGrid(uint gridHeight, MapShape mapShape)
    {
        threeAxisCells.Clear();

        Func<uint, IEnumerable<Vector3Int>> defineGridCells;
        if (!GenerationLogic.TryGetValue(mapShape, out defineGridCells))
            defineGridCells = CalculateHexagon;

        IEnumerable<Vector3Int> gridToMake = defineGridCells(gridHeight);

        foreach (Vector3Int gridVector in gridToMake)
            GenerateCell(gridVector, default);
    }

    #endregion
   
    #region TryGetCells
    static protected Dictionary<uint, IEnumerable<Vector3Int>> Hexagon3AxisLookup = new Dictionary<uint, IEnumerable<Vector3Int>>();

    protected IEnumerable<Vector3Int> Calculate3AxisHexagon(uint radius)
    {
        IEnumerable<Vector3Int> result;
        if(!Hexagon3AxisLookup.TryGetValue(radius, out result))
        {
            result = CalculateHexagon(radius).Select(X => CalcuateThreeAxisPosition(X));
            Hexagon3AxisLookup.Add(radius, result);
        }
        return result;
    }

    public IEnumerable<ICell> TryGetCells(Vector3Int origin, uint radius = 1) { return TryGetCells(origin, Calculate3AxisHexagon(radius)); }
    public IEnumerable<ICell> TryGetCells(Vector3Int origin, IEnumerable<Vector3Int> threeAxisList)
    {
        return threeAxisList.Select(X =>
        {
            ICell DesiredCell;
            TryGetCell(origin + X, out DesiredCell);
            return DesiredCell;
        }).Where(X => X != default);
    }

    public bool TryGetCell(Vector3Int threeAxis, out ICell foundCell)
    {
        Cell internalFoundCell;
        bool returnVar = threeAxisCells.TryGetValue(threeAxis, out internalFoundCell);
        foundCell = internalFoundCell;
        return returnVar;
    }
    #endregion

    protected Tile SelectTile(uint tileMapId, TileInfo tile)
    {
        return tilemaps[tileMapId].TranslateTileInfo(tile);
    }

    #region PaintTile
    public void PaintTile(uint tileMapId, IEnumerable<ICell> cells, TileInfo tile, bool clearFirst = false)
    {
        if (clearFirst)
            ClearMapTiles(tileMapId);

        Tile selected = SelectTile(tileMapId, tile);
        foreach (ICell cell in cells)
            PaintTile(tileMapId, cell, selected);
    }
    public void PaintTile(uint tileMapId, ICell cell, TileInfo tileInfo) { PaintTile(tileMapId, cell, SelectTile(tileMapId, tileInfo)); }
    public void PaintTile(uint tileMapId, Vector3Int location, TileInfo tileInfo) { PaintTile(tileMapId, location, SelectTile(tileMapId, tileInfo)); }
    protected void PaintTile(uint tileMapId, ICell cell, uint tileId = uint.MaxValue){ PaintTile(tileMapId, cell, tilemaps[tileMapId].relatedTiles.ProtectedIndex(tileId)); }
    protected void PaintTile(uint tileMapId, ICell cell, Tile selected) { PaintTile(tileMapId, cell.GridPosition, selected); }
    protected void PaintTile(uint tileMapId, Vector3Int location, Tile selected)
    {
        tilemaps[tileMapId].tilemap.SetTile(location, selected);
    }
    #endregion

    public void ChangeTileInfo(ICell cell, TileInfo info) => (cell as Cell).TileInfo = info;

    public void ChangeTileOccupant(ICell cell, IOccupant occupant) => (cell as Cell).Occupant = occupant;

    public void ClearMapTiles(uint index)
    {
        tilemaps[index].tilemap.ClearAllTiles();
    }

    protected static IEnumerable<Vector3Int> CalculateHexagon(uint radius)
    {
        List<Vector3Int> finalGrid = new List<Vector3Int>();

        CalcuateRow(0, -(int)radius, (int)radius, finalGrid);
        for (int i = 1; i <= radius; i++)
        {
            int half = i / 2;
            int oddCorrection = i % 2;
            CalcuateRow(i, -(int)radius + half, (int)radius - half - oddCorrection, finalGrid);
            CalcuateRow(-i, -(int)radius + half, (int)radius - half - oddCorrection, finalGrid);
        }

        return finalGrid;
    }

    protected static void CalcuateRow(int Y, int xMin, int xMax, List<Vector3Int> list)
    {
        int currentX = xMin;
        while (currentX <= xMax)
            list.Add(new Vector3Int(currentX++, Y, 0));
    }

    protected Vector3Int CalcuateThreeAxisPosition(Vector3Int gridPos)
    {
        int newX = gridPos.x - gridPos.y / 2;
        if (gridPos.y % 2 == -1)
            newX++;
        int newZ = -(newX + gridPos.y);
        return  new Vector3Int(newX, gridPos.y, newZ);
    }

    protected void GenerateCell(Vector3Int gridPos, TileInfo info)
    {
        Vector3Int threeAxisPosition = CalcuateThreeAxisPosition(gridPos);
        Cell newCell = new Cell(gridPos, threeAxisPosition, default, info);

        threeAxisCells.Add(threeAxisPosition, newCell);
    }
}
