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
			if (node.GetType() == typeof(Godot.Button))
			{
				((Button)node).Pressed += () => QuestionPressed((Button)node);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	private void QuestionPressed(Button button)
	{
		GD.Print("Button pressed");
	}
	
}
