using Godot;
using System;

public partial class CloseButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void closeApp()
	{
		GD.Print("Button pressed");
		GetTree().Quit();
	}
	public override void _Pressed()
	{
		base._Pressed();
		closeApp();
	}
}
