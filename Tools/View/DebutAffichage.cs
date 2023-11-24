using Godot;

namespace T3Projet.Tools.View;

public partial class DebutAffichage : Node2D
{
    private const string DEFAULT_TEXT_DEBUT = "Voulez-vous commencer une partie ?";
    private const string DEFAULT_TEXT_RECOMMENCE = "Voulez-vous recommencer une partie ?";
    
    private Label infoDebut;
    public static DebutAffichage instance;
    public override void _Ready()
    {
        infoDebut = GetChild<Label>(3);
        instance = this;
    }

    public void AfficheDebutDebut()
    {
        infoDebut.Text = DEFAULT_TEXT_DEBUT;
        this.Visible = true;
    }

    public void AfficheDebutRecommence()
    {
        infoDebut.Text = DEFAULT_TEXT_RECOMMENCE;
        this.Visible = true;
    }

    public void OnBoutonDebutPressed()
    {
        this.Visible = false;
    }
    
}