using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot.Collections;
using T3Projet.Tools.Gestion;
using T3Projet.Tools.Models;
using T3Projet.Tools.View;

public partial class GamePlay : Node2D
{
	public const int prixConsultation = 25;

	private Partie partie;
	private Patient patient;
	private Maladie maladie;

	private PartieDataAffichage partieDataAffichage;
	private PatientAffichage patientAffichage;
	private DiagnosticAffichage diagnosticAffichage;
	private QuestionsAffichage questionsAffichage;
	private DebutAffichage debutAffichage;
	public override void _Ready()
	{
		while (partieDataAffichage == null)
		{
			partieDataAffichage = PartieDataAffichage.instance;
		}
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
		debutAffichage.AfficheDebutDebut();
	}
	private void CloseApp()
	{
		DATABASE.CloseConnection();
		GetTree().Quit();
	}
	private void JouerPartie()
	{
		partie = new Partie();
		this.JouerConsultation();
	}
	private void JouerConsultation()
	{
		partie.ChangerInfoPartie(partieDataAffichage);
		partie.NbConsultation++;
		int idPatient = Patient.RandomIdPatient();
		patient = new Patient(idPatient);
		int retardEffect = (partie.RetardAvance > 0) ? partie.RetardAvance * 5 : partie.RetardAvance * 2;
		patientAffichage.ChangerValeurBarreStress(patient.Stress + retardEffect);
		patientAffichage.ChangerValeurBarreDiagnostic(0);
		string nomImage = patient.DonnerNomImageCaractere(ImagesPatient.Types.DEFAULT);
		if (nomImage != "Default")
		{
			patientAffichage.ChangerCaracterePatient(nomImage);
		}
		else
		{
			patientAffichage.ChangerCaracterePatient(nomImage);
			patientAffichage.FaireParlerPatientCharParChar("Désolé, je me présente. Je suis l'infirmier ZimmerDoc et j'ai le malheur de vous dire que notre médecin généraliste n'est pas présent aujourd'hui (Congé). Veiller, revenir plus-tard ? \u1F637");
		}
		int idMaladie = Maladie.RandomIdMaladie();
		GD.Print("Id maladie = " + idMaladie);
		maladie = new Maladie(idMaladie);
		questionsAffichage.ChangerEtatMasque(true);
		patientAffichage.FaireParlerPatientCharParChar("Bonjour, je suis [name] et je suis malade. GoodDoc, pouvez-vous m'aider ?" , patient.Nom);
	}
	private void AjouterQuestion()
	{
		List<Question> questions = maladie.QuestionsSuivante();
		if (questions != null)
		{
			int i = 0;
			foreach (Question question in questions)
			{
				questionsAffichage.AfficheQuestionA(question, i);
				i++;
			}
		}
		else
		{
			questionsAffichage.ChangerEtatMasque(true);
		}
	}
	private void FaireLeDiagnostic()
	{
		questionsAffichage.AfficheQuestionReset();
		partie.RetardAvance = TimerController.CalculRetard(partie.NbConsultation);
		GD.Randomize();
		int result = GD.RandRange(0, 100);
		if (partie.RetardAvance <= 0 && result <= patient.Diag)
		{
			diagnosticAffichage.AfficheDiagnosticVraiAvance(maladie.Nom, partie.RetardAvance * (-1));
		}
		else if (partie.RetardAvance <= 0 && result > patient.Diag)
		{
			diagnosticAffichage.AfficheDiagnosticFauxAvance(maladie.Nom, partie.RetardAvance * (-1));
			partie.DiagFaux++;
		}
		else if (partie.RetardAvance > 0 && result <= patient.Diag)
		{
			diagnosticAffichage.AfficheDiagnosticVraiRetard(maladie.Nom, partie.RetardAvance);
		}
		else
		{
			diagnosticAffichage.AfficheDiagnosticFauxRetard(maladie.Nom, partie.RetardAvance);
			partie.DiagFaux++;
		}
	}

	private void AppliquerLaReponse(int diag, int stress)
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
			FaireLeDiagnostic();
		}
		else if (patient.Diag >= 100)
		{
			partie.StressEleve++;
			FaireLeDiagnostic();
		}
		else
		{
			patientAffichage.FaireParlerPatientCharParChar(réponse.RéponseText);
		}
	}
	private void ContinuePartie()
	{
		partie.Argent += prixConsultation;
		if (TimerController.TempsEstPasser())
		{
			debutAffichage.AfficheDebutRecommence();
		}
		else if (partie.NbConsultation == TimerController.CalculNbConsultation())
		{
			debutAffichage.AfficheDebutRecommence();
		}
		else
		{
			this.JouerConsultation();
		}
	}
	public void OnBoutonFermerPressed()
	{
		CloseApp();
	}
	public void OnBoutonFermer2Pressed()
	{
		CloseApp();
	}
	public void OnBoutonContinuerPressed()
	{
		ContinuePartie();
	}
	public void OnBoutonDebutPressed()
	{
		this.JouerPartie();
	}
	public void OnBoutonDiagnosticPressed()
	{
		FaireLeDiagnostic();
	}
	public void OnBoutonQuestionBoutonQuestionPressed(int diag, int stress)
	{
		AppliquerLaReponse(diag , stress);
	}
	public void OnTimerCharParCharFin()
	{
		questionsAffichage.ChangerEtatMasque(false);
		AjouterQuestion();
	}
}
