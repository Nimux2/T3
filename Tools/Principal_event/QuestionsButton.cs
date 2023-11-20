using Godot;
using System;
using Godot.Collections;

public partial class QuestionsButton : Node2D
{
	private CustomSignals signal;
	private ColorRect mask;
	public override void _Ready()
	{
		signal = GetNode<CustomSignals>("/root/CustomSignal");
		signal.QuestionsButtonLockButton += () => lockButton();
		signal.QuestionsButtonUnlockButton += () => unlockButton();
		Array<Node> nodes = this.GetChildren();
		foreach (var node in nodes)
		{
			GD.Print($"Name : {node.Name}");
			if (node.GetType() == typeof(Button))
			{
				((Button)node).Pressed += () => QuestionPressed((Button)node);
			}
			else if (node.GetType() == typeof(ColorRect))
			{
				mask = (ColorRect)node;
			}
		}
	}
	private void QuestionPressed(Button button)
	{
		GD.Print($"Button pressed : {button.Name}");
	}

	private void lockButton()
	{
		mask.Visible = true;
	}

	private void unlockButton()
	{
		mask.Visible = false;
	}
	
}
