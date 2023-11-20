using Godot;
using System;

public partial class DiagButton : Button
{
	private CustomSignals signal;
	public override void _Ready()
	{
		signal = GetNode<CustomSignals>("/root/CustomSignal");
		this.Pressed += () => DiagPressed();
	}

	private void DiagPressed()
	{
		GD.Print("Button diag pressed");
		//diagnostic.printAllData("Peste" , 50 , 0);
		signal.EmitSignal(nameof(CustomSignals.DiagnosticPrintData), "Peste", 50, 0);
	}
}
