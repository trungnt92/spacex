using UnityEngine;

public static class NumberHelper
{
	public static float RandomWithin(float low1, float up1, float low2, float up2)
	{
		float num1 = Random.Range(low1, up1);
		float num2 = Random.Range(low2, up2);

		return Random.Range(0, 2) == 0 ? num1 : num2;
    }
}