using Godot;

[GlobalClass]
public partial class TerrainTypes : Resource{
    [Export] public string _type = "";
    [Export] public float _height;
    [Export] public Color _color;
}
