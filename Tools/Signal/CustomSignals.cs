using Godot;
using System;

public partial class CustomSignals : Node
{
	[Signal] 
	public delegate void DiagnosticPrintDataEventHandler(string maladie, int pourcentage, int lateTime);

	[Signal] 
	public delegate void PersonnageChangePersonnageEventHandler(string name, string image_name);

	[Signal]
	public delegate void PersonnageSpeechEventHandler(string texte);

	[Signal]
	public delegate void PersonnageSpeechCharEventHandler(string texte);

	[Signal]
	public delegate void QuestionsButtonLockButtonEventHandler();
	
	[Signal]
	public delegate void QuestionsButtonUnlockButtonEventHandler();
}
