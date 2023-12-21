using System;
using System.Threading.Tasks;
using Godot;
using T3Projet.Scripts.Models;

namespace T3Projet.Scripts.View;

public partial class PatientAffichage : Node2D
{
    // Liste des attributes des éléments graphique.
    private Marker2D marqueurHaut;
    private Sprite2D personnage;
    private Marker2D marqueurBas;
    private RichTextLabelTimer parolePersonnage;
    private ProgressBar barreDiagnostic;
    private ProgressBar barreStress;
    
    // Singleton pour l'initialisation du jeu
    public static PatientAffichage instance;
    
    /// <summary>
    /// Méthode qui charge les éléments pour le texte et initialise le singleton.
    /// </summary>
    /// <returns></returns>
    public override void _Ready()
    {
        marqueurHaut = GetChild<Marker2D>(0);
        personnage = GetChild<Sprite2D>(1);
        marqueurBas = GetChild<Marker2D>(2);
        parolePersonnage = GetChild<RichTextLabelTimer>(4);
        instance = this;
    }
    
    /// <summary>
    /// Méthode qui permet de placer automatiquement le patient.
    /// </summary>
    /// <returns></returns>
    private void AutoPlacer()
    {
        Vector2 originPositionCalcul = new Vector2();
        originPositionCalcul.X = this.marqueurBas.GlobalPosition.X;
        originPositionCalcul.Y = (this.marqueurBas.GlobalPosition.Y + this.marqueurHaut.GlobalPosition.Y) / 2;
        this.personnage.GlobalPosition = originPositionCalcul;
        float scaleCalcul = (this.marqueurBas.GlobalPosition.Y - this.marqueurHaut.GlobalPosition.Y) / this.personnage.Texture.GetHeight();
        this.personnage.Scale = new Vector2(scaleCalcul, scaleCalcul);
    }
    
    /// <summary>
    /// Méthode qui change l'image du patient (autoplacement).
    /// </summary>
    /// <param name="nomImage"></param>
    /// <returns></returns>
    public void ChangerCaracterePatient(string nomImage)
    {
        if (nomImage != null)
        {
            string path = $"res://PicturesPatients/{nomImage}.png";
            Texture2D texture = GD.Load<Texture2D>(path);
            this.personnage.Texture = texture;
            AutoPlacer();
        }
    }
    
    /// <summary>
    /// Méthode qui fait parler le patient de façon simple.
    /// </summary>
    /// <param name="parole"></param>
    /// <param name="nom"></param>
    /// <returns></returns>
    public void FaireParlerPatient(string parole , string nom = null)
    {
        parolePersonnage.EcrireSimple(parole.Replace("[name]", nom));
    }
    
    /// <summary>
    /// Méthode qui fait parler le patient de façon complexe charactere par charactere.
    /// </summary>
    /// <param name="parole"></param>
    /// <param name="nom"></param>
    /// <returns></returns>
    public void FaireParlerPatientCharParChar(string parole , string nom = null)
    {
        parolePersonnage.EcrireCharParChar(parole.Replace("[name]", nom));
    }
    
    /// <summary>
    /// Méthode qui ajoute la barre de diagnostic.
    /// </summary>
    /// <param name="barreDiag"></param>
    /// <returns></returns>
    public void AjouterInstanceBarreDiagnostic(ProgressBar barreDiag) //nécessaire pas possible de recup depuis ici
    {
        this.barreDiagnostic = barreDiag;
    }
    
    /// <summary>
    /// Méthode qui change l'affichage de l'avancer du diagnostic.
    /// </summary>
    /// <param name="diag"></param>
    /// <returns></returns>
    public void ChangerValeurBarreDiagnostic(int diag)
    {
        if (diag > 100)
        {
            barreDiagnostic.Value = 100;
        }
        else
        {
            barreDiagnostic.Value = diag;
        }
    }
    
    /// <summary>
    /// Méthode qui ajoute la barre de stress.
    /// </summary>
    /// <param name="barreDiag"></param>
    /// <returns></returns>
    public void AjouterInstanceBarreStress(ProgressBar barreStress) //nécessaire pas possible de recup depuis ici
    {
        this.barreStress = barreStress;
    }
    
    /// <summary>
    /// Méthode qui change l'affichage du stress du patient.
    /// </summary>
    /// <param name="stress"></param>
    /// <returns></returns>
    public void ChangerValeurBarreStress(int stress)
    {
        if (stress <= 100)
        {
            barreStress.Value = stress;
        }
    }
}