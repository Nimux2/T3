using Godot;
using System;

public partial class GamePlay : Node2D
{
	public static GamePlay currentInstance;

	private CustomSignals signal;
	public override void _Ready()
	{
		currentInstance = this;
		signal = GetNode<CustomSignals>("/root/CustomSignal");
	}

	public void CodeTest()
	{
		GD.Print("Start");
		signal.EmitSignal(nameof(CustomSignals.PersonnageChangePersonnage), "mario", "personnage2");
		signal.EmitSignal(nameof(CustomSignals.PersonnageSpeechChar), "Hello , I am [name] and I love SAIF.");
		signal.EmitSignal(nameof(CustomSignals.QuestionsButtonUnlockButton));

	}
}
