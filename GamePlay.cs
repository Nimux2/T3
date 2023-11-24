using Godot;
using System;
using System.Threading.Tasks;
using Godot.Collections;
using T3Projet.Tools.Models;
using T3Projet.Tools.View;

public partial class GamePlay : Node2D
{
	private int nbPartie = 0;
	private int nbConsultation = 0;
	private int retard_avance = 0;
	private int diagFaux = 0;

	private Patient patient;
	private Maladie maladie;
	
	private PatientAffichage patientAffichage;
	private DiagnosticAffichage diagnosticAffichage;
	private QuestionsAffichage questionsAffichage;
	private DebutAffichage debutAffichage;

	public static GamePlay instance;
	public override void _Ready()
	{
		instance = this;
		GD.Print("Start");
		while (patientAffichage == null)
		{
			patientAffichage = PatientAffichage.instance;
		}
		while (questionsAffichage == null)
		{
			questionsAffichage = QuestionsAffichage.instance;
		}
		
		while (diagnosticAffichage == null)
		{
			diagnosticAffichage = DiagnosticAffichage.instance;
		}
		while (debutAffichage == null)
		{
			debutAffichage = DebutAffichage.instance;
		}
		patientAffichage.AddInstanceBarreDiagnostic(GetNode<ProgressBar>("DiagnosticData/BarreDiagnostic"));
		patientAffichage.AddInstanceBarreStress(GetNode<ProgressBar>("StressData/BarreStress"));
		GD.Print("End");
		debutAffichage.AfficheDebutDebut();
	}
	private void CloseApp()
	{
		DATABASE.CloseConnection();
		GetTree().Quit();
	}
	public void OnBoutonFermerPressed()
	{
		CloseApp();
	}
	public void JouerPartie()
	{
		nbConsultation = 0;
		retard_avance = 0;
		diagFaux = 0;
		GD.Print("Debut de consultation");
		this.JouerConsultation();
	}
	private void JouerConsultation()
	{
		int idPatient = Patient.RandomIdPatient();
		patient = new Patient(0);
		patientAffichage.ChangerValeurBarreStress(20); // from patient
		patientAffichage.ChangerValeurBarreDiagnostic(0);
		//load image
		int idMaladie = Maladie.RandomIdMaladie();
		maladie = new Maladie(1);
		questionsAffichage.ChangerEtatMasque(true);
		patientAffichage.FaireParlerPatient("Bonjour, je suis [name]." , patient.Nom);
		nbConsultation++;
		AjouterQuestion();
		questionsAffichage.ChangerEtatMasque(false);
	}

	private void AjouterQuestion()
	{
		int i = 0;
		foreach (Question question in maladie.QuestionsSuivante())
		{
			questionsAffichage.AfficheQuestionA(question , i);
			i++;
		}
	}
	public void OnBoutonFermer2Pressed()
	{
		CloseApp();
	}
	public void OnBoutonContinuerPressed()
	{
		nbConsultation++;
		if (diagFaux >= 3)
		{
			//fin de partie trop de mauvais diagnostic
			debutAffichage.AfficheDebutRecommence();
		}
		else
		{
			this.JouerConsultation();
		}
	}
	public void OnBoutonDebutPressed()
	{
		GD.Print("Debut de partie");
		this.JouerPartie();
	}
	public void OnBoutonDiagnosticPressed()
	{
		retard_avance = TimerController.CalculRetard(nbConsultation);
		GD.Randomize();
		int result = GD.RandRange(0, 100);
		if (retard_avance <= 0 && result <= patient.Diag)
		{
			diagnosticAffichage.AfficheDiagnosticVraiAvance(maladie.Nom , retard_avance * (-1));
		}
		else if (retard_avance <= 0 && result > patient.Diag)
		{
			diagnosticAffichage.AfficheDiagnosticFauxAvance(maladie.Nom , retard_avance * (-1));
			diagFaux++;
		}
		else if (retard_avance > 0 && result <= patient.Diag)
		{
			diagnosticAffichage.AfficheDiagnosticVraiRetard(maladie.Nom , retard_avance);
		}
		else
		{
			diagnosticAffichage.AfficheDiagnosticFauxRetard(maladie.Nom , retard_avance);
			diagFaux++;
		}
	}
	public void OnBoutonQuestionPressed(int diag , int stress)
	{
		questionsAffichage.ChangerEtatMasque(true);
		patient.Diag += (diag / 10);
		patient.Stress += (stress / 10);
		GD.Print("Diag : " + patient.Diag + ", Stress : " + patient.Stress);
		patientAffichage.ChangerValeurBarreDiagnostic(patient.Diag);
		patientAffichage.ChangerValeurBarreStress(patient.Stress);
		Réponse réponse = maladie.RéponseQuestion(patient.Stress);
		if (réponse == null)
		{
			//fin de partie stress trop elever
			debutAffichage.AfficheDebutRecommence();
			GD.Print("Tu est mort de stress");
		}
		else
		{
			patientAffichage.FaireParlerPatientCharParChar(réponse.RéponseText);
			//patientAffichage.FaireParlerPatient(réponse.RéponseText);
			AjouterQuestion();
		}
		questionsAffichage.ChangerEtatMasque(false);
	}
}
