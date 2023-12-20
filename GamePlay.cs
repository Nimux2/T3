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
	// Attribute pour la partie.
	private Partie partie;
	
	// Liste des attributes pour la consultation.
	private Patient patient;
	private Maladie maladie;

	// Liste des attributes pour l'affichage.
	private PartieDataAffichage partieDataAffichage;
	private PatientAffichage patientAffichage;
	private DiagnosticAffichage diagnosticAffichage;
	private QuestionsAffichage questionsAffichage;
	private DebutAffichage debutAffichage;
	
	/// <summary>
	/// Méthode qui initialise le jeu avec la recherche des views cree par Godot.
	/// </summary>
	/// <returns></returns>
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
	
	/// <summary>
	/// Méthode de fermeture de l'application.
	/// </summary>
	/// <returns></returns>
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
	
	/// <summary>
	/// Méthode qui initialise le début de consultation.
	/// </summary>
	/// <returns></returns>
	private void JouerConsultation()
	{
		partie.NbConsultation++;
		partie.ChangerInfoPartie(partieDataAffichage);
		int idPatient = Patient.RandomIdPatient();
		patient = new Patient(idPatient);
		if (partie.RetardAvance < 0 || partie.RetardAvance > 0)
		{
			patient.Stress +=  partie.RetardAvance * 2;
		}
		patientAffichage.ChangerValeurBarreStress(patient.Stress);
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
	
	/// <summary>
	/// Méthode qui applique la question au bouton des questions.
	/// </summary>
	/// <returns></returns>
	private void AjouterQuestion()
	{
		List<Question> questions = maladie.QuestionsSuivante();
		if (questions != null)
		{
			int i = 0;
			foreach (Question question in questions)
			{
				questionsAffichage.AfficheQuestion(question, i);
				i++;
			}
		}
		else
		{
			questionsAffichage.ChangerEtatMasque(true);
		}
	}
	
	/// <summary>
	/// Méthode qui applique les données pour le diagnostic.
	/// </summary>
	/// <returns></returns>
	private void FaireLeDiagnostic()
	{
		questionsAffichage.AfficheQuestionReset();
		partie.CalculRetardAvance();
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
	
	/// <summary>
	/// Méthode qui applique les effets de la question sur les éléments.
	/// </summary>
	/// <param name="diag">effet sur le diag</param>
	/// <param name="stress">effet sur le stress</param>
	/// <param name="temps">effet sur le temps</param>
	/// <returns></returns>
	private void AppliquerLaReponse(int diag, int stress, int temps)
	{
		questionsAffichage.ChangerEtatMasque(true);
		patient.Diag += diag;
		patient.Stress += stress;
		if (patient.Stress < 0)
		{
			patient.Stress = 0;
		}
		partie.Temps += temps;
		partie.ChangerTemps(partieDataAffichage);
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
	
	/// <summary>
	/// Méthode pour savoir si il peut continuer la partie.
	/// </summary>
	/// <returns></returns>
	private void ContinuePartie()
	{
		partie.Argent += Partie.PRIX_CONSULTATION;
		if (partie.TempsEstPassee())
		{
			debutAffichage.AfficheDebutRecommence();
		}
		else if (partie.APlusDePatient())
		{
			debutAffichage.AfficheDebutRecommence();
		}
		else
		{
			this.JouerConsultation();
		}
	}
	
	/*
	 * Liste des signaux généré par Godot. ("Controlleur")
	 */
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
	public void OnBoutonQuestionBoutonQuestionPressed(int diag, int stress , int temps)
	{
		AppliquerLaReponse(diag , stress, temps);
	}
	public void OnTimerCharParCharFin()
	{
		questionsAffichage.ChangerEtatMasque(false);
		AjouterQuestion();
	}
}
