using Godot;
using System;

public partial class PasseButton : Button
{
	/// <summary>
	/// Méthode qui charge le gameplay si le bouton est presset.
	/// </summary>
	/// <returns></returns>
	public override void _Pressed()
	{
		PackedScene newScene = GD.Load<PackedScene>("res://GamePlay.tscn");
		GetTree().ChangeSceneToPacked(newScene);
	}
}
