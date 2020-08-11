using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestBattlefieldManager : GridManager
{
    [Header("Grid Variables")]
    [SerializeField] protected bool clearFirst;
    [SerializeField] protected uint gridHeight;
    [Header("Render Variables")]
    [SerializeField] protected uint mapId;
    [SerializeField] protected TileInfo info;


    protected IGridManager gridManager => this;
    protected IGridRenderer gridRenderer => this;

    [ContextMenu("Gen Grid")]
    public void GenerateGridManager()
    {
        gridManager.GenerateGrid(gridHeight, default);
        CheckForCell(new Vector3Int(0, 0, 0));
    }

    [ContextMenu("Get Neighbors: 1 rad")]
    protected void Rad1NeighborCheck()
    {
        gridManager.GenerateGrid(1, default);

        CheckForCell(new Vector3Int(1, 0, -1));

        IEnumerable<ICell> cells = gridManager.TryGetCells(new Vector3Int(0, 0, 0));
        if(cells.Count() != 7)
            Debug.Log("Expected cell count does not match. Testing against 7.");

        cells = gridManager.TryGetCells(new Vector3Int(1, 0, -1));
        if (cells.Count() != 4)
            Debug.Log("Expected cell count does not match. Testing against 4.");
    }

    [ContextMenu("Change TileInfo")]
    protected void ChangeTileInfo()
    {
        Rad1NeighborCheck();        
        
        gridManager.ChangeTileInfo(threeAxisCells[new Vector3Int(0, 0, 0)], TileInfo.DeepWater);
        
        ICell cell;
        gridManager.TryGetCell(new Vector3Int(0, 0, 0), out cell);

        if (cell.TileInfo != TileInfo.DeepWater)
            Debug.Log("Data was not updated.");
    }

    [ContextMenu("Change TileOccupant")]
    protected void ChangeTileOccupant()
    {
        throw new System.NotImplementedException();
    }

    protected void CheckForCell(Vector3Int location)
    {
        ICell cell;
        gridManager.TryGetCell(location, out cell);

        if (cell == default)
            Debug.Log("No cell.");
    }

    [ContextMenu("Paint Tile")]
    protected void PaintTileInSpace()
    {
        gridRenderer.PaintTile(mapId, new Vector3Int(0, 0, 0), info);
    }
}

