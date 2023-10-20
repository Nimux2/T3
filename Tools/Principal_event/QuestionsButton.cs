using Godot;
using System;
using Godot.Collections;

public partial class QuestionsButton : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Array<Node> nodes = this.GetChildren();
		foreach (var node in nodes)
		{
			if (node.GetType() == typeof(Button))
			{
				((Button)node).Pressed += () => QuestionPressed((Button)node);
			}
		}
	}
	private void QuestionPressed(Button button)
	{
		GD.Print($"Button pressed : {button.Name}");
	}
	
}
