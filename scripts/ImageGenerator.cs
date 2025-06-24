using Godot;
using Godot.Collections;

public class ImageGenerator{
    public Image CreateImageFromNoise(float[,] noiseArray, int width, int height){
		Image image = Image.CreateEmpty(width, height, false, Image.Format.Rgba8);

		Color white = new Color(1f, 1f, 1f);
		Color black = new Color(0,0,0);

		for (int x = 0; x < width; x++){
			for (int y = 0; y < height; y++){
				Color color = black.Lerp(white, noiseArray[x, y]);
				image.SetPixel(x, y, color);
			}
		}
		return image;
	}

	public Image CreateColoredImageFromNoise(float[,] noiseArray, Array<TerrainTypes> terrainTypes, int width, int height){
		Image image = Image.CreateEmpty(width, height, false, Image.Format.Rgba8);

		Color land = terrainTypes[0]._color;
		Color water = terrainTypes[1]._color;
		Color sand = terrainTypes[2]._color;
		
		for (int x = 0; x < width; x++){
			for (int y = 0; y < height; y++){
				if (noiseArray[x, y] > 0.5f){
					image.SetPixel(x, y, land);
				}
				else if (noiseArray[x, y] > 0.3f){
					image.SetPixel(x, y, sand);
				}
				else{
					image.SetPixel(x, y, water);
				}
			
			}
		}
		return image;
	}
}
