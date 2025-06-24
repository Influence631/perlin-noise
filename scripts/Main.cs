using Godot;
using System;
using Godot.Collections;


public partial class Main : Node2D
{
	public int width;
	public int height;
	public float scale = 50f;
	int octaves = 4;
	public float persistance = 0.5f;
	public float lacunarity = 2f;
	public float noiseScale = 120.1123f;
	public float step = 0.5f;
	float[,] noiseArray;
	MeshInstance2D plane;
	Random random;
	Vector2 offset = Vector2.Zero;

	Label perLabel, lacLabel, scaleLabel;
	[Export] private Array<TerrainTypes> terrainTypes;
	public enum DisplayMode{
		BlackAndWhite,
		Colored
	};
	public DisplayMode displayMode;

	NoiseArrayGenerator gen;
	ImageGenerator imgGen;
	public override void _Ready(){
		displayMode = DisplayMode.Colored;
		plane = GetNode<MeshInstance2D>("plane");

		random = new Random();
		var viewport = GetViewport().GetTexture().GetSize();
		width = (int)viewport.X / 2;
		height = (int)viewport.Y / 2;
		int seed = random.Next(0, 513);
		
		
		gen = new NoiseArrayGenerator(seed);
		imgGen = new ImageGenerator();

		Instantiate(persistance,lacunarity, noiseScale, offset);


		perLabel = GetNode<Label>("%persistance label");
		lacLabel = GetNode<Label>("%lacunarity label");
		scaleLabel = GetNode<Label>("%scale label");
		UpdateLabels();
	}

	public void Instantiate(float persistance, float lacunarity, float noiseScale, Vector2 offset){
		this.persistance = persistance;
		this.lacunarity = lacunarity;
		this.noiseScale = noiseScale;
		this.offset = offset;


		noiseArray = gen.GenerateNoiseArray(width, height, noiseScale, octaves, offset, persistance, lacunarity);

		ApplyTexture();
		
	}

	

	public void ApplyTexture(){
		Image img;
		if (displayMode == DisplayMode.BlackAndWhite){
			img = imgGen.CreateImageFromNoise(noiseArray, width, height);
		}
		else{
			img = imgGen.CreateColoredImageFromNoise(noiseArray, terrainTypes, width, height);
		}
		ImageTexture texture = ImageTexture.CreateFromImage(img);

		plane.Texture = texture;
		plane.Scale = new Vector2(width, height);
	}
	public void GenerateButton(){
		if (this.displayMode == DisplayMode.Colored)
			displayMode = DisplayMode.BlackAndWhite;
		else
			displayMode = DisplayMode.Colored;
		Instantiate(persistance,lacunarity, noiseScale, offset);
		UpdateLabels();
	}
	public void PersistanceChanged(float value){
		this.persistance = value;
		Instantiate(persistance,lacunarity, noiseScale, offset);
		UpdateLabels();
		
	}
	public void LacunarityChanged(float value){
		this.lacunarity = value;
		Instantiate(persistance,lacunarity, noiseScale, offset);
		UpdateLabels();
	}
	
	public void ScaleChanged(float value){
		this.noiseScale = value;
		Instantiate(persistance,lacunarity, noiseScale, offset);
		UpdateLabels();
	}

	public void OffsetXIncreased(){
		offset.X += step;
		Instantiate(persistance,lacunarity, noiseScale, offset);
	}

	public void OffsetXDecreased(){
		offset.X -= step;
		Instantiate(persistance,lacunarity, noiseScale, offset);
	}

	public void OffsetYIncreased(){
		offset.Y -= step;
		Instantiate(persistance,lacunarity, noiseScale, offset);
	}

	public void OffsetYDecreased(){
		offset.Y += step;
		Instantiate(persistance,lacunarity, noiseScale, offset);
	}

	public void UpdateLabels(){
		perLabel.Text = "persistance " + this.persistance;
		lacLabel.Text = "lacunarity" + this.lacunarity;
		scaleLabel.Text = "noise scale" + this.noiseScale;
	}
}
