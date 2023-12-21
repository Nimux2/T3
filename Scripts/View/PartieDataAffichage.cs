using Godot;

namespace T3Projet.Scripts.View;

public partial class PartieDataAffichage : Node2D
{
    // Liste des attributs des éléments graphique.
    private Label tempsData;
    private Label argentData;
    private Label patientData;
    private Label bonDiagnosticData;
    private Label mauvaisDiagnosticData;
    private Label stressEleveData;
    
    // Singleton pour l'initialisation du jeu.
    public static PartieDataAffichage instance;
    
    /// <summary>
    /// Méthode qui charge les éléments pour le texte et initialise le singleton.
    /// </summary>
    /// <returns></returns>
    public override void _Ready()
    {
        this.tempsData = GetChild(0).GetChild<Label>(1);
        this.argentData = GetChild(2).GetChild<Label>(1);
        this.patientData = GetChild(3).GetChild<Label>(1);
        this.bonDiagnosticData = GetChild(4).GetChild<Label>(1);
        this.mauvaisDiagnosticData = GetChild(5).GetChild<Label>(1);
        this.stressEleveData = GetChild(6).GetChild<Label>(1);
        instance = this;
    }
    
    /// <summary>
    /// Méthode qui change le temps du jeu.
    /// </summary>
    /// <param name="heure"></param>
    /// <param name="minute"></param>
    /// <returns></returns>
    public void ChangerTempsJeu(int heure , int minute)
    {
        this.tempsData.Text = heure.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0');
    }
    
    /// <summary>
    /// Méthode qui change l'argent gagné par le médecin.
    /// </summary>
    /// <param name="argent"></param>
    /// <returns></returns>
    public void ChangerArgentMedecin(double argent)
    {
        argentData.Text = argent.ToString() + " euros";
    }
    
    /// <summary>
    /// Méthode qui change le nombre de patient en salle d'attente.
    /// </summary>
    /// <param name="nbPatient"></param>
    /// <returns></returns>
    public void ChangerPatientEnAttente(int nbPatient)
    {
        patientData.Text = nbPatient.ToString();
        patientData.Text += (nbPatient == 0 || nbPatient == 1) ? " patient" : " patients";
    }
    
    /// <summary>
    /// Méthode qui change le nombre de bon diagnostic.
    /// </summary>
    /// <param name="bonDiagnostic"></param>
    /// <returns></returns>
    public void ChangerBonDiagnostic(int bonDiagnostic)
    {
        bonDiagnosticData.Text = bonDiagnostic.ToString();
        bonDiagnosticData.Text += (bonDiagnostic == 0 || bonDiagnostic == 1) ? " patient" : " patients";
    }
    
    /// <summary>
    /// Méthode qui change le nombre de mauvais diagnostics.
    /// </summary>
    /// <param name="mauvaisDiagnostic"></param>
    /// <returns></returns>
    public void ChangerMauvaisDiagnostic(int mauvaisDiagnostic)
    {
        mauvaisDiagnosticData.Text = mauvaisDiagnostic.ToString();
        mauvaisDiagnosticData.Text += (mauvaisDiagnostic == 0 || mauvaisDiagnostic == 1) ? " patient" : " patients";
    }
    
    /// <summary>
    /// Méthode qui change le nombre de stress élevé.
    /// </summary>
    /// <param name="stressEleve"></param>
    /// <returns></returns>
    public void ChangerStressEleve(int stressEleve)
    {
        stressEleveData.Text = stressEleve.ToString();
        stressEleveData.Text += (stressEleve == 0 || stressEleve == 1) ? " patient" : " patients";
    }

}