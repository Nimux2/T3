using Godot;
using System;

public partial class ButtonDiag : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Pressed += () => DiagPressed();
	}
	private void DiagPressed()
	{
		GD.Print("Button diag pressed");
	}
}
