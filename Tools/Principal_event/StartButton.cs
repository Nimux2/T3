using Godot;
using System;
using System.Threading.Tasks;
using Godot.Collections;

public partial class StartButton : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Array<Node> nodes = this.GetChildren();
		foreach (var node in nodes)
		{
			if (node.Name == "StartButton")
			{
				((Button)node).Pressed += () => StartPressed();
				ButtonFlashing((Button)node);
			}
			else if (node.Name == "CloseButton2")
			{
				((Button)node).Pressed += () => GetTree().Quit();
			}
		}
	}
	private void StartPressed()
	{
		GD.Print("Start session");
		this.Visible = false;
		Timer_controller.currentInstance.Start();
		GamePlay.currentInstance.CodeTest();
		//appel 
	}

	private void ButtonFlashing(Button button)
	{
		Action<object> action = delegate(object o)
		{
			while (true)
			{
				((Button)o).Visible = !((Button)o).Visible;
				if (((Button)o).Visible)
				{
					Task.Delay(1000).Wait();
				}
				else
				{
					Task.Delay(500).Wait();
				}
			}
		};
		Task task = new Task(action, button);
		task.Start();
	}
	
}
