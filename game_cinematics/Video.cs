using Godot;
using System;

public partial class Video : VideoStreamPlayer
{
	public override void _Process(double delta)
	{
		if (!this.IsPlaying())
		{
			PackedScene newScene = GD.Load<PackedScene>("res://GamePlay.tscn");
			GetTree().ChangeSceneToPacked(newScene);
		}
	}
}
