using UnityEngine;

public class TestGridRenderer : GridManager
{
    [SerializeField] protected uint mapId;
    [SerializeField] protected TileInfo info;
    [SerializeField] protected bool clearFirst; 

    protected IGridManager gridManager => this;
    protected IGridRenderer gridRenderer => this;

    [ContextMenu("Paint Tile")]
    protected void PaintTileInSpace()
    {
        gridRenderer.PaintTile(mapId, new Vector3Int(0, 0, 0), info);
    }

    //void PaintTile(uint TileMapId, IEnumerable<ICell> cells, TileInfo info, bool clearFirst = false);
    //void PaintTile(uint TileMapId, ICell cell, TileInfo info);
    //void PaintTile(uint TileMapId, Vector3Int gridVector, TileInfo info);
}
