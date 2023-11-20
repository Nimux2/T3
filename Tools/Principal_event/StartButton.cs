using Godot;
using System;
using System.Threading.Tasks;
using Godot.Collections;

public partial class StartButton : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private const string defaultTextStart = "Voulez-vous commencer une partie ?";
	private const string defaultTextRestart = "Voulez-vous recommencer une partie ?";

	private Label textStartOrRestart;
	public override void _Ready()
	{
		foreach (var node in this.GetChildren())
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
			else if (node.Name == "StartInfo")
			{
				textStartOrRestart = (Label)node;
				textStartOrRestart.Text = defaultTextStart;
			}
		}
	}
	private void StartPressed()
	{
		GD.Print("Start session");
		this.Visible = false;
		GamePlay.currentInstance.CodeTest();
	}

	private void ButtonFlashing(Button button)
	{
		Action<object> action = delegate(object o)
		{
			while (true)
			{
				((Button)o).ShowBehindParent = !((Button)o).ShowBehindParent;
				if (((Button)o).ShowBehindParent)
				{
					Task.Delay(250).Wait();
				}
				else
				{
					Task.Delay(1000).Wait();
				}
			}
		};
		Task task = new Task(action, button);
		task.Start();
	}

	public void changeToStartOrRestart(ModeAffichage mode)
	{
		switch (mode)
		{
			case ModeAffichage.START:
				textStartOrRestart.Text = defaultTextStart;
				break;
			case ModeAffichage.RESTART:
				textStartOrRestart.Text = defaultTextRestart;
				break;
			default:
				textStartOrRestart.Text = defaultTextStart;
				break;
		}
	}

	public enum ModeAffichage
	{
		START,
		RESTART,
	}
}
