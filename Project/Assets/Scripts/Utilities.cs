public enum Tags
{
    Player1,
    Player2,
    Terrain,
    OutOfBounds
}

public static class ExtensionMethods
{
    public static float Map01To(this float value, float destLow, float destHigh)
    {
        return value * (destHigh - destLow) + destLow;
    }
}