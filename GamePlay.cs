using Godot;
using System;

public partial class GamePlay : Node2D
{
	public static GamePlay currentInstance;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentInstance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void CodeTest()
	{
		GD.Print("Start");
		Sprite2D node = GetNodeOrNull<Sprite2D>("Patient/Personnage");
		Personnage personnage = Personnage.From(node);
		personnage.changePersonnage("mario", "personnage2");
		personnage.speechChar("Hello, I am [name] Bros and I love SAIF !");
	}
}
