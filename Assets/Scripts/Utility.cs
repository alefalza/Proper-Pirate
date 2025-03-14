using UnityEngine;

public static class Utility
{
    public static float GetPerlinNoise(float x, float y, int width, int depth, float frequency)
    {
        float noise = 0f;
        
        float waveLengthX = width / frequency;
        float waveLengthY = depth / frequency;
        
        float xCoord = x / waveLengthX;
        float yCoord = y / waveLengthY;

        noise = Mathf.PerlinNoise(xCoord, yCoord);

        return noise;
    }
}