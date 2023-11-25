using System;
using System.Threading.Tasks;
using Godot;
using T3Projet.Tools.Models;

namespace T3Projet.Tools.View;

public partial class PatientAffichage : Node2D
{
    public static PatientAffichage instance;
    
    private Marker2D marqueurHaut;
    private Sprite2D personnage;
    private Marker2D marqueurBas;
    private RichTextLabelTimer parolePersonnage;

    private ProgressBar barreDiagnostic;
    private ProgressBar barreStress;
    
    public override void _Ready()
    {
        marqueurHaut = GetChild<Marker2D>(0);
        personnage = GetChild<Sprite2D>(1);
        marqueurBas = GetChild<Marker2D>(2);
        parolePersonnage = GetChild<RichTextLabelTimer>(4);
        instance = this;
    }
    private void AutoPlacement()
    {
        Vector2 originPositionCalcul = new Vector2();
        originPositionCalcul.X = this.marqueurBas.GlobalPosition.X;
        originPositionCalcul.Y = (this.marqueurBas.GlobalPosition.Y + this.marqueurHaut.GlobalPosition.Y) / 2;
        this.personnage.GlobalPosition = originPositionCalcul;
        float scaleCalcul = (this.marqueurBas.GlobalPosition.Y - this.marqueurHaut.GlobalPosition.Y) / this.personnage.Texture.GetHeight();
        this.personnage.Scale = new Vector2(scaleCalcul, scaleCalcul);
    }
    public void ChangerCaracterePatient(string nomImage)
    {
        if (nomImage != null)
        {
            string path = $"{System.IO.Directory.GetCurrentDirectory()}/PatientImages/{nomImage}.png";
            GD.Print(path);
            Texture2D texture = GD.Load<Texture2D>(path);
            this.personnage.Texture = texture;
            AutoPlacement();
        }
    }
    public void FaireParlerPatient(string parole , string nom = null)
    {
        parolePersonnage.EcrireSimple(parole.Replace("[name]", nom));
    }
    public void FaireParlerPatientCharParChar(string parole , string nom = null)
    {
        parolePersonnage.EcrireCharParChar(parole.Replace("[name]", nom));
    }
    public void AddInstanceBarreDiagnostic(ProgressBar barreDiag) //nécessaire pas possible de recup depuis ici
    {
        this.barreDiagnostic = barreDiag;
    }
    public void ChangerValeurBarreDiagnostic(int diag)
    {
        if (diag <= 100)
        {
            barreDiagnostic.Value = diag;
        }
    }
    public void AddInstanceBarreStress(ProgressBar barreStress) //nécessaire pas possible de recup depuis ici
    {
        this.barreStress = barreStress;
    }
    public void ChangerValeurBarreStress(int stress)
    {
        if (stress <= 100)
        {
            barreStress.Value = stress;
        }
    }
}