using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot.Collections;
using T3Projet.Tools.Gestion;
using T3Projet.Scripts.Models;
using T3Projet.Scripts.View;

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
	private DébutAffichage débutAffichage;
	
	/// <summary>
	/// Méthode qui initialise le jeu avec la recherche des views cree par Godot.
	/// </summary>
	/// <returns></returns>
	public override void _Ready()
	{
		ChangerVariableStaticDePartie();
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
		while (débutAffichage == null)
		{
			débutAffichage = DébutAffichage.instance;
		}
		patientAffichage.AjouterInstanceBarreDiagnostic(GetNode<ProgressBar>("DiagnosticData/BarreDiagnostic"));
		patientAffichage.AjouterInstanceBarreStress(GetNode<ProgressBar>("StressData/BarreStress"));
		débutAffichage.AfficherDébutDébut();
	}

	/// <summary>
	/// Méthode qui charge les variables static dans les parties.
	/// </summary>
	/// <returns></returns>
	public void ChangerVariableStaticDePartie()
	{
		try {
			ConfigFile config = new ConfigFile();
			config.Load("res://Config/gameconfig.cfg");
			if (config.HasSection("Game") && config.HasSectionKey("Game", "price_appointment"))
			{
				Partie.PRIX_CONSULTATION = config.GetValue("Game", "price_appointment").As<double>();
			}

			if (config.HasSection("Game") && config.HasSectionKey("Game", "min_patient"))
			{
				Partie.MIN_PATIENT = config.GetValue("Game", "min_patient").As<int>();
			}

			if (config.HasSection("Game") && config.HasSectionKey("Game", "max_patient"))
			{
				Partie.MAX_PATIENT = config.GetValue("Game", "max_patient").As<int>();
			}

			if (config.HasSection("Game") && config.HasSectionKey("Game", "hour_start"))
			{
				Partie.HEURE_DEPART = config.GetValue("Game", "hour_start").As<int>();
			}

			if (config.HasSection("Game") && config.HasSectionKey("Game", "minute_start"))
			{
				Partie.MINUTE_DEPART = config.GetValue("Game", "minute_start").As<int>();
			}

			if (config.HasSection("Game") && config.HasSectionKey("Game", "hour_end"))
			{
				Partie.HEURE_FIN = config.GetValue("Game", "hour_end").As<int>();
			}

			if (config.HasSection("Game") && config.HasSectionKey("Game", "minute_end"))
			{
				Partie.MINUTE_FIN = config.GetValue("Game", "minute_end").As<int>();
			}
		}
		catch (Exception err)
		{
			GD.Print("ERROR : Config = " + err.Message);
		}
	}
	
	/// <summary>
	/// Méthode de fermeture de l'application.
	/// </summary>
	/// <returns></returns>
	private void FermerApplication()
	{
		Database.FermerConnection();
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
		partie.ChangerInfoPartie(partieDataAffichage);
		partie.ChangerTemps(partieDataAffichage);
		partie.NbConsultation++;
		int idPatient = Patient.GenererRandomIdPatient();
		patient = new Patient(idPatient);
		if (partie.RetardAvance < 0 || partie.RetardAvance > 0)
		{
			patient.Stress +=  (int)(partie.RetardAvance / 2);
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
		int idMaladie = Maladie.GenererRandomIdMaladie();
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
		List<Question> questions = maladie.DonnerQuestionsSuivante();
		if (questions != null)
		{
			int i = 0;
			foreach (Question question in questions)
			{
				questionsAffichage.AfficherQuestion(question, i);
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
	private void FaireDiagnostic()
	{
		questionsAffichage.AfficherQuestionReset();
		partie.CalculRetardAvance();
		GD.Randomize();
		int result = GD.RandRange(0, 100);
		if (partie.RetardAvance <= 0 && patient.Stress >= 100)
		{
			diagnosticAffichage.AfficherDiagnosticStressAvance(maladie.Nom, partie.RetardAvance * (-1));
			partie.DiagFaux++;
		}
		else if (partie.RetardAvance > 0 && patient.Stress >= 100)
		{
			diagnosticAffichage.AfficherDiagnosticStressRetard(maladie.Nom, partie.RetardAvance);
			partie.DiagFaux++;
		}
		else if (partie.RetardAvance <= 0 && result <= patient.Diag)
		{
			diagnosticAffichage.AfficherDiagnosticVraiAvance(maladie.Nom, partie.RetardAvance * (-1));
		}
		else if (partie.RetardAvance <= 0 && result > patient.Diag)
		{
			diagnosticAffichage.AfficherDiagnosticFauxAvance(maladie.Nom, partie.RetardAvance * (-1));
			partie.DiagFaux++;
		}
		else if (partie.RetardAvance > 0 && result <= patient.Diag)
		{
			diagnosticAffichage.AfficherDiagnosticVraiRetard(maladie.Nom, partie.RetardAvance);
		}
		else
		{
			diagnosticAffichage.AfficherDiagnosticFauxRetard(maladie.Nom, partie.RetardAvance);
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
	private void AppliquerRéponse(int diag, int stress, int temps)
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
		patientAffichage.ChangerValeurBarreDiagnostic(patient.Diag);
		patientAffichage.ChangerValeurBarreStress(patient.Stress);
		Réponse réponse = maladie.DonnerRéponseAQuestion(patient.Stress);
		if (réponse == null)
		{
			FaireDiagnostic();
		}
		else if (patient.Stress >= 100)
		{
			partie.StressEleve++;
			FaireDiagnostic();
		}
		else if (patient.Diag >= 100)
		{
			FaireDiagnostic();
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
	private void ContinuerPartie()
	{
		partie.Argent += Partie.PRIX_CONSULTATION;
		if (partie.TempsEstPassee())
		{
			débutAffichage.AfficherDébutRecommence();
		}
		else if (partie.APlusDePatient())
		{
			débutAffichage.AfficherDébutRecommence();
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
		FermerApplication();
	}
	public void OnBoutonFermer2Pressed()
	{
		FermerApplication();
	}
	public void OnBoutonContinuerPressed()
	{
		ContinuerPartie();
	}
	public void OnBoutonDébutPressed()
	{
		this.JouerPartie();
	}
	public void OnBoutonDiagnosticPressed()
	{
		FaireDiagnostic();
	}
	public void OnBoutonQuestionBoutonQuestionPressed(int diag, int stress , int temps)
	{
		AppliquerRéponse(diag , stress, temps);
	}
	public void OnTimerCharParCharFin()
	{
		questionsAffichage.ChangerEtatMasque(false);
		AjouterQuestion();
	}
}
