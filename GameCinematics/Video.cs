using Godot;
using System;

public partial class Video : VideoStreamPlayer
{
	/// <summary>
	/// Méthode qui charge le gameplay si la cinematic est fini.
	/// </summary>
	/// <param name="delta">Not use</param>
	/// <returns></returns>
	public override void _Process(double delta)
	{
		if (!this.IsPlaying())
		{
			PackedScene newScene = GD.Load<PackedScene>("res://GamePlay.tscn");
			GetTree().ChangeSceneToPacked(newScene);
		}
	}
}
