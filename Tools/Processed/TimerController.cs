using Godot;
using System;

public partial class TimerController : Timer
{
	public const int HEURE_DEPART = 8;
	public const int MINUTE_DEPART = 0;
	public const int HEURE_FIN = 12;
	public const int MINUTE_FIN = 0;
	public const int DUREE_CONSULTATION = 30; //en minute
	
	private Label timerLabel;
	private static int heure = 8;
	public static int Heure {
		get => Heure;
		
	}
	private static int minute = 0;
	public static int Minute
	{
		get => Minute; 
	}
	
	public override void _Ready()
	{
		this.timerLabel = this.GetChildOrNull<Label>(0);
		this.Timeout += () => ActualiserTimer();
	}
	private void ResetTimer() 
	{
		heure = HEURE_DEPART;
		minute = MINUTE_DEPART;
		this.timerLabel.Text = heure.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0');
	}
	private void ActualiserTimer() 
	{
		minute++;
		if (minute >= 60)
		{
			heure++;
			minute = 0;
		}
		if (heure == HEURE_FIN && minute == MINUTE_FIN)
		{
			//make event
		}
		this.timerLabel.Text = heure.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0');
	}
	public void OnBoutonDebutPressed()
	{
		this.ResetTimer();
		this.Start();
	}
	public void OnBoutonContinuerPressed()
	{
		this.Start();
	}

	public void OnBoutonDiagnosticPressed()
	{
		this.Stop();
	}
	public static int CalculRetard(int nbConsultation)
	{
		return (((heure - HEURE_DEPART) * 60 ) + minute - MINUTE_DEPART) - (nbConsultation * DUREE_CONSULTATION);
	}
}
