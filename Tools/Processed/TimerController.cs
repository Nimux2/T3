using Godot;
using System;

public partial class TimerController : Timer
{
	// Called when the node enters the scene tree for the first time.
	private Label timerShow;
	private int hour = 8;
	private int minute = 0;
	
	public override void _Ready()
	{
		timerShow = this.GetChildOrNull<Label>(0);
		this.Timeout += () => PrintTimer();
	}
	private void PrintStrartTimer()
	{
		timerShow.Text = hour.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0');
	}
	
	private void PrintTimer()
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

		timerShow.Text = hour.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0');
	}
	private void StartButtonPressed()
	{
		this.Start();
	}
	private void DiagButtonPressed()
	{
		this.Stop();
	}
	private void ContinueButtonPressed()
	{
		this.Start();
	}
}
