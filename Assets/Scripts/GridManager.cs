using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class GridManager : MonoBehaviour, IGridManager, IGridRenderer
{
    [Header("Tilemap Variabbles")]
    [SerializeField] protected Tile[] groundTiles;
    [SerializeField] protected Tile[] highlightTiles;
    [SerializeField] protected Tilemap[] tilemaps;
    protected Dictionary<Vector3Int, Cell> threeAxisCells = new Dictionary<Vector3Int, Cell>();


    protected List<uint> tilemapIds = new List<uint>();
    public IReadOnlyList<uint> TilemapIds => tilemapIds; 
    protected List<uint> tileIds = new List<uint>();
    public IReadOnlyList<uint> TileIds { get => tileIds; }

    public void GenerateGrid(uint TileMapId, int gridHeight, MapShape mapShape)
    {
        Clear();

        if (mapShape == MapShape.Hexagon)
            GenerateHexagon(gridHeight);
    }

    public void HighlightTileMap(uint TileMapId, IEnumerable<ICell> cells, uint tileId = 0)
    {
        foreach (ICell cell in cells)
            tilemaps[TileMapId].SetTile(cell.GridPosition, highlightTiles[tileId]);
    }



    public bool TryGetCell(Vector3Int threeAxis, out ICell foundCell)
    {
        Cell internalFoundCell;
        bool returnVar = threeAxisCells.TryGetValue(threeAxis, out internalFoundCell);
        foundCell = internalFoundCell;
        return returnVar;
    }
    #region TryGetCells
    static protected Dictionary<uint, IEnumerable<Vector3Int>> Hexagon3AxisLookup = new Dictionary<uint, IEnumerable<Vector3Int>>()
    {
        {0, new Vector3Int[] {Vector3Int.zero } }
        //TODO Fill this in
    };

    protected IEnumerable<Vector3Int> Calculate3AxisHexagon(uint radius)
    {
        IEnumerable<Vector3Int> result;
        Hexagon3AxisLookup.TryGetValue(radius, out result);
        //TODO: code this to use hexagon3Axis or to calcuate it manually for larger hexagons
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
    #endregion
    public void PaintTile(uint TileMapId, IEnumerable<ICell> cells, uint tileId = uint.MaxValue)
    {
        Tile selected = (tileId > groundTiles.Length) ? default : groundTiles[tileId];
        foreach (ICell cell in cells)
            PaintTile(TileMapId, cell, selected);
    }
    protected void PaintTile(uint tileMapId, ICell cell, uint tileId = uint.MaxValue){PaintTile (tileMapId, cell, (tileId > groundTiles.Length) ? default : groundTiles[tileId]);}
    protected void PaintTile(uint TileMapId, ICell cell, Tile selected)
    {
        tilemaps[TileMapId].SetTile(cell.GridPosition, selected);
    }

    public void ChangeTileInfo(ICell cell, TileInfo info) => (cell as Cell).TileInfo = info;

    public void ChangeTileOccupant(ICell cell, IOccupant occupant) => (cell as Cell).Occupant = occupant;

    protected void Clear()
    {
        ChangeTile(default, threeAxisCells.Values);
        threeAxisCells.Clear();
    }

    protected void GenerateHexagon(int radius)
    {
        //Add randomization for ground types somewhere using the line below.
        uint tile = TileIds[0];
        TileInfo info = TileInfo.Flat;

        GenerateRow(0, -radius, radius, info, tile);
        for (int i = 1; i <= radius; i++)
        {
            int half = i / 2;
            int oddCorrection = i % 2;
            GenerateRow(i, -radius + half, radius - half - oddCorrection, info, tile);
            GenerateRow(-i, -radius + half, radius - half - oddCorrection, info, tile);
        }
    }

    protected void GenerateRow(int Y, int xMin, int xMax, TileInfo info, uint tile)
    {
        int currentX = xMin;
        while (currentX <= xMax)
            GenerateCell(new Vector3Int(currentX++, Y, 0), info, tile);
    }

    protected void GenerateCell(Vector3Int gridPos, TileInfo info, uint tile)
    {
        int newX = gridPos.x - gridPos.y / 2;
        if (gridPos.y % 2 == -1)
            newX++;
        int newZ = -(newX + gridPos.y);
        Vector3Int threeAxisPosition = new Vector3Int(newX, gridPos.y, newZ);
        Cell newCell = new Cell(gridPos, threeAxisPosition, default, info);

        threeAxisCells.Add(threeAxisPosition, newCell);

        //ChangeTile(default, threeAxisCells[threeAxisPosition].GridPosition);
    }



}
