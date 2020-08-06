using UnityEngine;

public class Cell : ICell
{
    public Vector3Int GridPosition { get; set; }

    public Vector3Int ThreeAxisPosition { get; set; }

    public IOccupant Occupant { get; set; }

    public TileInfo TileInfo { get; set; }

    public Cell(Vector3Int gridPosition, Vector3Int threeAxisPosition, IOccupant occupant, TileInfo tileInfo)
    {
        GridPosition = gridPosition;
        ThreeAxisPosition = threeAxisPosition;
        Occupant = occupant;
        TileInfo = tileInfo;
    }
}
