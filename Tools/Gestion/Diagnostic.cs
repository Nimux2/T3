using Godot;
using System;

public partial class Diagnostic : Node2D
{

	private const string DEFAULT_TEXT_DIAG = "Vous avez trouvé la maladie : [maladie]";
	private const string DEFAULT_TEXT_NOT_DIAG = "Vous n'avez pas trouvé la maladie : [maladie]";
	private const string DEFAULT_TEXT_LATE = "Vous avez [temps] minutes de retard.";
	
	private Label textDiag;
	private Label textLate;
	private Button buttonContinue;
	private CustomSignals signal;
	public override void _Ready()
	{
		signal = GetNode<CustomSignals>("/root/CustomSignal");
		signal.DiagnosticPrintData += (maladie, pourcentage, lateTime) => PrintAllData(maladie, pourcentage, lateTime);
		GD.Randomize();
		foreach (var node in this.GetChildren())
		{
			if (node.Name == "DiagInfo")
			{
				textDiag = (Label)node;
			}
			else if (node.Name == "LateInfo")
			{
				textLate = (Label)node;
			}
			else if (node.Name == "ContinueButton")
			{
				((Button)node).Pressed += () => ContinuePressed();
			}
		}
	}
	
	private void PrintAllData(string maladie, int pourcentage, int lateTime)
	{
		this.Visible = true;
		textDiag.Text = String.Empty;
		textLate.Text = String.Empty;
		int rand = GD.RandRange(0, 100);
		GD.Print($"Random : {rand}");
		if (rand <= pourcentage)
		{
			textDiag.Text = DEFAULT_TEXT_DIAG.Replace("[maladie]", maladie);
		}
		else
		{
			textDiag.Text = DEFAULT_TEXT_NOT_DIAG.Replace("[maladie]", maladie);
		}

		textLate.Text = DEFAULT_TEXT_LATE.Replace("[temps]", lateTime.ToString());
	}

	private void ContinuePressed()
	{
		this.Visible = false;
	}
}
