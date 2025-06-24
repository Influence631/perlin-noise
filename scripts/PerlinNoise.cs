// See https://aka.ms/new-console-template for more information
using System;
using Godot;

public class PerlinNoise
{
    private int[] permutation;
    private int[] p;

    public PerlinNoise(int seed)
    {
        var random = new Random(seed);
        permutation = new int[256];
        p = new int[512];

        for (int i = 0; i < 256; i++)
        {
            permutation[i] = i;
        }

        for (int i = 255; i > 0; i--)
        {
            int swapIndex = random.Next(i + 1);
            int temp = permutation[i];
            permutation[i] = permutation[swapIndex];
            permutation[swapIndex] = temp;
        }

        for (int i = 0; i < 512; i++){
            p[i] = permutation[i % 256];
        }
    }

    // Fade function to smooth out the noise
    private static float Fade(float t){
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    private static float Lerp(float t, float a, float b){
        return a + t * (b - a);
    }


    private static float Grad(int hash, float x, float y)
    {
        int h = hash & 7;
        float u = h < 4 ? x : y;
        float v = h < 4 ? y : x;

        return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
    }


    public float Perlin(float x, float y){
        int X = (int)Math.Floor(x) & 255;
        int Y = (int)Math.Floor(y) & 255;

        x -= (float)Math.Floor(x);
        y -= (float)Math.Floor(y);

        float u = Fade(x);
        float v = Fade(y);

        // Hash coordinates of the 4 square corners
        int aa = p[p[X] + Y];
        int ab = p[p[X] + Y + 1];
        int ba = p[p[X + 1] + Y];
        int bb = p[p[X + 1] + Y + 1];

        float result = Lerp(v,
            Lerp(u, Grad(aa, x, y), Grad(ba, x - 1, y)),
            Lerp(u, Grad(ab, x, y - 1), Grad(bb, x - 1, y - 1))
        );

        return (result + 1.0f) / 2.0f;
    }
}

