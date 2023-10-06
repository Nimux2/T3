using Godot;
using System;

public partial class Timer_controller : Timer
{
	// Called when the node enters the scene tree for the first time.
	private Label timer_show;
	private int hour = 8;
	private int minute = 0;
	
	public override void _Ready()
	{
		timer_show = this.GetChildOrNull<Label>(0);
		this.Timeout += () => printTimer();
		this.Start();
		printStrartTimer();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	private void printStrartTimer()
	{
		timer_show.Text = hour.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0');
	}
	
	private void printTimer()
	{
		minute++;
		if (minute >= 60)
		{
			hour++;
			minute = 0;
		}
		if (hour >= 24)
		{
			//next day
		}

		timer_show.Text = hour.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0');
	}
}
