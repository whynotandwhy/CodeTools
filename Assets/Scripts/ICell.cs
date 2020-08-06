using UnityEngine;

public interface ICell
{
    Vector3Int GridPosition { get; }
    Vector3Int ThreeAxisPosition { get; }
    IOccupant Occupant { get; }
    TileInfo TileInfo { get; }

    /* float DistanceTo(ICell cell); */
}
