using Godot;
using System;

public partial class CloseButton : Button
{
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
