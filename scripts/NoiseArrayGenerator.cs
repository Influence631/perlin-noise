using Godot;

public class NoiseArrayGenerator{
    float[,] noiseArray;
    PerlinNoise perlinNoise;
    public NoiseArrayGenerator(int seed){
        perlinNoise = new PerlinNoise(seed);
    }
    public float[,] GenerateNoiseArray(int width, int height, float scale, int octaves, Vector2 offset, float persistance, float lacunarity){
		noiseArray = new float[width, height];
	
		for (int x = 0; x < width; x++){
			for (int y = 0; y < height; y++){
				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int octave = 0; octave < octaves; octave++){
					float sampleX = offset.X + (float)(x - width * 0.5) / scale * frequency;
					float sampleY = offset.Y + (float)(y - height * 0.5) / scale * frequency;

					float perlinValue = perlinNoise.Perlin(sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}
				
				noiseArray[x, y] = (noiseHeight + 1) / 2.0f;
        	}
    	}
    return noiseArray;
    }
}
