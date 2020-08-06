[System.Flags]
public enum TileInfo : long
{
    Flat = 1 << 0,
    Hilly = 1 << 1,
    Mountainous = 1 << 2,
    ShallowWater = 1 << 3,
    DeepWater = 1 << 4,
}
