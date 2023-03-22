using UnityEngine;

public static class GameHelper
{
    public static float GameBoundaryX = 1000;
    public static float GameBoundaryY = 1000;
    public static float GameBoundaryZ = 1000;
    
    public static float NoSpawnRegion = 50;

    private static readonly string kShipSelection = "kShipSelection";

    public static Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-GameBoundaryX, GameBoundaryX),
            Random.Range(-GameBoundaryY, GameBoundaryY),
            Random.Range(-GameBoundaryZ, GameBoundaryZ));
    }

    public static Vector3 RandonVel()
    {
        var vel = new Vector3(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1));
        return vel.normalized * 10f;
    }

    public static Vector3 RandomPos(float innerBound)
    {
        var x = NumberHelper.RandomWithin(-GameBoundaryX, -innerBound, innerBound, GameBoundaryX);
        var y = NumberHelper.RandomWithin(-GameBoundaryY, -innerBound, innerBound, GameBoundaryY);
        var z = NumberHelper.RandomWithin(-GameBoundaryZ, -innerBound, innerBound, GameBoundaryZ);
        return new Vector3(x, y, z);
    }

    public static Vector3 RandomPos(float innerBound, float outerBound)
    {
        var x = NumberHelper.RandomWithin(-outerBound, -innerBound, innerBound, outerBound);
        var y = NumberHelper.RandomWithin(-outerBound, -innerBound, innerBound, outerBound);
        var z = NumberHelper.RandomWithin(-outerBound, -innerBound, innerBound, outerBound);
        return new Vector3(x, y, z);
    }

    public static int LoadShipSelection()
    {
        return PlayerPrefs.GetInt(kShipSelection, 0);
    }

    public static void SaveShipSelection(int idx)
    {
        PlayerPrefs.SetInt(kShipSelection, idx);
    }
}

