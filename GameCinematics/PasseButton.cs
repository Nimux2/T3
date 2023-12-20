using Godot;
using System;

public partial class PasseButton : Button
{
	public override void _Pressed()
	{
		PackedScene newScene = GD.Load<PackedScene>("res://GamePlay.tscn");
		GetTree().ChangeSceneToPacked(newScene);
	}
}
